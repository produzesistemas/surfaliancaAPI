import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/_services/product.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Product } from 'src/app/_models/product-model';
import { forkJoin } from 'rxjs';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { ProductStatus } from 'src/app/_models/product-status-model';
import { ProductType } from 'src/app/_models/product-type-model';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html'
})
export class ProductFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public product: Product = new Product();

  lstProductStatus = [];
  lstProductType = [];
  productStatus = ProductStatus;
  productType = ProductType;

  public isPromotion = false;
  public isSpotlight = false;

  uploaded = false;
  logo: any;
  public files: any = [];

  @ViewChild('fileUpload') fileUpload: ElementRef;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private productService: ProductService
  ) { }

  get f() { return this.formAdd.controls; }

  configEditor: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: '',
    translate: 'no',
    defaultParagraphSeparator: '',
    defaultFontName: 'Arial',
    toolbarHiddenButtons: [
      [],
      ['toggleEditorMode', 'removeFormat']
    ]
  };

  ngOnInit() {
    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required]],
      description: [''],
      value: ['', [Validators.required]],
      valuePromotion: [null],
      isPromotion: ['false', [Validators.required]],
      isSpotlight: ['false', [Validators.required]],
      productType: ['', [Validators.required]],
      productStatus: ['', [Validators.required]]
    });

    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.product.id = Number(params.id);
        
      }
    });

        if (this.product.id > 0) {
          this.load();
        }
        this.loadStatus();
        this.loadTypes();
  }

  load() {
     this.productService.get(this.product.id).subscribe(product => {
      this.product = product;
      this.setControls(this.product);
    });
  }

  setControls(item: Product) {
    this.formAdd.controls.id.setValue(item.id);
    this.formAdd.controls.name.setValue(item.name);
    this.formAdd.controls.description.setValue(item.description);
    this.formAdd.controls.value.setValue(item.value);
    this.formAdd.controls.isPromotion.setValue(String(item.isPromotion));
    this.formAdd.controls.isSpotlight.setValue(String(item.isSpotlight));
    this.formAdd.controls.productType.setValue(item.productTypeId);
    this.formAdd.controls.productStatus.setValue(item.productStatusId);
    if (item.isPromotion) {
      this.isPromotion = true;
      this.formAdd.controls.valuePromotion.setValue(item.valuePromotion);
    }
    this.logo = environment.urlImagesProducts + item.imageName;
  }

  onSave() {
    // if ((this.file === undefined) && (this.logo === undefined)){
    //   this.toastr.error('Selecione uma Foto!');
    //   return;
    // }
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    this.product.value = this.formAdd.controls.value.value;
    this.product.description = this.formAdd.controls.description.value;
    this.product.name = this.formAdd.controls.name.value;
    this.product.isSpotlight = this.formAdd.controls.isSpotlight.value === 'false' ? false : true;
    this.product.isPromotion = this.formAdd.controls.isPromotion.value === 'false' ? false : true;
    this.product.productStatusId = Number(this.formAdd.controls.productStatus.value);
    this.product.productTypeId = Number(this.formAdd.controls.productType.value);
    if (this.product.isPromotion) {
      this.product.valuePromotion = this.formAdd.controls.valuePromotion.value;
    }
    
    formData.append('product', JSON.stringify(this.product));
    if(this.files.length > 0) {
      this.files.forEach(f => {
        formData.append('file', f.file, f.file.name);
    });
  }
    this.productService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/product']);
    });
  }

  onCancel() {
    this.router.navigate(['partner-area/product']);
  }
  
  onFileChange(event) {
    if (event.target.files.length > 3) {
    return this.toastr.error('Só é permitido anexar três arquivos ou menos!');
    }
    
    if (event.target.files.length > 0) {
      // this.onResetFileChange();
      this.files = [];
      for (const file of event.target.files) {
        this.files.push({ file });
      }
    }
  }

  onResetFileChange() {
    this.fileUpload.nativeElement.value = '';
}

getImage(nomeImage) {
  return environment.urlImagesProducts + nomeImage;
}

handleChange(evt) {
  if (evt.target.checked) {
    if (evt.target.id === 'promotionYes') {
      this.isPromotion = true;
      this.formAdd.controls.valuePromotion.setValidators([Validators.required, Validators.minLength(1)]);
    }
    if (evt.target.id === 'promotionNo') {
      this.isPromotion = false;
      this.formAdd.controls.valuePromotion.clearValidators();
      this.formAdd.controls.valuePromotion.updateValueAndValidity();
    }
  }
}

loadStatus() {
  const keys = Object.keys(this.productStatus).map(key => ({ id: this.productStatus[key], name: key }))
  keys.slice(keys.length / 2).forEach(item => {
    this.lstProductStatus.push(item);
  });
}

loadTypes() {
  const keys = Object.keys(this.productType).map(key => ({ id: this.productType[key], name: key }))
  keys.slice(keys.length / 2).forEach(item => {
    this.lstProductType.push(item);
  });
}

}


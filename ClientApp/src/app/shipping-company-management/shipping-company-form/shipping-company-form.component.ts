import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ShippingCompany } from 'src/app/_models/shipping company-model';
import { ShippingCompanyService } from 'src/app/_services/shipping company.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { State } from 'src/app/_models/state-model';
import { StateService } from 'src/app/_services/state.service';
import { ShippingCompanyState } from 'src/app/_models/shipping-company-state-model';

@Component({
  selector: 'app-shipping-company-form',
  templateUrl: './shipping-company-form.component.html'
})
export class ShippingCompanyFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public shippingCompany: ShippingCompany = new ShippingCompany();
  uploaded = false;
  img: any;
  lstStates: State[] = [];
  public file: any;
  @ViewChild('fileUpload') fileUpload: ElementRef;
  @ViewChild('selectAll') private selectAll: any;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private stateService: StateService,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private shippingCompanyService: ShippingCompanyService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.shippingCompany.id = Number(params.id);
        this.load();
      }
    });

    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required, Validators.maxLength(255)]],
      value: [''],
    });

    this.load();

    this.stateService.getAll().subscribe(result => {
      if (result.length > 0) {
        result.forEach(r => {
          r.isSelected = false
        });
      }
      this.lstStates = result
  });
}

  load() {
    if (this.shippingCompany.id > 0) {
      this.shippingCompanyService.getById(this.shippingCompany.id).subscribe(result => {
        this.shippingCompany = result;
        this.formAdd.controls.id.setValue(this.shippingCompany.id);
        this.formAdd.controls.name.setValue(this.shippingCompany.name);
        this.formAdd.controls.value.setValue(this.shippingCompany.value);
        this.img = environment.urlImagesLojas + this.shippingCompany.imageName;

        this.shippingCompany.shippingCompanyStates.forEach(s =>{
          let find = this.lstStates.find(x => x.id === s.stateId);
                if (find) {
                  find.isSelected = true;
                  find.taxValue = s.taxValue;
                }
        })
      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    // this.shippingCompany = new ShippingCompany(this.formAdd.value);
    if ((this.file === undefined) && (this.shippingCompany === undefined)){
      this.toastr.error('Selecione uma Foto!');
      return;
    }
    if (this.shippingCompany.id > 0) {
      let itens = this.lstStates.filter(x => x.isSelected)
      let id = this.shippingCompany.id
      if (itens.length === 0) {
        return this.toastr.error("Selecione algum estado para entrega pela transportadora!")
      }
      let lst = this.shippingCompany.shippingCompanyStates;
      this.shippingCompany.shippingCompanyStates = itens.map(function (i) {
        let s = new ShippingCompanyState();
        let f = lst.find(x => x.stateId == i.id);
        if (f) {
          s.id = f.id;
        }
        s.shippingCompanyStateId = id;
        s.stateId = i.id;
        s.taxValue = i.taxValue;
        return s;
      });
    } else {
      this.shippingCompany.name = this.formAdd.controls.name.value;
      let itens = this.lstStates.filter(x => x.isSelected)
      this.shippingCompany.shippingCompanyStates = itens.map(function (i) {
        let s = new ShippingCompanyState();
        s.stateId = i.id;
        s.taxValue = i.taxValue;
        return s;
      });

    }

    formData.append('shippingCompany', JSON.stringify(this.shippingCompany));
    formData.append('file', this.file);

    this.shippingCompanyService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/shipping-company']);
  });
  }

  onCancel() {
    this.router.navigate([`partner-area/shipping-company`]);
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      this.file = event.target.files[0];
    }
  }

  onResetFileChange() {
    this.fileUpload.nativeElement.value = '';
}

toggleAll() {
  const allEqual: boolean = this.selectAll.nativeElement.checked;
  this.lstStates.forEach(row => {
    row.isSelected = allEqual;
  });
}

}


import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Store } from '../_models/store';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StoreService } from '../_services/store.service';
import { AuthenticationService } from '../_services/authentication.service';

@Component({
    selector: 'app-store-management',
    templateUrl: './store-management.component.html'
})

export class StoreManagementComponent implements OnInit {
    public currentUser;
    form: FormGroup;
    formAddress: FormGroup;
    // formFileUpload: FormGroup;
    fileToUploadLogo: File = null;
    fileToUploadStore: File = null;

    @ViewChild('fileLogo') uploadedFileLogo: HTMLInputElement;
    @ViewChild('fileStore') uploadedFileStore: HTMLInputElement;

    uploaded = false;

    imageLogo: any;
    imageStore: any;
    public submitted = false;
    public submittedCep = false;
    public store: any;
    public fileLogo: any;
    public fileStore: any;

    constructor( private toastr: ToastrService,
                 private router: Router,
                 private formBuilder: FormBuilder,
                 private authenticationService: AuthenticationService,
                 private storeService: StoreService
                 ) {
    }

    ngOnInit() {
      this.form = this.formBuilder.group({
        name: ['', Validators.required],
        description: ['', Validators.required],
        cnpj: ['', Validators.required],
        contact: ['', Validators.required],
        phone: ['', Validators.required],
        exchangePolicy: [''],
        deliveryPolicy: [''],
        valueMinimum: ['', Validators.required],
        numberInstallmentsCard: ['', Validators.required],
        street: ['', Validators.required],
        district: ['', Validators.required],
        city: ['', Validators.required],
        number: ['', Validators.required],
        cep: ['', Validators.required],
        id: [0]
      });

      this.currentUser = this.authenticationService.getCurrentUser();
      this.storeService.get().subscribe((result) => {
          this.store = result;
          if (this.store) {
            this.imageLogo = environment.urlImagesLojas + this.store.imageName;
            this.imageStore = environment.urlImagesLojas + this.store.imageStore;
            this.load(result);
          }
});

}

load(store: any) {
  this.form.controls.name.setValue(store.name);
  this.form.controls.description.setValue(store.description);
  this.form.controls.cnpj.setValue(store.cnpj);
  this.form.controls.contact.setValue(store.contact);
  this.form.controls.phone.setValue(store.phone);
  this.form.controls.exchangePolicy.setValue(store.exchangePolicy);
  this.form.controls.deliveryPolicy.setValue(store.deliveryPolicy);
  this.form.controls.valueMinimum.setValue(store.valueMinimum);
  this.form.controls.numberInstallmentsCard.setValue(store.numberInstallmentsCard);
  this.form.controls.street.setValue(store.street);
  this.form.controls.city.setValue(store.city);
  this.form.controls.district.setValue(store.district);
  this.form.controls.cep.setValue(store.cep);
  this.form.controls.number.setValue(store.number);
  this.form.controls.id.setValue(store.id);
}

get f() { return this.form.controls; }

onFileLogoChange(event) {
    if (event.target.files.length > 0) {
      this.fileLogo = event.target.files[0];
    }
  }

  onFileStoreChange(event) {
    if (event.target.files.length > 0) {
      this.fileStore = event.target.files[0];
    }
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
        return;
    }

    const formData = new FormData();
    this.store = new Store(this.form.value);
    if ((this.fileLogo === undefined) && (this.imageLogo === undefined)){
      this.toastr.error('Selecione uma logomarca!');
      return;
    }
    formData.append('store', JSON.stringify(this.store));
    formData.append('fileLogo', this.fileLogo);
    formData.append('fileStore', this.fileStore);

    this.storeService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
  });
}



}

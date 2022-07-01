import { Component, OnInit, Injector, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Store } from '../_models/store';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { StoreService } from '../_services/store.service';
import { AuthenticationService } from '../_services/authentication.service';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
    selector: 'app-store-management',
    templateUrl: './store-management.component.html'
})

export class StoreManagementComponent implements OnInit {
    public currentUser;
    form: FormGroup;
    formAddress: FormGroup;
    public submitted = false;
    public submittedCep = false;
    public store: any;
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
        freeShipping: [''],
        offPix: [''],
        keyPix: [''],
        warranty: [''],
        valueMinimum: ['', Validators.required],
        numberInstallmentsCard: ['', Validators.required],
        street: ['', Validators.required],
        district: ['', Validators.required],
        city: ['', Validators.required],
        number: ['', Validators.required],
        postalCode: ['', Validators.required],
        id: [0]
      });

      this.currentUser = this.authenticationService.getCurrentUser();
      this.storeService.get().subscribe((result) => {
          this.store = result;
          if (this.store) {
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
  this.form.controls.warranty.setValue(store.warranty);

  this.form.controls.offPix.setValue(store.offPix);
  this.form.controls.keyPix.setValue(store.keyPix);

  this.form.controls.valueMinimum.setValue(store.valueMinimum);
  this.form.controls.freeShipping.setValue(store.freeShipping);
  this.form.controls.numberInstallmentsCard.setValue(store.numberInstallmentsCard);
  this.form.controls.street.setValue(store.street);
  this.form.controls.city.setValue(store.city);
  this.form.controls.district.setValue(store.district);
  this.form.controls.postalCode.setValue(store.postalCode);
  this.form.controls.number.setValue(store.number);
  this.form.controls.id.setValue(store.id);
}

get f() { return this.form.controls; }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
        return;
    }

    this.store = new Store(this.form.value);
    

    this.storeService.save(this.store).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
  });
}



}

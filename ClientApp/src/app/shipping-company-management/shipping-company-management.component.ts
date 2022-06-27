import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { ShippingCompanyService } from 'src/app/_services/shipping company.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ShippingCompany } from '../_models/shipping company-model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-shipping-company-management',
  templateUrl: './shipping-company-management.component.html'
})

export class ShippingCompanyManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  submitted = false;
  lst = [];
  shippingCompany: any;
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private shippingCompanyService: ShippingCompanyService,
    private router: Router,
  ) {
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      name: ['']
    });
    this.onSubmit();
  }

  get f() { return this.form.controls; }

  onSubmit() {
    const filter: FilterDefaultModel = new FilterDefaultModel();
    filter.name = this.form.controls.name.value;
    this.shippingCompanyService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
        console.log()
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/shipping-company/0/0`]);
  }

  edit(obj: ShippingCompany) {
    this.router.navigate([`partner-area/shipping-company/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, shippingCompany: ShippingCompany) {
    this.shippingCompany = shippingCompany;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.shippingCompanyService.deleteById(this.shippingCompany.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.shippingCompany);
      if (index !== -1) {
        this.lst.splice(index, 1);
      }
      this.closeDelete();
      this.toastr.success('Excluído com sucesso!', '');
    });
  }

  closeDelete() {
  this.modalDelete.hide();
  }

  getImage(nomeImage) {
    return environment.urlImagesLojas + nomeImage;
}


onActive(item) {
  this.shippingCompanyService.active(item).subscribe(result => {
    this.onSubmit();
  });
}

}

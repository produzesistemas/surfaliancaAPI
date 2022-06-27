import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { LogoService } from 'src/app/_services/logo.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Logo } from '../_models/logo-model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-logo-management',
  templateUrl: './logo-management.component.html'
})

export class LogoManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  submitted = false;
  lst = [];
  logo: any;
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private logoService: LogoService,
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
    this.logoService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/logo/0/0`]);
  }

  edit(obj: Logo) {
    this.router.navigate([`partner-area/logo/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, logo: Logo) {
    this.logo = logo;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.logoService.deleteById(this.logo.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.logo);
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
  this.logoService.active(item).subscribe(result => {
    this.onSubmit();
  });
}

}

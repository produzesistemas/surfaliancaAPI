import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { FinSystem } from '../_models/fin-system-model';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { LaminationService } from 'src/app/_services/lamination.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Lamination } from '../_models/lamination-model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-lamination-management',
  templateUrl: './lamination-management.component.html'
})

export class LaminationManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  loading = false;
  submitted = false;
  lst = [];
  lamination: any;
  @Output() action = new EventEmitter();
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private authenticationService: AuthenticationService,
    private laminationService: LaminationService,
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
    this.laminationService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/lamination/0/0`]);
  }

  edit(obj: FinSystem) {
    this.router.navigate([`partner-area/lamination/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, lamination: Lamination) {
    this.lamination = lamination;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.laminationService.deleteById(this.lamination.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.lamination);
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

  onActive(item) {
    this.laminationService.active(item).subscribe(result => {
      this.onSubmit();
    });
  }




}

import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { Stringer } from '../_models/stringer-model';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { StringerService } from 'src/app/_services/stringer.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-stringer-management',
  templateUrl: './stringer-management.component.html'
})

export class StringerManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  loading = false;
  submitted = false;
  lst = [];
  stringer: any;
  @Output() action = new EventEmitter();
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private authenticationService: AuthenticationService,
    private stringerService: StringerService,
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
    this.stringerService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/stringer/0/0`]);
  }

  edit(obj: Stringer) {
    this.router.navigate([`partner-area/stringer/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, stringer: Stringer) {
    this.stringer = stringer;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.stringerService.deleteById(this.stringer.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.stringer);
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
    this.stringerService.active(item).subscribe(result => {
      this.onSubmit();
    });
  }



}

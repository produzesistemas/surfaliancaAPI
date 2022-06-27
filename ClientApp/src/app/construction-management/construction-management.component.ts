import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { FinSystem } from '../_models/fin-system-model';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { ConstructionService } from 'src/app/_services/construction.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Construction } from '../_models/construction-model';

@Component({
  selector: 'app-construction-management',
  templateUrl: './construction-management.component.html'
})

export class ConstructionManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  loading = false;
  submitted = false;
  lst = [];
  construction: any;
  @Output() action = new EventEmitter();
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private authenticationService: AuthenticationService,
    private constructionService: ConstructionService,
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
    this.constructionService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/construction/0/0`]);
  }

  edit(obj: FinSystem) {
    this.router.navigate([`partner-area/construction/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, construction: Construction) {
    this.construction = construction;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.constructionService.deleteById(this.construction.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.construction);
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
    this.constructionService.active(item).subscribe(result => {
      this.onSubmit();
    });
  }




}

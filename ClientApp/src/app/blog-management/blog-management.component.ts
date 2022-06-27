import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { FinSystem } from '../_models/fin-system-model';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { BlogService } from 'src/app/_services/blog.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Blog } from '../_models/blog-model';

@Component({
  selector: 'app-blog-management',
  templateUrl: './blog-management.component.html'
})

export class BlogManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  loading = false;
  submitted = false;
  lst = [];
  blog: any;
  @Output() action = new EventEmitter();
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private authenticationService: AuthenticationService,
    private blogService: BlogService,
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
    this.blogService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/blog/0/0`]);
  }

  edit(obj: FinSystem) {
    this.router.navigate([`partner-area/blog/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, blog: Blog) {
    this.blog = blog;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.blogService.deleteById(this.blog.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.blog);
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



}

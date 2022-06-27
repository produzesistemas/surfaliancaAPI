import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { ColorService } from 'src/app/_services/color.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Color } from '../_models/color-model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-color-management',
  templateUrl: './color-management.component.html'
})

export class ColorManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  submitted = false;
  lst = [];
  color: any;
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private colorService: ColorService,
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
    this.colorService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/color/0/0`]);
  }

  edit(obj: Color) {
    this.router.navigate([`partner-area/color/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, color: Color) {
    this.color = color;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.colorService.deleteById(this.color.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.color);
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
  this.colorService.active(item).subscribe(result => {
    this.onSubmit();
  });
}

}

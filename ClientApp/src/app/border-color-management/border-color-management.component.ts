import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { BorderColorService } from 'src/app/_services/border-color.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BorderColor } from '../_models/border-color-model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-border-color-management',
  templateUrl: './border-color-management.component.html'
})

export class BorderColorManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  submitted = false;
  lst = [];
  bordercolor: any;
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private borderColorService: BorderColorService,
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
    this.borderColorService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/border-color/0/0`]);
  }

  edit(obj: BorderColor) {
    this.router.navigate([`partner-area/border-color/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, bordercolor: BorderColor) {
    this.bordercolor = bordercolor;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.borderColorService.deleteById(this.bordercolor.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.bordercolor);
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
  this.borderColorService.active(item).subscribe(result => {
    this.onSubmit();
  });
}

}

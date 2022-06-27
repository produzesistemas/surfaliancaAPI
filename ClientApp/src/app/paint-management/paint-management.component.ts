import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { PaintService } from 'src/app/_services/paint.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Paint } from '../_models/paint-model';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-paint-management',
  templateUrl: './paint-management.component.html'
})

export class PaintManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  submitted = false;
  lst = [];
  paint: any;
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private paintService: PaintService,
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
    this.paintService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/paint/0/0`]);
  }

  edit(obj: Paint) {
    this.router.navigate([`partner-area/paint/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, paint: Paint) {
    this.paint = paint;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.paintService.deleteById(this.paint.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.paint);
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
  this.paintService.active(item).subscribe(result => {
    this.onSubmit();
  });
}

}

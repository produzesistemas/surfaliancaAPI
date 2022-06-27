import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { BoardModel } from '../_models/board-model-model';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { BoardModelService } from 'src/app/_services/board-model.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-board-model-management',
  templateUrl: './board-model-management.component.html'
})

export class BoardModelManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  submitted = false;
  lst = [];
  boardModel: any;
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private boardModelService: BoardModelService,
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
    this.boardModelService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/board-model/0/0`]);
  }

  onEdit(obj: BoardModel) {
    this.router.navigate([`partner-area/board-model/${obj.id}/1`]);
  }

  onView(obj: BoardModel) {
    this.router.navigate([`partner-area/board-model/${obj.id}/0`]);
  }

  deleteById(template: TemplateRef<any>, boardModel: BoardModel) {
    this.boardModel = boardModel;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.boardModelService.deleteById(this.boardModel.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.boardModel);
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
  this.boardModelService.active(item).subscribe(result => {
    this.onSubmit();
  });
}


}


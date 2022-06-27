import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { BoardType } from '../_models/board-type-model';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { BoardTypeService } from 'src/app/_services/board-type.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-board-type-management',
  templateUrl: './board-type-management.component.html'
})

export class BoardTypeManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  submitted = false;
  lst = [];
  boardType: any;
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private boardTypeService: BoardTypeService,
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
    this.boardTypeService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/board-type/0/0`]);
  }

  edit(obj: BoardType) {
    this.router.navigate([`partner-area/board-type/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, boardType: BoardType) {
    this.boardType = boardType;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.boardTypeService.deleteById(this.boardType.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.boardType);
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


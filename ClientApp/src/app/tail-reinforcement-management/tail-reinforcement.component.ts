import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { FinSystem } from '../_models/fin-system-model';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TailReinforcement } from '../_models/tail-reinforcement-model';
import { TailReinforcementService } from '../_services/tail-reinforcement.service';

@Component({
  selector: 'app-tail-reinforcement-management',
  templateUrl: './tail-reinforcement.component.html'
})

export class TailReinforcementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  loading = false;
  submitted = false;
  lst = [];
  tail: any;
  @Output() action = new EventEmitter();
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private tailReinforcementService: TailReinforcementService,
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
    this.tailReinforcementService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/tail-reinforcement/0/0`]);
  }

  edit(obj: FinSystem) {
    this.router.navigate([`partner-area/tail-reinforcement/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, tail: TailReinforcement) {
    this.tail = tail;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.tailReinforcementService.deleteById(this.tail.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.tail);
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
    this.tailReinforcementService.active(item).subscribe(result => {
      this.onSubmit();
    });
  }




}

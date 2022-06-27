import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import * as moment from 'moment';
import { environment } from '../../environments/environment';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { Team } from '../_models/team-model';
import { TeamService } from 'src/app/_services/team.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-team-management',
  templateUrl: './team-management.component.html'
})

export class TeamManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  formAdd: FormGroup;
  loading = false;
  submitted = false;
  lst = [];
  isNew = false;
  team: any;
  @Output() action = new EventEmitter();
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private authenticationService: AuthenticationService,
    private teamService: TeamService,
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
    this.teamService.getByFilter(filter).subscribe(
      result => {
        this.lst = result;
      }
    );
  }

  onNew() {
    this.router.navigate([`partner-area/team/0/0`]);
  }

  onEdit(obj: Team) {
    this.router.navigate([`partner-area/team/${obj.id}/1`]);
  }

  onDeleteById(template: TemplateRef<any>, team: Team) {
    this.team = team;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  onConfirmDelete() {
    this.teamService.deleteById(this.team.id).subscribe(() => {
      this.onCloseDelete();
      this.toastr.success('Excluído com sucesso', '');
      this.onSubmit();
    });
  }

  onCloseDelete() {
  this.modalDelete.hide();
  }



}

import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Tail } from '../_models/tail-model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TailService } from '../_services/tail.service';
import { AuthenticationService } from '../_services/authentication.service';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
    selector: 'app-tail-management',
    templateUrl: './tail-management.component.html'
})

export class TailManagementComponent implements OnInit {
    modalRef: BsModalRef;
    modalDelete: BsModalRef;
    form: FormGroup;
    loading = false;
    submitted = false;
    lst = [];
    tail: any;
    page = 1;
    pageSize = 5;

    constructor( private toastr: ToastrService,
                 private router: Router,
                 private formBuilder: FormBuilder,
                 private authenticationService: AuthenticationService,
                 private tailService: TailService,
                 private modalService: BsModalService,
                 ) {
    }

    ngOnInit() {
        this.form = this.formBuilder.group({
            name: ['']
          });
          this.onSubmit();
      };

      get f() { return this.form.controls; }

      onSubmit() {
        const filter: FilterDefaultModel = new FilterDefaultModel();
        filter.name = this.form.controls.name.value;
        this.tailService.getByFilter(filter).subscribe(
          data => {
            this.lst = data;
          }
        );
      }

      onNew() {
        this.router.navigate([`partner-area/tail/0/0`]);
      }

      edit(obj: Tail) {
        this.router.navigate([`partner-area/tail/${obj.id}/1`]);
      }

      deleteById(template: TemplateRef<any>, tail: Tail) {
        this.tail = tail;
        this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
      }

      confirmDelete() {
        this.tailService.deleteById(this.tail.id).subscribe(() => {
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

      getImage(nomeImage) {
        return environment.urlImagesLojas + nomeImage;
    }
    onActive(item) {
      this.tailService.active(item).subscribe(result => {
        this.onSubmit();
      });
    }

}

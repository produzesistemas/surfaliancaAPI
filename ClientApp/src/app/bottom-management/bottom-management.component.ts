import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Bottom } from '../_models/bottom-model';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BottomService } from '../_services/bottom.service';
import { AuthenticationService } from '../_services/authentication.service';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
    selector: 'app-bottom-management',
    templateUrl: './bottom-management.component.html'
})

export class BottomManagementComponent implements OnInit {
    modalRef: BsModalRef;
    modalDelete: BsModalRef;
    form: FormGroup;
    loading = false;
    submitted = false;
    lst = [];
    bottom: any;
    page = 1;
    pageSize = 5;
  
    constructor( private toastr: ToastrService,
                 private router: Router,
                 private formBuilder: FormBuilder,
                 private authenticationService: AuthenticationService,
                 private bottomService: BottomService,
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
        this.bottomService.getByFilter(filter).subscribe(
          data => {
            this.lst = data;
          }
        );
      }

      onNew() {
        this.router.navigate([`partner-area/bottom/0/0`]);
      }
    
      edit(obj: Bottom) {
        this.router.navigate([`partner-area/bottom/${obj.id}/1`]);
      }
    
      deleteById(template: TemplateRef<any>, bottom: Bottom) {
        this.bottom = bottom;
        this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
      }
    
      confirmDelete() {
        this.bottomService.deleteById(this.bottom.id).subscribe(() => {
          const index: number = this.lst.indexOf(this.bottom);
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
      this.bottomService.active(item).subscribe(result => {
        this.onSubmit();
      });
    }
    
}

import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { Product } from '../_models/product-model';
import { ProductService } from 'src/app/_services/product.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { forkJoin } from 'rxjs';
import { FilterDefaultModel } from '../_models/filter-default-model';

@Component({
  selector: 'app-product-management',
  templateUrl: './product-management.component.html'
})

export class ProductManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  loading = false;
  submitted = false;
  lst = [];
  product: any;
  page = 1;
  pageSize = 5;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private authenticationService: AuthenticationService,
    private productService: ProductService,
    private router: Router
  ) {
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      name: ['']
    });
    this.onSubmit();
  }

  get f() { return this.form.controls; }
  getImage(nomeImage) {
    return environment.urlImagesProducts + nomeImage;
}

  onSubmit() {
    const filter: FilterDefaultModel = new FilterDefaultModel();
    if (this.form.controls.name) { filter.name = this.form.controls.name.value;}
    this.productService.getByFilter(filter).subscribe(result => {
    this.lst = result;
  });
  }

  onNew() {
    this.router.navigate([`partner-area/product/0`]);
  }

  edit(obj: Product) {
    this.router.navigate([`partner-area/product/${obj.id}`]);
  }

  deleteById(template: TemplateRef<any>, item: Product) {
    this.product = item;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.productService.deleteById(this.product.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.product);
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
    this.productService.active(item).subscribe(result => {
      this.onSubmit();
    });
  }

}

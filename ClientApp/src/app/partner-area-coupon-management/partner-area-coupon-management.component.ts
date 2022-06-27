import { Component, OnInit, TemplateRef, Output, EventEmitter } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { Coupon } from 'src/app/_models/coupon-model';
import { CouponService } from 'src/app/_services/coupon.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { FilterDefaultModel } from '../_models/filter-default-model';

@Component({
  selector: 'app-partner-area-coupon',
  templateUrl: './partner-area-coupon-management.component.html'
})

export class PartnerAreaCouponComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  form: FormGroup;
  loading = false;
  submitted = false;
//   lstCategorias = [];
  lst = [];
  coupon: any;
  @Output() action = new EventEmitter();
  page = 1;
  pageSize = 10;

  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private couponService: CouponService,
    private router: Router
  ) {
  }

  ngOnInit() {
    this.form = this.formBuilder.group({
      descricao: ['']
    });
    const filter: FilterDefaultModel = new FilterDefaultModel();
      this.couponService.getByFilter(filter)
    .subscribe(result => {
      this.lst = result;
    });
  }

  get f() { return this.form.controls; }

  onSubmit() {
    const filter: FilterDefaultModel = new FilterDefaultModel();
    if (this.form.controls.descricao.value) {
      filter.name = this.form.controls.descricao.value;
    }
    this.couponService.getByFilter(filter).subscribe(
      data => {
        this.lst = data;
      }
    );
  }

  onNew() {
    this.router.navigate([`/partner-area/coupon/0/0`]);
  }

  edit(obj: Coupon) {
    this.router.navigate([`/partner-area/coupon/${obj.id}/1`]);
  }

  deleteById(template: TemplateRef<any>, item: Coupon) {
    this.coupon = item;
    this.modalDelete = this.modalService.show(template, { class: 'modal-md' });
  }

  confirmDelete() {
    this.couponService.deleteById(this.coupon.id).subscribe(() => {
      const index: number = this.lst.indexOf(this.coupon);
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
    this.couponService.active(item).subscribe(result => {
      this.onSubmit();
    });
  }
}

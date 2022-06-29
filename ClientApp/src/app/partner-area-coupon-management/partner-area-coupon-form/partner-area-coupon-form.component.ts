import { Component, OnInit, Injector, ViewChild, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router, ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CouponService } from '../../_services/coupon.service';
import { Coupon } from '../../_models/coupon-model';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { NgbDate } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-partner-area-coupon-form',
  templateUrl: './partner-area-coupon-form.component.html'
})

export class PartnerAreaCouponFormComponent implements OnInit {
  public currentUser;
  form: FormGroup;
  public submitted = false;
  public coupon: Coupon = new Coupon();
  public lstTipos = [];
  public lstClientes = [];
  public isGeral = false;

  constructor(private toastr: ToastrService,
    private router: Router,
    private formBuilder: FormBuilder,
    private authenticationService: AuthenticationService,
    private route: ActivatedRoute,
    private couponService: CouponService) {
  }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.coupon.id = Number(params.id);
      }
    });

    this.form = this.formBuilder.group({
      code: ['', Validators.required],
      description: ['', Validators.required],
      quantity: ['', Validators.required],
      type: ['', Validators.required],
      general: ['true', Validators.required],
      value: ['', Validators.required],
      valueMinimum: ['', Validators.required],
      initialDate: ['', Validators.required],
      finalDate: ['', Validators.required],
      client: ['']
    });

    // this.loadCategorias();
    this.lstTipos = [];
    this.lstTipos.push({ value: 'false', label: "Porcentagem" })
    this.lstTipos.push({ value: 'true', label: "Valor" })

    this.authenticationService.getClientsStore().subscribe(result => {
      if (result !== undefined) {
        this.lstClientes = result;
        this.loadForm();
      }
    });


  }

  loadForm() {
    if (this.coupon.id !== undefined) {
      this.couponService.get(this.coupon.id).subscribe(coupon => {
        if (coupon !== undefined) {
          this.loadObject(coupon);
        }
      });
    }
  }

  get f() { return this.form.controls; }

  loadObject(item) {
    this.form.controls.description.setValue(item.description);
    this.form.controls.code.setValue(item.code);
    this.form.controls.value.setValue(item.value);
    this.form.controls.quantity.setValue(item.quantity);
    this.form.controls.type.setValue(item.type.toString());
    this.form.controls.general.setValue(item.general.toString());
    if (!item.general) {
      let cliente = this.lstClientes.find(x => x.id === item.clientId);
      this.form.controls.client.setValue(cliente.id);
      this.isGeral = true;
      this.form.controls.client.setValidators([Validators.required]);
    }
    this.form.controls.valueMinimum.setValue(item.valueMinimum);
    let dt = new Date(item.initialDate);
    let ngbDate = new NgbDate(dt.getFullYear(), dt.getMonth() + 1, dt.getDate());
    this.form.controls.initialDate.setValue(ngbDate);
    dt = new Date(item.finalDate);
    ngbDate = new NgbDate(dt.getFullYear(), dt.getMonth() + 1, dt.getDate());
    this.form.controls.finalDate.setValue(ngbDate);
  }

  onCancel() {
    this.router.navigate([`/partner-area/coupon`]);
  }

  onSave() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.coupon.code = this.form.controls.code.value;
    this.coupon.description = this.form.controls.description.value;
    this.coupon.initialDate = new Date(this.form.controls.initialDate.value.year,
      this.form.controls.initialDate.value.month - 1,
      this.form.controls.initialDate.value.day, 0, 0, 0, 0);
    this.coupon.finalDate = new Date(this.form.controls.finalDate.value.year,
      this.form.controls.finalDate.value.month - 1,
      this.form.controls.finalDate.value.day, 0, 0, 0, 0);
    this.coupon.quantity = Number(this.form.controls.quantity.value);
    this.coupon.type = this.form.controls.type.value === 'false' ? false : true;
    this.coupon.value = this.form.controls.value.value;
    this.coupon.valueMinimum = this.form.controls.valueMinimum.value;
    this.coupon.general = this.form.controls.general.value === 'false' ? false : true;

    if (!this.coupon.general) {
      this.coupon.clientId = this.form.controls.client.value;
    }

    this.couponService.save(this.coupon).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['/partner-area/coupon']);
    });


  }

  handleChange(evt) {
    if (evt.target.checked) {
      if (evt.target.id === 'geralYes') {
        this.isGeral = false;
        this.form.controls.client.clearValidators();
        this.form.controls.client.updateValueAndValidity();
      }
      if (evt.target.id === 'geralNo') {
        this.isGeral = true;
        this.form.controls.client.setValidators([Validators.required]);
      }
    }
  }


}


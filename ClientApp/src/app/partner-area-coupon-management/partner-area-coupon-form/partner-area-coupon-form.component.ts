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
      codigo: ['', Validators.required],
      descricao: ['', Validators.required],
      quantidade: ['', Validators.required],
      tipo: ['', Validators.required],
      geral: ['true', Validators.required],
      valor: ['', Validators.required],
      valorMinimo: ['', Validators.required],
      dataInicial: ['', Validators.required],
      dataFinal: ['', Validators.required],
      cliente: ['']
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
    this.form.controls.descricao.setValue(item.descricao);
    this.form.controls.codigo.setValue(item.codigo);
    this.form.controls.valor.setValue(item.valor);
    this.form.controls.quantidade.setValue(item.quantidade);
    this.form.controls.tipo.setValue(item.tipo.toString());
    this.form.controls.geral.setValue(item.geral.toString());
    if (!item.geral) {
      let cliente = this.lstClientes.find(x => x.id === item.clienteId);
      this.form.controls.cliente.setValue(cliente.id);
      this.isGeral = true;
      this.form.controls.cliente.setValidators([Validators.required]);
    }
    this.form.controls.valorMinimo.setValue(item.valorMinimo);
    let dt = new Date(item.initialDate);
    let ngbDate = new NgbDate(dt.getFullYear(), dt.getMonth() + 1, dt.getDate());
    this.form.controls.dataInicial.setValue(ngbDate);
    dt = new Date(item.finalDate);
    ngbDate = new NgbDate(dt.getFullYear(), dt.getMonth() + 1, dt.getDate());
    this.form.controls.dataFinal.setValue(ngbDate);
  }

  onCancel() {
    this.router.navigate([`/partner-area/coupon`]);
  }

  onSave() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }
    this.coupon.codigo = this.form.controls.codigo.value;
    this.coupon.descricao = this.form.controls.descricao.value;
    this.coupon.initialDate = new Date(this.form.controls.dataInicial.value.year,
      this.form.controls.dataInicial.value.month - 1,
      this.form.controls.dataInicial.value.day, 0, 0, 0, 0);
    this.coupon.finalDate = new Date(this.form.controls.dataFinal.value.year,
      this.form.controls.dataFinal.value.month - 1,
      this.form.controls.dataFinal.value.day, 0, 0, 0, 0);
    this.coupon.quantidade = Number(this.form.controls.quantidade.value);
    this.coupon.tipo = this.form.controls.tipo.value === 'false' ? false : true;
    this.coupon.value = this.form.controls.valor.value;
    this.coupon.valorMinimo = this.form.controls.valorMinimo.value;
    this.coupon.geral = this.form.controls.geral.value === 'false' ? false : true;

    if (!this.coupon.geral) {
      this.coupon.clienteId = this.form.controls.cliente.value;
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
        this.form.controls.cliente.clearValidators();
        this.form.controls.cliente.updateValueAndValidity();
      }
      if (evt.target.id === 'geralNo') {
        this.isGeral = true;
        this.form.controls.cliente.setValidators([Validators.required]);
      }
    }
  }


}


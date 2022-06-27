import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { StoreService } from '../_services/store.service';
import {Location} from '@angular/common';


@Component({
    selector: 'app-contato-management',
    templateUrl: './contato-management.component.html'
})

export class ContatoManagementComponent implements OnInit {
    form: FormGroup;
    submitted = false;
    constructor(private toastr: ToastrService,
        private storeService: StoreService,
        private formBuilder: FormBuilder,
        private _location: Location,
        private router: Router) {
    }
    get f() { return this.form.controls; }

    ngOnInit() {
        this.form = this.formBuilder.group({
            nomeCompleto: ['', Validators.required],
            email: ['', Validators.required],
            assunto: ['', Validators.required],
            mensagem: ['', Validators.required]
        });
  }

  onSubmit() {
    this.submitted = true;
    if (this.form.invalid) {
      return;
    }

    let msg: any = {};
    msg.assunto = this.form.controls.assunto.value;
    msg.nomeCompleto = this.form.controls.nomeCompleto.value;
    msg.email = this.form.controls.email.value;
    msg.mensagem = this.form.controls.mensagem.value;
    this.storeService.sendMessage(msg).subscribe(result => {
            this.toastr.success("Mensagem enviada com sucesso!")
            return this.router.navigate(['/index']);
    })


  }

  onBack() {
    this._location.back();
  }

}


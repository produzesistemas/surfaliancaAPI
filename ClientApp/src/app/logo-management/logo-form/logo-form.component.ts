import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Logo } from 'src/app/_models/logo-model';
import { LogoService } from 'src/app/_services/logo.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-logo-form',
  templateUrl: './logo-form.component.html'
})
export class LogoFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public logo: Logo = new Logo();
  uploaded = false;
  img: any;
  public file: any;
  @ViewChild('fileUpload') fileUpload: ElementRef;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private logoService: LogoService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.logo.id = Number(params.id);
        this.load();
      }
    });

    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required, Validators.maxLength(255)]],
      value: [''],
    });

    this.load();

  }

  load() {
    if (this.logo.id > 0) {
      this.logoService.getById(this.logo.id).subscribe(result => {
        this.logo = result;
        this.formAdd.controls.id.setValue(this.logo.id);
        this.formAdd.controls.name.setValue(this.logo.name);
        this.formAdd.controls.value.setValue(this.logo.value);
        this.img = environment.urlImagesLojas + this.logo.imageName;
      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    this.logo = new Logo(this.formAdd.value);
    if ((this.file === undefined) && (this.logo === undefined)){
      this.toastr.error('Selecione uma Foto!');
      return;
    }
    formData.append('logo', JSON.stringify(this.logo));
    formData.append('file', this.file);
    this.logoService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/logo']);
  });
  }

  onCancel() {
    this.router.navigate([`partner-area/logo`]);
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      this.file = event.target.files[0];
    }
  }

  onResetFileChange() {
    this.fileUpload.nativeElement.value = '';
}

}


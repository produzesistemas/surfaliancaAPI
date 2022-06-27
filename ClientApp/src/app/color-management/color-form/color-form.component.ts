import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Color } from 'src/app/_models/color-model';
import { ColorService } from 'src/app/_services/color.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-color-form',
  templateUrl: './color-form.component.html'
})
export class ColorFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public color: Color = new Color();
  uploaded = false;
  logo: any;
  public file: any;
  @ViewChild('fileUpload') fileUpload: ElementRef;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private colorService: ColorService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.color.id = Number(params.id);
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
    if (this.color.id > 0) {
      this.colorService.getById(this.color.id).subscribe(result => {
        this.color = result;
        this.formAdd.controls.id.setValue(this.color.id);
        this.formAdd.controls.name.setValue(this.color.name);
        this.formAdd.controls.value.setValue(this.color.value);
        this.logo = environment.urlImagesLojas + this.color.imageName;
      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    this.color = new Color(this.formAdd.value);
    if ((this.file === undefined) && (this.logo === undefined)){
      this.toastr.error('Selecione uma Foto!');
      return;
    }
    formData.append('color', JSON.stringify(this.color));
    formData.append('file', this.file);
    this.colorService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/color']);
  });
  }

  onCancel() {
    this.router.navigate([`partner-area/color`]);
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


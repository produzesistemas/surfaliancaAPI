import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BorderColor } from 'src/app/_models/border-color-model';
import { BorderColorService } from 'src/app/_services/border-color.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-border-color-form',
  templateUrl: './border-color-form.component.html'
})
export class BorderColorFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public bordercolor: BorderColor = new BorderColor();
  uploaded = false;
  logo: any;
  public file: any;
  @ViewChild('fileUpload') fileUpload: ElementRef;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private borderColorService: BorderColorService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.bordercolor.id = Number(params.id);
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
    if (this.bordercolor.id > 0) {
      this.borderColorService.getById(this.bordercolor.id).subscribe(result => {
        this.bordercolor = result;
        this.formAdd.controls.id.setValue(this.bordercolor.id);
        this.formAdd.controls.name.setValue(this.bordercolor.name);
        this.formAdd.controls.value.setValue(this.bordercolor.value);
        this.logo = environment.urlImagesLojas + this.bordercolor.imageName;
      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    this.bordercolor = new BorderColor(this.formAdd.value);
    if ((this.file === undefined) && (this.logo === undefined)){
      this.toastr.error('Selecione uma Foto!');
      return;
    }
    formData.append('borderColor', JSON.stringify(this.bordercolor));
    formData.append('file', this.file);
    this.borderColorService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/border-color']);
  });
  }

  onCancel() {
    this.router.navigate([`partner-area/border-color`]);
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


import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Bottom } from 'src/app/_models/bottom-model';
import { BottomService } from 'src/app/_services/bottom.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-bottom-form',
  templateUrl: './bottom-form.component.html'
})
export class BottomFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public bottom: Bottom = new Bottom();
  formFileUpload: FormGroup;
  fileToUpload: File = null;
  @ViewChild('fileUpload') fileUpload: ElementRef;

  uploaded = false;
  logo: any;
  public file: any;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private bottomService: BottomService
  ) { }

  get f() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.bottom.id = Number(params.id);
        this.load();
      }
    });

    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required]],
    });

    this.load();

  }

  load() {
    if (this.bottom.id > 0) {
      this.bottomService.getById(this.bottom.id).subscribe(result => {
        this.bottom = result;
        this.formAdd.controls.id.setValue(this.bottom.id);
        this.formAdd.controls.name.setValue(this.bottom.name);
        this.logo = environment.urlImagesLojas + this.bottom.imageName;
      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    this.bottom = new Bottom(this.formAdd.value);
    if ((this.file === undefined) && (this.logo === undefined)){
      this.toastr.error('Selecione uma Foto!');
      return;
    }
    formData.append('bottom', JSON.stringify(this.bottom));
    formData.append('file', this.file);
    this.bottomService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/bottom']);
  });
  }

  onCancel() {
    this.router.navigate([`partner-area/bottom`]);
  }

  onResetFileChange() {
    this.fileUpload.nativeElement.value = '';
}

  onFileChange(event) {
    const extension = event.target.files[0].name.split('.');
    if ((extension[1] !== 'jpg') && (extension[1] !== 'png')) {
      this.onResetFileChange();
      this.toastr.error('Só é permitido arquivos no formato JPG e PNG');
      return;
    }
    if (event.target.files.length > 0) {
      this.file = event.target.files[0];
    }
  }

}


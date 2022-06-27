import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Tail } from 'src/app/_models/tail-model';
import { TailService } from 'src/app/_services/tail.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-tail-form',
  templateUrl: './tail-form.component.html'
})
export class TailFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public tail: Tail = new Tail();
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
    private tailService: TailService
  ) { }

  get f() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.tail.id = Number(params.id);
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
    if (this.tail.id > 0) {
      this.tailService.getById(this.tail.id).subscribe(result => {
        this.tail = result;
        this.formAdd.controls.id.setValue(this.tail.id);
        this.formAdd.controls.name.setValue(this.tail.name);
        this.logo = environment.urlImagesLojas + this.tail.imageName;
      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    this.tail = new Tail(this.formAdd.value);
    if ((this.file === undefined) && (this.logo === undefined)){
      this.toastr.error('Selecione uma Foto!');
      return;
    }
    formData.append('tail', JSON.stringify(this.tail));
    formData.append('file', this.file);
    this.tailService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/tail']);
  });
  }

  onCancel() {
    this.router.navigate([`partner-area/tail`]);
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


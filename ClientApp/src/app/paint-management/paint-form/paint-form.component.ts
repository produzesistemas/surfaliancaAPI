import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Paint } from 'src/app/_models/paint-model';
import { PaintService } from 'src/app/_services/paint.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { BoardModelService } from 'src/app/_services/board-model.service';
import { BoardModel } from 'src/app/_models/board-model-model';

@Component({
  selector: 'app-paint-form',
  templateUrl: './paint-form.component.html'
})
export class PaintFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public paint: Paint = new Paint();
  uploaded = false;
  logo: any;
  boardModels = [];
  public file: any;
  @ViewChild('fileUpload') fileUpload: ElementRef;
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private boardModelService: BoardModelService,
    private paintService: PaintService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.paint.id = Number(params.id);
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
    if (this.paint.id > 0) {
      this.paintService.getById(this.paint.id).subscribe(result => {
        this.paint = result;
        this.formAdd.controls.id.setValue(this.paint.id);
        this.formAdd.controls.name.setValue(this.paint.name);
        this.formAdd.controls.value.setValue(this.paint.value);
        this.logo = environment.urlImagesPaint + this.paint.imageName;
      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    this.paint = new Paint(this.formAdd.value);
    if ((this.file === undefined) && (this.logo === undefined)){
      this.toastr.error('Selecione uma Foto!');
      return;
    }
    formData.append('paint', JSON.stringify(this.paint));
    formData.append('file', this.file);
    this.paintService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/paint']);
  });
  }

  onCancel() {
    this.router.navigate([`partner-area/paint`]);
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


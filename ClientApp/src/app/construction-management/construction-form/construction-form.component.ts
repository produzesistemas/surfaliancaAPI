import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Construction } from 'src/app/_models/construction-model';
import { ConstructionService } from 'src/app/_services/construction.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-construction-form',
  templateUrl: './construction-form.component.html'
})
export class ConstructionFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public construction: Construction = new Construction();
  configEditor: AngularEditorConfig = {
    editable: true,
    spellcheck: true,
    height: '15rem',
    minHeight: '5rem',
    placeholder: '',
    translate: 'no',
    defaultParagraphSeparator: '',
    defaultFontName: 'Arial',
    toolbarHiddenButtons: [
      [],
      ['toggleEditorMode', 'removeFormat']
    ]
  };
  public files: any = [];

  public isEdit = false;
  public isView = false;
  public isNew = false;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private constructionService: ConstructionService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.construction.id = Number(params.id);
        this.load();
      }
    });

    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required, Validators.maxLength(255)]],
      details: [''],
      urlMovie: [''],
      value: [''],
    });

    this.load();

  }

  load() {
    if (this.construction.id > 0) {
      this.constructionService.getById(this.construction.id).subscribe(result => {
        this.construction = result;
        this.formAdd.controls.id.setValue(this.construction.id);
        this.formAdd.controls.name.setValue(this.construction.name);
        this.formAdd.controls.value.setValue(this.construction.value);
        this.formAdd.controls.details.setValue(this.construction.details);
        this.formAdd.controls.urlMovie.setValue(this.construction.urlMovie);

      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const formData = new FormData();
    const item = new Construction();
    item.id = this.construction.id;
    item.value = this.formAdd.controls.value.value;
    item.details = this.formAdd.controls.details.value;
    item.name = this.formAdd.controls.name.value;
    item.urlMovie = this.formAdd.controls.urlMovie.value;

    formData.append('construction', JSON.stringify(item));
    if(this.files.length > 0) {
      this.files.forEach(f => {
        formData.append('file', f.file, f.file.name);
    });
    }

    this.constructionService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/construction']);
    });
  }

  onCancel() {
    this.router.navigate([`partner-area/construction`]);
  }

  getImage(nomeImage) {
    return environment.urlImagesLojas + nomeImage;
  }
  
  canEdit() {
    return this.isEdit;
  }
  
  canView() {
    return this.isView;
  }
  
  canSave() {
    return this.isNew;
  }
  
  onFileChange(event) {
    if (event.target.files.length > 4) {
    return this.toastr.error('Só é permitido anexar quatro arquivos ou menos!');
    }
    
    if (event.target.files.length > 0) {
      this.onResetFileChange();
      this.files = [];
      for (const file of event.target.files) {
        this.files.push({ file });
      }
    }
  }

  onResetFileChange() {
    this.files = [];
}

}


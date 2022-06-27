import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Stringer } from 'src/app/_models/stringer-model';
import { StringerService } from 'src/app/_services/stringer.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';

@Component({
  selector: 'app-stringer-form',
  templateUrl: './stringer-form.component.html'
})
export class StringerFormComponent implements OnInit {
  formAdd: FormGroup;
  img: any;
  submitted = false;
  public stringer: Stringer = new Stringer();
  public files: any = [];
  hasSelected: any;
  colors: any;
  @ViewChild('selectAll') private selectAll: any;

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

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private stringerService: StringerService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
        this.stringer.id = Number(params.id);
    });

    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(255)]],
      details: [''],
      value: [''],
    });

    this.load();



  }

  load() {

    if (this.stringer.id > 0) {
      this.stringerService.getById(this.stringer.id).subscribe(result => {
        this.stringer = result;
        this.formAdd.controls.id.setValue(this.stringer.id);
        this.formAdd.controls.name.setValue(this.stringer.name);
        this.formAdd.controls.value.setValue(this.stringer.value);
        this.formAdd.controls.details.setValue(this.stringer.details);
      });
    }


  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }

    // const formData = new FormData();
    // const item = new Stringer();
    // item.id = this.stringer.id;
    // item.details = this.formAdd.controls.details.value;
    // item.name = this.formAdd.controls.name.value;
    // item.value = this.formAdd.controls.value.value;

    const model = new Stringer(this.formAdd.value);
    if (this.stringer.id) {
      model.id = this.stringer.id;
    }

    // formData.append('stringer', JSON.stringify(item));
    // if(this.files.length > 0) {
    //   this.files.forEach(f => {
    //     formData.append('file', f.file, f.file.name);
    // });
    // }

    this.stringerService.save(model).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/stringer']);
    });
  }

  onCancel() {
    this.router.navigate([`partner-area/stringer`]);
  }


//   onFileChange(event) {
//     if (event.target.files.length > 4) {
//     return this.toastr.error('Só é permitido anexar um arquivo!');
//     }

//     if (event.target.files.length > 0) {
//       this.onResetFileChange();
//       this.files = [];
//       for (const file of event.target.files) {
//         this.files.push({ file });
//       }
//     }
//   }

//   onResetFileChange() {
//     this.files = [];
// }








}


import { Component, OnInit, ViewChild, ElementRef, EventEmitter, Output, Input } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { Construction } from 'src/app/_models/construction-model';
import { ConstructionService } from 'src/app/_services/construction.service';
import { ToastrService } from 'ngx-toastr';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { environment } from 'src/environments/environment';
import { TailReinforcement } from 'src/app/_models/tail-reinforcement-model';
import { TailReinforcementService } from 'src/app/_services/tail-reinforcement.service';

@Component({
  selector: 'app-tail-reinforcement-form',
  templateUrl: './tail-reinforcement-form.component.html'
})
export class TailReinforcementFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public tail: TailReinforcement = new TailReinforcement();
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
    private tailReinforcementService: TailReinforcementService
  ) { }

  get q() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.tail.id = Number(params.id);
        this.load();
      }
    });

    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required, Validators.maxLength(255)]],
      details: [''],
      value: [''],
    });

    this.load();

  }

  load() {
    if (this.tail.id > 0) {
      this.tailReinforcementService.getById(this.tail.id).subscribe(result => {
        this.tail = result;
        this.formAdd.controls.id.setValue(this.tail.id);
        this.formAdd.controls.name.setValue(this.tail.name);
        this.formAdd.controls.value.setValue(this.tail.value);
        this.formAdd.controls.details.setValue(this.tail.details);
      });
    }

  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    const item = new TailReinforcement();
    item.id = this.tail.id;
    item.value = this.formAdd.controls.value.value;
    item.details = this.formAdd.controls.details.value;
    item.name = this.formAdd.controls.name.value;
    this.tailReinforcementService.save(item).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/tail-reinforcement']);
    });
  }

  onCancel() {
    this.router.navigate([`partner-area/tail-reinforcement`]);
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
  

}


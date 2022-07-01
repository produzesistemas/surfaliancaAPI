import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { BoardModel } from 'src/app/_models/board-model-model';
import { BoardModelService } from 'src/app/_services/board-model.service';
import { ToastrService } from 'ngx-toastr';
import { IDropdownSettings } from 'ng-multiselect-dropdown';
import { FilterDefaultModel } from 'src/app/_models/filter-default-model';
import { environment } from 'src/environments/environment';
import { AngularEditorConfig } from '@kolkov/angular-editor';
import { BoardModelDimensions } from 'src/app/_models/board-model-dimensions-model';
import { forkJoin } from 'rxjs';
import { BottomService } from 'src/app/_services/bottom.service';
import { ConstructionService } from 'src/app/_services/construction.service';
import { FinSystemService } from 'src/app/_services/fin-system.service';
import { LaminationService } from 'src/app/_services/lamination.service';
import { StringerService } from 'src/app/_services/stringer.service';
import { TailService } from 'src/app/_services/tail.service';
import { TailReinforcementService } from 'src/app/_services/tail-reinforcement.service';

@Component({
  selector: 'app-board-model-form',
  templateUrl: './board-model-form.component.html'
})
export class BoardModelFormComponent implements OnInit {
  formAdd: FormGroup;
  formDimension: FormGroup;
  formLstDimension: FormGroup;
  submitted = false;
  submittedDimension = false;
  public boardModel: BoardModel = new BoardModel();
  selectedItems = [];
  logos = [];
  lstdimensions = [];
  tails = [];
  constructions = [];
  finSystens = [];
  bottons = [];
  tailReforcements = [];
  stringers = [];
  laminations = [];

  dropdownSettings: IDropdownSettings = {};
  multiSettings: IDropdownSettings = {};

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

  uploaded = false;
  logo: any;
  public files: any = [];

  public isEdit = false;
  public isView = false;
  public isNew = false;

  @ViewChild('fileUpload') fileUpload: ElementRef;

  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private route: ActivatedRoute,
    private boardModelService: BoardModelService,
    private bottomService: BottomService,
    private constructionService: ConstructionService,
    private finSystemService: FinSystemService,
    private laminationService: LaminationService,
    private stringerService: StringerService,
    private tailService: TailService,
    private tailReinforcementService: TailReinforcementService
  ) { }

  get q() { return this.formAdd.controls; }
  get d() { return this.formDimension.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.boardModel.id = Number(params.id);
        if (params.isEdit === '1') {
          this.isEdit = true;
        } else {
          this.isView = true;
        }
      }
      if (params.id === '0') {
        this.isNew = true;
      }
    });

    this.formAdd = this.formBuilder.group({
      id: [0],
      name: ['', [Validators.required]],
      description: [''],
      urlMovie: [''],
      value: ['', [Validators.required]],
      daysProduction: ['', [Validators.required]],
      tails: [''],
      laminations: [''],
      constructions: [''],
      finSystens: [''],
      stringers: [''],
      bottons: [''],
      tailReforcements: [''],

    });

    this.formDimension = this.formBuilder.group({
      dimension: ['', [Validators.required]],

    });


    this.dropdownSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'name',
      selectAllText: 'Marque todos',
      unSelectAllText: 'Desmarque todos',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };
    this.multiSettings = {
      singleSelection: false,
      idField: 'id',
      textField: 'description',
      selectAllText: 'Marque todos',
      unSelectAllText: 'Desmarque todos',
      itemsShowLimit: 3,
      allowSearchFilter: true
    };

    forkJoin(
      this.constructionService.getAll(),
      this.tailService.getAll(),
      this.laminationService.getAll(),
      this.finSystemService.getAll(),
      this.stringerService.getAll(),
      this.bottomService.getAll(),
      this.tailReinforcementService.getAll()
    )    .subscribe(result => {
      this.constructions = result[0];
      this.tails = result[1];
      this.laminations = result[2];
      this.finSystens = result[3];
      this.stringers = result[4];
      this.bottons = result[5];
      this.tailReforcements = result[6];
    });
        if (this.boardModel.id > 0) {
          this.load();
        }
  }

  load() {
    this.boardModelService.getById(this.boardModel.id).subscribe(boardmodel => {
      this.boardModel = boardmodel;
      this.setControls(this.boardModel);
      this.boardModel.boardModelDimensions.forEach(bmd => {
        this.lstdimensions.push(bmd.description);
      })
    });
  }

  setControls(item: BoardModel) {
    this.formAdd.controls.id.setValue(item.id);
    this.formAdd.controls.daysProduction.setValue(item.daysProduction);
    this.formAdd.controls.description.setValue(item.description);
    this.formAdd.controls.name.setValue(item.name);
    this.formAdd.controls.urlMovie.setValue(item.urlMovie);
    this.formAdd.controls.value.setValue(item.value);


    if (this.isView) {
      this.formAdd.controls.daysProduction.disable();
      this.formAdd.controls.description.disable();
      this.formAdd.controls.name.disable();
      this.formAdd.controls.urlMovie.disable();
      this.formAdd.controls.value.disable();
    }
  }

  onSave() {
    this.submitted = true;
    if (this.formAdd.invalid) {
      return;
    }
    if (this.lstdimensions.length === 0) {
      return this.toastr.error('Coloque pelo menos uma dimensão');
    }

    const formData = new FormData();
    const item = new BoardModel();
    item.id = this.boardModel.id;
    item.value = this.formAdd.controls.value.value;
    item.description = this.formAdd.controls.description.value;
    item.name = this.formAdd.controls.name.value;
    item.daysProduction = this.formAdd.controls.daysProduction.value;
    item.urlMovie = this.formAdd.controls.urlMovie.value;


this.lstdimensions.forEach(dimension => {
  const boardmodeldimension = new BoardModelDimensions();
  boardmodeldimension.description = dimension;
item.boardModelDimensions.push(boardmodeldimension);
})

    formData.append('boardModel', JSON.stringify(item));
    if(this.files.length > 0) {
      this.files.forEach(f => {
        formData.append('file', f.file, f.file.name);
    });
    }

    this.boardModelService.save(formData).subscribe(result => {
      this.toastr.success('Registro efetuado com sucesso!');
      this.router.navigate(['partner-area/board-model']);
    });

  }

  onCancel() {
    this.router.navigate(['partner-area/board-model']);
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

onAddDimension() {
  this.submittedDimension = true;
  if (this.formDimension.invalid) {
    return;
  }
  this.lstdimensions.push(this.formDimension.controls.dimension.value);
  this.formDimension.controls.dimension.reset();
  this.submittedDimension = false;
}

deleteById(dimension: any) {
  const index: number = this.lstdimensions.indexOf(dimension);
  if (index !== -1) {
    this.lstdimensions.splice(index, 1);
  }
}



}


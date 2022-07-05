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
import { BoardModelBottom } from '../../_models/board-model-bottom-model';
import { BoardModelLamination } from 'src/app/_models/board-model-lamination-model';
import { BoardModelFinSystem } from 'src/app/_models/board-model-fin-system-model';
import { BoardModelConstruction } from 'src/app/_models/board-model-construction-model';
import { BoardModelStringer } from '../../_models/board-model-stringer-model';
import { BoardModelTail } from '../../_models/board-model-tail-model';
import { BoardModelTailReinforcement } from '../../_models/board-model-tail-reinforcement-model';

@Component({
  selector: 'app-board-model-form',
  templateUrl: './board-model-form.component.html'
})
export class BoardModelFormComponent implements OnInit {
  formAdd: FormGroup;
  submitted = false;
  public boardModel: BoardModel = new BoardModel();
  selectedItems = [];
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
      dimension: [''],
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
      if (this.boardModel.id > 0) {
        this.load();
      }
    });

  }

  load() {
    this.boardModelService.getById(this.boardModel.id).subscribe(boardmodel => {
      this.boardModel = boardmodel;
      this.logo = environment.urlImagesLojas + this.boardModel.imageName;
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
    this.formAdd.controls.constructions.setValue(this.constructions.filter(x => item.boardModelConstructions.find(z => z.constructionId === x.id)));
    this.formAdd.controls.laminations.setValue(this.laminations.filter(x => item.boardModelLaminations.find(z => z.laminationId === x.id)));

    this.formAdd.controls.stringers.setValue(this.stringers.filter(x => item.boardModelStringers.find(z => z.stringerId === x.id)));
    this.formAdd.controls.finSystens.setValue(this.finSystens.filter(x => item.boardModelFinSystems.find(z => z.finSystemId === x.id)));
    this.formAdd.controls.tails.setValue(this.tails.filter(x => item.boardModelTails.find(z => z.tailId === x.id)));
    this.formAdd.controls.bottons.setValue(this.bottons.filter(x => item.boardModelBottoms.find(z => z.bottomId === x.id)));
    this.formAdd.controls.tailReforcements.setValue(this.tailReforcements.filter(x => item.boardModelTailReinforcements.find(z => z.tailReinforcementId === x.id)));

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
    // const item = new BoardModel();
    this.boardModel.value = this.formAdd.controls.value.value;
    this.boardModel.description = this.formAdd.controls.description.value;
    this.boardModel.name = this.formAdd.controls.name.value;
    this.boardModel.daysProduction = this.formAdd.controls.daysProduction.value;
    this.boardModel.urlMovie = this.formAdd.controls.urlMovie.value;


this.lstdimensions.forEach(dimension => {
  const boardmodeldimension = new BoardModelDimensions();
  boardmodeldimension.description = dimension;
  this.boardModel.boardModelDimensions.push(boardmodeldimension);
})


if (this.formAdd.controls.laminations.value.length > 0) {
  this.formAdd.controls.laminations.value.forEach(lamination => {
    if(!this.boardModel.boardModelLaminations.find(x => x.laminationId === lamination.id)) {
      const newModel = new BoardModelLamination();
      newModel.id = 0;
      newModel.laminationId = lamination.id;
      newModel.boardModelId = this.boardModel.id;
      this.boardModel.boardModelLaminations.push(newModel);
    };
  });
}

if (this.formAdd.controls.finSystens.value.length > 0) {
  this.formAdd.controls.finSystens.value.forEach(fin => {
    if(!this.boardModel.boardModelFinSystems.find(x => x.finSystemId === fin.id)) {
      const newModel = new BoardModelFinSystem();
      newModel.id = 0;
      newModel.finSystemId = fin.id;
      newModel.boardModelId = this.boardModel.id;
      this.boardModel.boardModelFinSystems.push(newModel);
    };
  });
}

if (this.formAdd.controls.constructions.value.length > 0) {
  this.formAdd.controls.constructions.value.forEach(construction => {
    if(!this.boardModel.boardModelConstructions.find(x => x.constructionId === construction.id)) {
      const newModel = new BoardModelConstruction();
      newModel.id = 0;
      newModel.constructionId = construction.id;
      newModel.boardModelId = this.boardModel.id;
      this.boardModel.boardModelConstructions.push(newModel);
    };
  });
}

if (this.formAdd.controls.bottons.value.length > 0) {
  this.formAdd.controls.bottons.value.forEach(bottom => {
    if(!this.boardModel.boardModelBottoms.find(x => x.bottomId === bottom.id)) {
      const newModel = new BoardModelBottom();
      newModel.id = 0;
      newModel.bottomId = bottom.id;
      newModel.boardModelId = this.boardModel.id;
      this.boardModel.boardModelBottoms.push(newModel);
    };
  });
}

if (this.formAdd.controls.stringers.value.length > 0) {
  this.formAdd.controls.stringers.value.forEach(stringer => {
    if(!this.boardModel.boardModelStringers.find(x => x.stringerId === stringer.id)) {
      const newModel = new BoardModelStringer();
      newModel.id = 0;
      newModel.stringerId = stringer.id;
      newModel.boardModelId = this.boardModel.id;
      this.boardModel.boardModelStringers.push(newModel);
    };
  });
}

if (this.formAdd.controls.tails.value.length > 0) {
  this.formAdd.controls.tails.value.forEach(tail => {
    if(!this.boardModel.boardModelTails.find(x => x.tailId === tail.id)) {
      const newModel = new BoardModelTail();
      newModel.id = 0;
      newModel.tailId = tail.id;
      newModel.boardModelId = this.boardModel.id;
      this.boardModel.boardModelTails.push(newModel);
    };
  });
}

if (this.formAdd.controls.tailReforcements.value.length > 0) {
  this.formAdd.controls.tailReforcements.value.forEach(tailR => {
    if(!this.boardModel.boardModelTailReinforcements.find(x => x.tailReinforcementId === tailR.id)) {
      const newModel = new BoardModelTailReinforcement();
      newModel.id = 0;
      newModel.tailReinforcementId = tailR.id;
      newModel.boardModelId = this.boardModel.id;
      this.boardModel.boardModelTailReinforcements.push(newModel);
    };
  });
}


    formData.append('boardModel', JSON.stringify(this.boardModel));
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
  if (this.formAdd.controls.dimension.value === undefined) {
    return this.toastr.error("Informe as dimensões disponíveis para esse modelo e clique em adicionar")
  }
  this.lstdimensions.push(this.formAdd.controls.dimension.value);
  this.formAdd.controls.dimension.reset();
}

deleteById(dimension: any) {
  const index: number = this.lstdimensions.indexOf(dimension);
  if (index !== -1) {
    this.lstdimensions.splice(index, 1);
  }
}

}


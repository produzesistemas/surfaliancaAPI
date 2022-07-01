import { Component, OnInit, TemplateRef, Output, EventEmitter, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BoardModelService} from '../_services/board-model.service';
import { environment } from 'src/environments/environment';
import { ShoppingCartService } from '../_services/shopping-cart.service';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { BoardModel } from '../_models/board-model-model';
import { LevelService } from '../_services/level.service';
import { ConstructionService } from '../_services/construction.service';
import { Construction } from '../_models/construction-model';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { PaintService } from '../_services/paint.service';
import { TailService } from '../_services/tail.service';
import { forkJoin } from 'rxjs';
import { Lamination } from '../_models/lamination-model';
import { LaminationService } from '../_services/lamination.service';
import { FinSystemService } from '../_services/fin-system.service';
import { FinSystem } from '../_models/fin-system-model';
import { Stringer } from '../_models/stringer-model';
import { StringerService } from '../_services/stringer.service';

@Component({
  selector: 'app-order-management',
  templateUrl: './order-management.component.html',
  styleUrls: ['./order-management.component.css']
})

export class OrderManagementComponent implements OnInit {
  form: FormGroup;
  submitted = false;
  isSelectedDimensions = false;
  paints = [];
  lst = [];
  imgModel: any ="custom.png";
  imgTail: any;
  imgFinSystem: any;
  imgStringer: any;
  modalPaint: BsModalRef;
  levels = [];
  logos = [];
  paint: any;
  public lstBoardModels = [];
  public finalValue: number;
  constructions: Construction[] = [];
  laminations: Lamination[] = [];
  stringers: Stringer[] = [];
  finSystems: FinSystem[] = [];
  tails = [];
  boardModelDimensions = [];
  boardModels: BoardModel[] = [];
  boardModel: BoardModel = new BoardModel();
  construction: Construction = new Construction();
  lamination: Lamination = new Lamination();
  stringer: Stringer = new Stringer();
  finSystem: FinSystem = new FinSystem();
  public itemCart;
  public shoppingCart: any[] = [];
  modalRef: BsModalRef;
  modalHelp: BsModalRef;
  @ViewChild('modalOrder') public templateref: TemplateRef<any>;
  @ViewChild('modalHelp') public templatehelp: TemplateRef<any>;
  @ViewChild('modalPaint') public templaterefPaint: TemplateRef<any>;



  constructor(
    private formBuilder: FormBuilder,
    private toastr: ToastrService,
    private router: Router,
    private boardModelService: BoardModelService,
    private modalService: BsModalService,
    private constructionService: ConstructionService,
    private laminationService: LaminationService,
    private finSystemService: FinSystemService,
    private stringerService: StringerService,
    private tailService: TailService,
    private levelService: LevelService,
    private shoppingCartService: ShoppingCartService,
    private paintService: PaintService,

  ) {
  }

  ngOnInit() {
    this.shoppingCart = this.shoppingCartService.loadCart();
    this.form = this.formBuilder.group({
      logo: ['', Validators.required],
      boardModel: ['', Validators.required],
      boardModelDimension: [''],
      level: [''],
      construction: [''],
      lamination: [''],
      finSystem:[''],
      stringer:[''],
      tail: [''],
      otherDimensions: [''],
      paint: [''],



    });

    forkJoin(
      this.constructionService.getAll(),
      this.tailService.getAll(),
      this.laminationService.getAll(),
      this.finSystemService.getAll(),
      this.stringerService.getAll()
    )    .subscribe(result => {
      this.constructions = result[0];
      this.tails = result[1];
      this.laminations = result[2];
      this.finSystems = result[3];
      this.stringers = result[4];
    });


    this.levels = this.levelService.get();
  }

  get f() { return this.form.controls; }

  getImage() {
    return environment.urlImagesLojas + this.imgModel;
}

  onSelectedModel(boardModel) {
    this.router.navigate([`/order/${boardModel.id}`]);
  }

  getQuantityItems() {
    if (this.shoppingCart !== null) {
        return this.shoppingCart.map(x => {
            return x
        }).reduce((sum, current) => sum + (current ? current.quantity : 0), 0);
    } else {
        return 0;
    }
}

openShoppingCart() {
  if ((this.shoppingCart === null) ||
      (this.shoppingCart === undefined) ||
      (this.shoppingCart.length === 0)) {
      // return this.toastr.error('O Carrinho está vazio. Adicione produtos');
  }
  this.router.navigate(['/shoppingcart']);
}

onChangeMarca(){
  this.imgModel = "custom.png";
  const filter: FilterDefaultModel = new FilterDefaultModel();
  filter.id = Number(this.form.controls.logo.value);
  this.boardModelService.getBoardModelByLogo(filter).subscribe(
    data => {
      this.boardModels = data;
      this.boardModelDimensions = []
    }
  );


}

onChangeModel(){
  if (this.form.controls.boardModel.value === undefined) {
    this.imgModel = "custom.png";
  } else {
    this.imgModel = this.form.controls.boardModel.value.imageName;
    this.boardModelDimensions = this.form.controls.boardModel.value.boardModelDimensions
    this.boardModel = this.form.controls.boardModel.value;
    this.isSelectedDimensions = true;
    this.finalValue = this.boardModel.value;
  }

}

onChangeTail(){
  if (this.form.controls.tail.value === undefined) {
    this.imgTail = "";
  } else {
    this.imgTail = this.form.controls.tail.value.imageName;
  }
}



onChangeDimension(){
  if (this.form.controls.boardModelDimension.value === undefined || this.form.controls.boardModelDimension.value === "") {
    this.isSelectedDimensions = true;
  } else {
    this.isSelectedDimensions = false;
  }

}

onCheckDimension(){
    return this.isSelectedDimensions;
      }


      openModalPaint() {
        if (this.form.controls.boardModel.value !== undefined) {

          const filter: FilterDefaultModel = new FilterDefaultModel();
          filter.id = this.form.controls.boardModel.value.id;
          this.paintService.getByModel(filter).subscribe(
            data => {
              this.paints = data;
              this.modalPaint = this.modalService.show(this.templaterefPaint, { class: 'modal-xl' });

            }
          );

        }

    }

    closeModalPaint() {
      this.modalPaint.hide();
  }

  getImagePaint(nomeImage) {
    return environment.urlImagesLojas + nomeImage;
}

getImageTail() {
  return environment.urlImagesLojas + this.imgTail;
}
getImageFinSystem() {
  return environment.urlImagesProducts + this.imgFinSystem;
}
getImageStringer() {
  return environment.urlImagesLojas + this.imgStringer;
}





onSelect(paint){
  this.closeModalPaint();
  this.imgModel = paint.imageName;
  this.form.controls.paint.setValue(paint);
  this.onCalculate();
}

getValueFinal() {
  return this.finalValue;
}

onCalculate() {
  this.finalValue = this.boardModel.value;
  if (this.form.controls.construction.value) {
    this.finalValue = this.finalValue + this.form.controls.construction.value.value;
  }
  if (this.form.controls.paint.value) {
    this.finalValue = this.finalValue + this.form.controls.paint.value.value;
  }
}

openModal() {
  this.modalHelp = this.modalService.show(this.templatehelp, { class: 'modal-sm' });
}

closeModal() {
  this.modalHelp.hide();
}

onChangeConstruction(){
  this.construction = this.form.controls.construction.value;
  this.onCalculate();
}

onChangeLamination(){
  this.lamination = this.form.controls.lamination.value;
  this.onCalculate();
}
onChangeFinSystem(){
  if (this.form.controls.finSystem.value === undefined) {
    this.imgFinSystem = "";
  } else {
    this.imgFinSystem = this.form.controls.finSystem.value.imageName;
  }
}
onChangeStringer(){
  if (this.form.controls.stringer.value === undefined) {
    this.imgStringer = "";
  } else {
    this.imgStringer = this.form.controls.stringer.value.imageName;
  }
}



onConfirm() {
  this.submitted = true;
      if (this.form.invalid) {
        return;
      }
      if (this.form.controls.boardModelDimension.value === "" && this.form.controls.otherDimensions.value === ""){
        return this.toastr.error("Obrigatório selecionar uma dimensão ou digite sua dimensão");
      }
      this.shoppingCart = this.shoppingCartService.loadCart();
      if (this.shoppingCart === null) {
        this.shoppingCart = [];
      }
      this.itemCart = {};
      this.itemCart.quantity = 1;
      this.itemCart.value = this.finalValue;
      this.itemCart.typeSaleId = 1;
      this.itemCart.boardModelId = this.boardModel.id;
      this.itemCart.construction = this.form.controls.construction ? this.form.controls.construction.value : null;
      this.itemCart.constructionId = this.form.controls.construction ? this.form.controls.construction.value.id : null;
      this.itemCart.levelId = this.form.controls.level ? this.form.controls.level.value.value : null;
      this.itemCart.level = this.form.controls.level ? this.form.controls.level.value : null;
      this.itemCart.boardModelDimensionId = this.form.controls.boardModelDimension ? this.form.controls.boardModelDimension.value.id : null;
      this.itemCart.boardModelDimension = this.form.controls.boardModelDimension ? this.form.controls.boardModelDimension.value : null;
      this.itemCart.otherDimensions = this.form.controls.otherDimensions ? this.form.controls.otherDimensions.value : null;
      this.itemCart.tailId = this.form.controls.tail ? this.form.controls.tail.value.id : null;
      this.itemCart.tail = this.form.controls.tail ? this.form.controls.tail.value : null;
      this.itemCart.laminationId = this.form.controls.lamination ? this.form.controls.lamination.value.id : null;
      this.itemCart.lamination = this.form.controls.lamination ? this.form.controls.lamination.value : null;
      this.itemCart.name = this.boardModel.name;
      this.itemCart.imageName = this.imgModel;
      this.itemCart.paintId = this.form.controls.paint ? this.form.controls.paint.value.id : null;
      this.itemCart.paint = this.form.controls.paint ? this.form.controls.paint.value : null;
      this.itemCart.finSystemId = this.form.controls.finSystem ? this.form.controls.finSystem.value.id : null;
      this.itemCart.finSystem = this.form.controls.finSystem ? this.form.controls.finSystem.value : null;
      this.itemCart.stringerId = this.form.controls.stringer ? this.form.controls.stringer.value.id : null;
      this.itemCart.stringer = this.form.controls.stringer ? this.form.controls.stringer.value : null;
      this.shoppingCart.push(this.itemCart);
      this.shoppingCartService.updateCart(this.shoppingCart);
      this.modalRef = this.modalService.show(this.templateref, { class: 'modal-md' });
  }

  onCancel() {
    this.router.navigate([`/index`]);
  }

  onConfirmOrder() {
    this.close();
    return this.router.navigate(['/shoppingcart']);
    }

    onKeepBuying() {
      this.close();
      return this.router.navigate(['/index']);
    }

    close() {
      this.modalRef.hide();
      }


}

import { Component, OnInit, ViewChild, ElementRef, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { BoardModel } from 'src/app/_models/board-model-model';
import { environment } from '../../../environments/environment';
import { BoardModelService } from 'src/app/_services/board-model.service';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { LitigationService } from 'src/app/_services/litigation.service';
import { PaintService } from 'src/app/_services/paint.service';
import { OrderService } from 'src/app/_services/order.service';
import { FilterDefaultModel } from 'src/app/_models/filter-default-model';
import { Litigation } from 'src/app/_models/litigation-model';

@Component({
  selector: 'app-board-model-detail',
  templateUrl: './board-model-detail.component.html'
})
export class BoardModelDetailComponent implements OnInit {
  public currentUser;
  formAdd: FormGroup;
  formLitigation: FormGroup;
  submitted = false;
  submittedLitigation = false;
  public boardModel: BoardModel = new BoardModel();
  @ViewChild('modalOrder') public templateref: TemplateRef<any>;
  modalRef: BsModalRef;
  logo: any;
  imgPaint: any;
  imgTail: any;
  public shoppingCart: any[] = [];
  public itemCart;
  public finalValue: number;
  lstPaint = [];
  lstFinishing = [];
  lstWeight = [];
  lstLevel = [];
  constructor(
    private formBuilder: FormBuilder,
    private modalService: BsModalService,
    private router: Router,
    private toastr: ToastrService,
    private authenticationService: AuthenticationService,
    private litigationService: LitigationService,
    private orderService: OrderService,
    private route: ActivatedRoute,
    private shoppingCartService: ShoppingCartService,
    private boardModelService: BoardModelService,
    private paintService: PaintService
  ) { }

  get f() { return this.formAdd.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.boardModel.id = Number(params.id);
        this.load();
      } else {
          return this.toastr.error('Modelo não foi selecionado')
      }
    });

    this.formAdd = this.formBuilder.group({
       dimension: ['', [Validators.required]],
       color: ['', [Validators.required]],
    });

    // this.lstLevel = this.litigationService.getLevels();
    // this.lstWeight = this.litigationService.getWeight();

  }

  load() {
      this.boardModelService.getToOrder(this.boardModel).subscribe(result => {
        this.boardModel = result;
        this.finalValue = this.boardModel.value;
      });
  }

  onCancel() {
    this.router.navigate([`/index`]);
  }

  getImage(nomeImage) {
    return environment.urlImagesLojas + nomeImage;
}

onConfirm() {
  this.submitted = true;
      if (this.formAdd.invalid) {
        return;
      }
      this.shoppingCart = this.shoppingCartService.loadCart();
      if (this.shoppingCart === null) {
        this.shoppingCart = [];
      }
      this.itemCart = {};
      this.itemCart.productId = 0;
      this.itemCart.quantity = 1;
      this.itemCart.value = this.finalValue;
      this.itemCart.productTypeId = 1;
      this.itemCart.productStatusId = 1;
      this.itemCart.typeSaleId = 1;
      this.itemCart.boardModelId = this.boardModel.id;
      this.itemCart.boardTypeId = this.formAdd.controls.boardType.value.boardTypeId;
      this.itemCart.laminationId = this.formAdd.controls.lamination.value.laminationId;
      this.itemCart.shaperId = this.formAdd.controls.shaper.value.shaperId;
      this.itemCart.sizeId = this.formAdd.controls.size.value.sizeId;
      this.itemCart.widthId = this.formAdd.controls.width.value.widthId;
      this.itemCart.tailId = this.formAdd.controls.tail.value.tailId;
      this.itemCart.bottomId = this.formAdd.controls.bottom.value.bottomId;
      this.itemCart.constructionId = this.formAdd.controls.construction.value.constructionId;
      this.itemCart.name = this.boardModel.name;
      this.itemCart.imageName = this.boardModel.imageName;
      this.itemCart.paintId = this.formAdd.controls.paint ? this.formAdd.controls.paint.value.id : null;
      this.itemCart.finishingId = this.formAdd.controls.finishing ? this.formAdd.controls.finishing.value : null;
      this.itemCart.litigation = this.formLitigation.controls.litigation ? this.formLitigation.controls.litigation.value : null;
      this.shoppingCart.push(this.itemCart);
      this.shoppingCartService.updateCart(this.shoppingCart);
      this.modalRef = this.modalService.show(this.templateref, { class: 'modal-md' });
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

    // onChangeConstruction() {
    //     this.finalValue = this.boardModel.value;
    //     if (this.formAdd.controls.construction.value) {
    //       this.finalValue = this.finalValue + this.formAdd.controls.construction.value.construction.value;
    //     }
    //     if (this.formAdd.controls.paint.value) {
    //       this.finalValue = this.finalValue + this.formAdd.controls.paint.value.value;
    //     }

    //   }

        // onCalculationLitigation() {
        //   this.submittedLitigation = true;
        //   if (this.formLitigation.invalid) {
        //     return;
        //   }
        //   let v = this.lstWeight.find(x => x.id === this.formLitigation.controls.weigth.value).values[this.formLitigation.controls.level.value];
        //   this.formLitigation.controls.litigation.setValue(v);
        // }

}


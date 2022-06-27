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
import { PaintService } from 'src/app/_services/paint.service';
import { ColorService } from 'src/app/_services/color.service';
import { BorderColorService } from 'src/app/_services/border-color.service';
import { OrderService } from 'src/app/_services/order.service';
import { FilterDefaultModel } from 'src/app/_models/filter-default-model';
import { LogoService } from 'src/app/_services/logo.service';
import { ConstructionService } from 'src/app/_services/construction.service';

@Component({
  selector: 'app-order-form',
  templateUrl: './order-form.component.html'
})
export class OrderFormComponent implements OnInit {
  public currentUser;
  form: FormGroup;
  submitted = false;
  public boardModel: BoardModel = new BoardModel();
  @ViewChild('modalOrder') public templateref: TemplateRef<any>;
  @ViewChild('modalHelp') public templatehelp: TemplateRef<any>;
  modalRef: BsModalRef;
  modalHelp: BsModalRef;
  logo: any;
  construction: any;
  imgPaint: any;
  imgColor: any;
  imgBorderColor: any;
  imgPaintFront: any;
  imgColorFront: any;
  imgBorderColorFront: any;

  imgLogo: any;
  public shoppingCart: any[] = [];
  public itemCart;
  public finalValue: number;
  lstPaint = [];
  lstBorderColor = [];
  lstColor = [];
  lstFinishing = [];
  lstWeight = [];
  lstLevel = [];
  lstLogo = [];
  lstConstruction = [];
  boardModelDimensions = [];

  constructor(
    private formBuilder: FormBuilder,
    private modalService: BsModalService,
    private router: Router,
    private toastr: ToastrService,
    private authenticationService: AuthenticationService,
    private orderService: OrderService,
    private route: ActivatedRoute,
    private shoppingCartService: ShoppingCartService,
    private boardModelService: BoardModelService,
    private paintService: PaintService,
    private colorService: ColorService,
    private borderColorService: BorderColorService,
    private logoService: LogoService,
    private constructionService: ConstructionService


  ) { }

  get f() { return this.form.controls; }

  ngOnInit() {
    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.boardModel.id = Number(params.id);
        this.load();
      } else {
          return this.toastr.error('Modelo não foi selecionado')
      }
    });


    this.form = this.formBuilder.group({
      construction: ['', [Validators.required]],
      dimension: ['',[Validators.required]],
      logo: ['',[Validators.required]],
      paintBottom: [''],
      colorBottom: [''],
      borderColorBottom: [''],
      paintFront: [''],
      colorFront: [''],
      borderColorFront: [''],

   });




  }

  load() {
      this.boardModelService.getToOrder(this.boardModel).subscribe(result => {
        this.boardModel = result;
        this.finalValue = this.boardModel.value;
      });
      const filter: FilterDefaultModel = new FilterDefaultModel();
      filter.name = '';
      this.paintService.getAll(filter).subscribe(paints => {
        this.lstPaint = paints;
      });
      this.colorService.getAll(filter).subscribe(colors => {
        this.lstColor = colors;
      });
      this.borderColorService.getAll(filter).subscribe(borderColors => {
        this.lstBorderColor = borderColors;
      });
      this.orderService.getAllFinishing().subscribe(finishings => {
        this.lstFinishing = finishings;
      })
      this.logoService.getAll().subscribe(logos => {
        this.lstLogo = logos;
      });
      this.constructionService.getAllConstruction().subscribe(construction => {
        this.lstConstruction = construction;
      });


  }

  onCancel() {
    this.router.navigate([`/order`]);
  }

  getImage(nomeImage) {
    return environment.urlImagesLojas + nomeImage;
}

onConfirm() {
  this.submitted = true;
      if (this.form.invalid) {
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
      this.itemCart.constructionId = this.form.controls.construction.value.constructionId;
      this.itemCart.name = this.boardModel.name;
      this.itemCart.imageName = this.boardModel.imageName;
      this.itemCart.paintId = this.form.controls.paint ? this.form.controls.paint.value.id : null;
      this.itemCart.colorId = this.form.controls.color ? this.form.controls.color.value.id : null;
      this.itemCart.borderColorId = this.form.controls.borderColor ? this.form.controls.borderColor.value.id : null;
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

    onChangeConstruction() {
      this.onCalculate();
      }

    onChangePaintBottom() {
      this.imgPaint = environment.urlImagesLojas + this.form.controls.paintBottom.value.imageName;
      this.onCalculate();
      }

      onCalculate() {
        this.finalValue = this.boardModel.value;
        if (this.form.controls.construction.value) {
          this.finalValue = this.finalValue + this.form.controls.construction.value.value;
        }
        if (this.form.controls.paintBottom.value) {
          this.finalValue = this.finalValue + this.form.controls.paintBottom.value.value;
        }
        if (this.form.controls.colorBottom.value) {
          this.finalValue = this.finalValue + this.form.controls.colorBottom.value.value;
        }
        if (this.form.controls.borderColorBottom.value) {
          this.finalValue = this.finalValue + this.form.controls.borderColorBottom.value.value;
        }
        if (this.form.controls.paintFront.value) {
          this.finalValue = this.finalValue + this.form.controls.paintFront.value.value;
        }
        if (this.form.controls.colorFront.value) {
          this.finalValue = this.finalValue + this.form.controls.colorFront.value.value;
        }
        if (this.form.controls.borderColorFront.value) {
          this.finalValue = this.finalValue + this.form.controls.borderColorFront.value.value;
        }

      }

      getValueFinal() {
        return this.finalValue;
      }

      onChangeColorBottom() {
        this.imgColor = environment.urlImagesLojas + this.form.controls.colorBottom.value.imageName;
        this.onCalculate();
        }

        onChangeBorderColorBottom() {
          this.imgBorderColor = environment.urlImagesLojas + this.form.controls.borderColorBottom.value.imageName;
          this.onCalculate();
        }


        onChangePaintFront() {
          this.imgPaintFront = environment.urlImagesLojas + this.form.controls.paintFront.value.imageName;
          this.onCalculate();
        }

          onChangeColorFront() {
            this.imgColorFront = environment.urlImagesLojas + this.form.controls.colorFront.value.imageName;
            this.onCalculate();
          }

            onChangeBorderColorFront() {
              this.imgBorderColorFront = environment.urlImagesLojas + this.form.controls.borderColorFront.value.imageName;
              this.onCalculate();
            }




        onChangeLogo() {
          this.imgLogo = environment.urlImagesLojas + this.form.controls.logo.value.imageName;
          }

          closeModal() {
            this.modalHelp.hide();
          }

          openModal() {
                this.modalHelp = this.modalService.show(this.templatehelp, { class: 'modal-sm' });
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

}


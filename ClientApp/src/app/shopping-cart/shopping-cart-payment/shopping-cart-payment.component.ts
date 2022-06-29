import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Product } from 'src/app/_models/product-model';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FilterDefaultModel } from '../../_models/filter-default-model';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Order } from 'src/app/_models/order-model';
import { CurrencyPipe, Location } from '@angular/common';
import { CreditCard } from 'src/app/_models/credit-card-model';
import { Payment } from 'src/app/_models/payment-model';
import { MerchantOrder } from 'src/app/_models/merchant-order-model';
import { PaymentCieloService } from 'src/app/_services/payment-cielo.service';
import { BsDaterangepickerConfig } from 'ngx-bootstrap/datepicker';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';
import { StoreService } from 'src/app/_services/store.service';
import { OrderService } from 'src/app/_services/order.service';

@Component({
  selector: 'app-shopping-cart-payment',
  templateUrl: './shopping-cart-payment.component.html'
})
export class ShoppingCartPaymentComponent implements OnInit {
  modalRef: BsModalRef;
  formAdd: FormGroup;
  formCard: FormGroup;
  submitted = false;
  public totalValue: number;
  public order: Order = new Order();
  public loja: any = {};
  public parent: number = 0;
  public shoppingCart: any[] = [];
  public itemCart;
  public currentUser;
  public lst : any[] = [];
  public lstBrand : any[] = [];

  @ViewChild('modal') public templateref: TemplateRef<any>;
  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private currencyPipe: CurrencyPipe,
    private shoppingCartService: ShoppingCartService,
    private paymentCieloService: PaymentCieloService,
    private authenticationService: AuthenticationService,
    private _location: Location,
    private route: ActivatedRoute,
    private storeService: StoreService,
    private orderService: OrderService
  ) { }

  get fcard() { return this.formCard.controls; }

  ngOnInit() {
    if (this.authenticationService.getCurrentUser()) {
      this.authenticationService.getCurrentUser().role === 'Cliente' ? this.currentUser = this.authenticationService.getCurrentUser() : null;
  }

  this.loja = this.storeService.loadStoreSelected();
  if (this.loja) {
    for (let i = 0; i < this.loja.numberInstallmentsCard; i++) {
      this.lst.push({value: i + 1, label: (i + 1).toString()})
    }
  }

  this.formCard = this.formBuilder.group({
    cardNumber: ['', [Validators.required]],
    brand: ['', [Validators.required]],
    expirationDate: ['', [Validators.required]],
    holder: ['', [Validators.required]],
    securityCode: ['', [Validators.required]],
    orderValue: [this.getTotalProducts() + this.order.taxValue],
    quantity: ['', [Validators.required]],
});

this.lstBrand.push({value: 'American Express'});
this.lstBrand.push({value: 'Aura'});
this.lstBrand.push({value: 'Diners Club'});
this.lstBrand.push({value: 'Discover'});
this.lstBrand.push({value: 'Elo'});
this.lstBrand.push({value: 'Hipercard'});
this.lstBrand.push({value: 'JCB'});
this.lstBrand.push({value: 'Master'});
this.lstBrand.push({value: 'Visa'});

    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.order.id = Number(params.id);
        this.parent = Number(params.parent);
        this.load();
      }
    });
  }

  load() {
     this.orderService.getOrder(this.order.id).subscribe(order => {
      this.order = order;
    });
  }

  onCancel() {
    this._location.back();
  }

getImage(nomeImage) {
  return environment.urlImagesProducts + nomeImage;
}

    getSubtotalProducts(item) {
      return item.productValue * item.qtd;
      }

      // getTotalOrdereds() {
      //   let totalValue = 0;
      //   if (this.order.orderProduct.length > 0) {
      //     this.order.orderProduct.forEach((item) => {
      //       totalValue += (item.productValue * item.qtd);
      //     });
      //   }
      //   return totalValue;
      // }

      getTotalProducts() {
        let totalValue = 0;
        if (this.order.orderProduct.length > 0) {
          this.order.orderProduct.forEach((item) => {
            totalValue += (item.productValue * item.qtd);
          });
        }
        return totalValue;
      }

      getTotalOrdered() {
        let totalValue = 0;
        if (this.order.orderProductOrdered.length > 0) {
          this.order.orderProductOrdered.forEach((item) => {
            totalValue += (item.value * item.qtd);
          });
        }
        return totalValue;
      }

      getDataAcompanhamento(id) {
        const find = this.order.orderTracking.find(x => x.orderId === id);
        if (find) {
            return find.dateTracking;
        }
    }


    onConfirm() {
      this.submitted = true;
      if (this.formCard.invalid) {
          return;
      }

      const creditCard = new CreditCard();
      creditCard.Brand =  this.formCard.controls.brand.value;
      creditCard.CardNumber = this.formCard.controls.cardNumber.value;
      creditCard.ExpirationDate = this.formCard.controls.expirationDate.value;
      creditCard.Holder = this.formCard.controls.holder.value;
      creditCard.SecurityCode = this.formCard.controls.securityCode.value;
      const payment = new Payment();
      let srt = this.currencyPipe.transform(this.getTotalSale(), 'R$');
      payment.Amount = Number(srt.replace('.', '').replace(',', '').replace('R$', ''));

      payment.CreditCard = creditCard;
      payment.Installments = Number(this.formCard.controls.quantity.value);
      payment.SoftDescriptor = 'dajusemijoias.com.br';
      payment.Type = 'CreditCard';
      payment.Capture = true;
      payment.Authenticate = false;
      const merchantOrder = new MerchantOrder();
      merchantOrder.MerchantOrderId = this.order.id.toString();
      merchantOrder.Payment = payment;
      console.log(merchantOrder);
      this.paymentCieloService.sendCielo(merchantOrder).subscribe(result => {
        if (result) {
          console.log(result);
          if ((result.Payment.ReturnCode === '4') || (result.Payment.ReturnCode === '6') || (result.Payment.ReturnCode === '00')){
            this.order.paymentConditionId = 1;
            this.order.paymentId = result.Payment.PaymentId;
              this.orderService.setPaymentOk(this.order).subscribe(res => {
                  this.toastr.success('Pagamento aprovado com sucesso');
                  if (this.parent === 0) {
                    this.router.navigate(['/index']);
                  }
                  if (this.parent === 1) {
                    this.router.navigate(['/client-area-order']);
                  }
          });
          } else {
            switch(result.Payment.ReturnCode) {
              case '05':
              this.toastr.error('Não autorizado');
              break;
              case '57':
                this.toastr.error('Cartão Expirado');
                break;
                case '78':
                  this.toastr.error('Cartão Bloqueado');
                  break;
                  case '99':
                    this.toastr.error('Falha na conexão');
                    break;
                    case '77':
                      this.toastr.error('Cartão Cancelado');
                      break;
                      case '70':
                        this.toastr.error('Falha ao identificar o Cartão de Crédito');
                        break;
                      default:
                        this.toastr.error('Falha ao identificar o Cartão de Crédito');
                        break;

            }
            // return this.toastr.error(result.Message);
          }

        }

      });



  }

  getTotalSale() {
    let totalValue = this.getTotalProducts() + this.getTotalOrdered();
   if (this.order.coupon) {
       if (this.order.coupon.type) {
           totalValue -= this.order.coupon.value;
       } else {
           totalValue -= (totalValue * this.order.coupon.value) /100;
       }
   }

   if (this.order.taxValue) {
       totalValue += this.order.taxValue;
   }
   return totalValue;
}

getImageBoardModel(item) {
  return environment.urlImagesLojas + item.boardModel.imageName;
}

getImagePaint(item) {
  return environment.urlImagesLojas + item.paint.imageName;
}

getSubtotal(item) {
  return item.value * item.quantity;
  }

  getSubtotalOrdered(item) {
    return item.value * item.qtd;
    }


}


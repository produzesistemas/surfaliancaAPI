import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { OrderService } from 'src/app/_services/order.service';
import { ProductService } from 'src/app/_services/product.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Product } from 'src/app/_models/product-model';
import { ShoppingCartService } from '../../_services/shopping-cart.service';
import { PaymentCieloService } from '../../_services/payment-cielo.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FilterDefaultModel } from '../../_models/filter-default-model';
import { StoreService } from '../../_services/store.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Order } from 'src/app/_models/order-model';
// import { PedidoAvaliacao } from '../../_model/pedido-avaliacao-model';
import { Location } from '@angular/common';
// import { ProdutoAvaliacao } from 'src/app/_model/produto-avaliacao-model';
import { OrderProduct } from 'src/app/_models/order-product-model';
import { OrderEvaluation } from 'src/app/_models/order-evaluation-model';

@Component({
    selector: 'app-client-area-form',
    templateUrl: './client-area-form.component.html'
})
export class ClientAreaOrderFormComponent implements OnInit {
    modalRef: BsModalRef;
    modalPix: BsModalRef;
    formevaluation: FormGroup;
    submitted = false;
    public order: Order = new Order();
    public orderProduct: OrderProduct = new OrderProduct();
    public store: any = {};
    public parent: number = 0;
    public shoppingCart: any[] = [];
    public itemCart;
    public currentUser;
    payment: any;
    form: FormGroup;
    imgComprovantePix: any;
    @ViewChild('modal') public templateref: TemplateRef<any>;
    constructor(
        private modalService: BsModalService,
        private formBuilder: FormBuilder,
        private router: Router,
        private toastr: ToastrService,
        private _location: Location,
        private shoppingCartService: ShoppingCartService,
        private paymentCieloService: PaymentCieloService,
        private authenticationService: AuthenticationService,
        private route: ActivatedRoute,
        private storeService: StoreService,
        private orderService: OrderService,
        private productService: ProductService
    ) { }

    // get f() { return this.form.controls; }
    // get fa() { return this.formAvaliacao.controls; }

    ngOnInit() {
        if (this.authenticationService.getCurrentUser()) {
            this.authenticationService.getCurrentUser().role === 'Cliente' ? this.currentUser = this.authenticationService.getCurrentUser() : null;
        }
        this.form = this.formBuilder.group({
            comment: [''],
        });

        this.formevaluation = this.formBuilder.group({
            comment: [''],
        });
        this.route.params.subscribe(params => {
            if (params.id > 0) {
                this.order.id = Number(params.id);
                this.load();
            }
        });
    }

    load() {
        this.orderService.getOrder(this.order.id).subscribe(order => {
            this.order = order;
            if (this.order.paymentId) {
                this.paymentCieloService.getCielo(this.order.paymentId).subscribe(payment => {
                    if (payment) {
                        let f = payment.Payment.Amount.toString().substring(0, payment.Payment.Amount.toString().length - 2) + '.' + payment.Payment.Amount.toString().slice(-2);
                        payment.Payment.Amount = Number(f);
                        this.payment = payment;
                    }

                });
            }
            // if (this.order.formaPagamentoId === 2) {
            //     this.imgComprovantePix = environment.urlImagesProducts + this.order.imgComprovantePix;
            // }
        });
    }

    //   setControls(item: Produto) {
    //     this.formAdd.controls.id.setValue(item.id);
    //     this.formAdd.controls.description.setValue(item.descricao);
    //     this.formAdd.controls.value.setValue(item.valor);
    //   }

    // onCancel() {
    //     const filter: FilterDefaultModel = new FilterDefaultModel();
    //     filter.id = this.order.id;
    //     this.orderService.cancel(filter).subscribe(res => {
    //         this.toastr.success('Pedido cancelado com sucesso');
    //         this.router.navigate(['/client-area-order']);
    //     });
    // }

    onBack() {
        this._location.back();
    }

    getImageProduct(nomeImage) {
        return environment.urlImagesProducts + nomeImage;
    }

    getSubtotalProducts(item) {
        return item.valorProduto * item.qtd;
    }

    getTotalOrdereds() {
        let totalValue = 0;
        if (this.order.orderProduct.length > 0) {
            this.order.orderProduct.forEach((item) => {
                totalValue += (item.productValue * item.qtd);
            });
        }
        return totalValue;
    }

    getTotalProducts() {
        let totalValue = 0;
        if (this.order.orderProduct.length > 0) {
            this.order.orderProduct.forEach((item) => {
                totalValue += (item.productValue * item.qtd);
            });
        }
        return totalValue;
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



    getDateFollowUp(orderTracking) {
        return orderTracking.data;
    }

    getStatusAtual(order) {
        return order.orderTracking.length > 0 ? order.orderTracking[order.orderTracking.length - 1].statusPaymentOrder.description : undefined;
    }

    getStatusOrder(order) {
        return order.orderTracking.length > 0 ? order.orderTracking[order.orderTracking.length - 1].statusOrder.description : undefined;
    }

    getStatusFollowUp(id) {
        return this.order.orderTracking.find(x => x.statusOrderId === id) ? true : false;
    }

    getSubtotal(item) {
        return item.productValue * item.qtd;
    }

    checkCancel(item) {
        if (item.orderTracking.length > 0) {
            return item.orderTracking[item.orderTracking.length - 1].statusOrderId === 1 ? true : false;
        }
    }

    checkEvaluation(item) {
        if (item.orderTracking.length > 0) {
            return item.orderTracking[item.orderTracking.length - 1].statusOrderId === 7 &&
                item.orderTracking[item.orderTracking.length - 1].statusPaymentOrderId === 2 &&
                item.orderEvaluation.length === 0 ? true : false;
        }
    }


    productEvaluate(qtdStars, orderProcuct) {
        const evaluation: OrderEvaluation = new OrderEvaluation();
        evaluation.comment = this.formevaluation.controls.comment.value;
        evaluation.star = qtdStars;
        evaluation.orderId = this.order.id;
        evaluation.id = orderProcuct.id;
        this.productService.evaluation(evaluation).subscribe(result => {
            this.toastr.success('Produto avaliado com sucesso');
            this.close();
            return this.router.navigate(['/client-area-order']);
        });

    }

    close() {
        this.modalRef.hide();
    }
    closePix() {
        this.modalPix.hide();
    }

    displayModal(template: TemplateRef<any>, item: OrderProduct) {
        this.orderProduct = item;
        this.modalRef = this.modalService.show(template, { class: 'modal-md' });
    }

    openReceiptPix(template: TemplateRef<any>) {
        this.modalPix = this.modalService.show(template, { class: 'modal-md' });
    }

    onCancel() {
      const filter: FilterDefaultModel = new FilterDefaultModel();
      filter.id = this.order.id;
      this.orderService.cancel(filter).subscribe(res => {
          this.toastr.success('Pedido cancelado com sucesso');
          this.router.navigate(['/client-area/order']);
      });
  }

  getSubtotalOrdered(item) {
    return item.value * item.qtd;
    }

    getImagePaint(item) {
      return environment.urlImagesLojas + item.paint.imageName;
    }

    getImageBoardModel(item) {
      return environment.urlImagesLojas + item.boardModel.imageName;
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




}


import { Component, OnInit, TemplateRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { OrderService } from '../_services/order.service';
import { Order } from '../_models/order-model';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
    selector: 'app-client-area',
    templateUrl: './client-area.component.html'
})

export class ClientAreaComponent implements OnInit {
    lst = [];
    public order: Order = new Order();
    modalDetails: BsModalRef;
    constructor( private toastr: ToastrService,
      private modalService: BsModalService,
                 private router: Router,
                 private orderService: OrderService,) {
    }

    ngOnInit() {
        this.orderService.getByUser().subscribe(
            data => {
              this.lst = data;
            }
          );
    }

    getImage(nomeImage) {
        return environment.urlImagesLojas + nomeImage;
    }

      checkPay(item) {
        return item.pedidoAcompanhamentos.find(x => x.statusPagamentoPedidoId === 2) ? false : true;
      }

    getImageProduct(nomeImage) {
        return environment.urlImagesProducts + nomeImage;
    }

    getSubtotal(item) {
        return item.productValue * item.qtd;
        }

    getTotalItems() {
            return this.order.orderProduct.reduce((sum, current) => sum + (current.productValue * current.qtd), 0);
        }

    getStatusAcompanhamento(id) {
        return this.order.orderTracking.find(x => x.statusOrderId === id) ? true : false;
    }

    getDataAcompanhamento(id) {
        const find = this.order.orderTracking.find(x => x.statusOrderId === id);
        if (find) {
            return find.dateTracking;
        }
    }

    getStatusAtual(order: Order) {
      return order.orderTracking[order.orderTracking.length-1].statusPaymentOrder.description;
    }

    getStatusOrder(order: Order) {
      return order.orderTracking[order.orderTracking.length-1].statusOrder.description;
    }

    getTotalPedido(order: Order) {
      return (order.orderProductOrdered.reduce((sum, current) => sum + (current.value * current.qtd), 0)) +
      (order.orderProduct.reduce((sum, current) => sum + (current.productValue * current.qtd), 0)) +
      order.taxValue
    }


    openDetails(order: Order) {
      this.router.navigate([`/client-area/order/${order.id}`]);
    }

    closeDetails() {
      this.modalDetails.hide();
      }

      getImagePaint(item) {
        return environment.urlImagesLojas + item.paint.imageName;
      }

      getImageBoardModel(item) {
        return environment.urlImagesLojas + item.boardModel.imageName;
      }

}


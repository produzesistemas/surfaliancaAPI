import { StatusOrder } from "./status-order-model";
import { StatusPaymentOrder } from "./status-payment-order-model";

export class OrderTracking {
    id: number;
    orderId: number;
    statusOrderId: number;
    statusPaymentOrderId: number;
    dateTracking: Date;

    statusOrder: StatusOrder = new StatusOrder();
    statusPaymentOrder: StatusPaymentOrder = new StatusPaymentOrder();
}
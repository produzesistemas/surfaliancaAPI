import { OrderProduct } from "./order-product-model";
import { OrderProductOrdered } from "./order-product-ordered-model";
import { OrderTracking } from "./order-tracking-model";
import { Coupon } from './coupon-model';

export class Order {
    id: number;
    obs: string;
    taxValue: number;
    applicationUserId: string;
    couponId: number;
    paymentConditionId: number;
    paymentId: string;
    address: string;
    city: string;
    postalCode: string;
    district: string;
    complement: string;
    reference: string;
    state: string;
    coupon: Coupon;
    justification: string;
    deliveryDate: Date;

    orderProduct: OrderProduct[] = [];
    orderProductOrdered: OrderProductOrdered[] = [];
    orderTracking: OrderTracking[] = [];
}

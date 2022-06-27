import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Order } from '../_models/order-model';
import { Finishing } from '../_models/finishing-model';

@Injectable({ providedIn: 'root' })
export class OrderService extends GenericHttpService<Order> {
    constructor(private http: HttpClient) {
        super(http);
    }

    save(entity) {
      return this.http.post(`${this.getUrlApi()}order/save`, entity,
      {
        params: new HttpParams().set(
          'Client',
          'true'
        )
      });
   }

    getAllFinishing() {
      return this.http.get<Finishing[]>(`${this.getUrlApi()}order/getAllFinishing`);
   }

   getLastOrderByUser() {
    return this.http.get<Order>(`${this.getUrlApi()}order/getLastOrderByUser`,
    {
      params: new HttpParams().set(
        'Client',
        'true'
      )
    });
 }
 getOrder(id: number) {
  return this.http.get<any>(`${this.getUrlApi()}order/${id}`);
}

    getByUser() {
      return this.http.get<Order[]>(`${this.getUrlApi()}order/getByUser`,
      {
        params: new HttpParams().set(
          'Client',
          'true'
        )
      });
    }

 getById(id: number) {
  return this.http.get<Order>(`${this.getUrlApi()}order/${id}`,
  {
    params: new HttpParams().set(
      'Client',
      'true'
    )
  });
}

cancel(filter: any) {
  return this.postAll('order/cancel', filter);
}


sendPaymentCielo(filter: any) {
  return this.http.post(`${this.getUrlApi()}order/sendPaymentCielo`, filter,
  {
    params: new HttpParams().set(
      'Client',
      'true'
    )
  });
}

setPaymentOk(filter: any) {
  return this.postAll('order/setPaymentOk', filter);
}

evaluation(filter: any) {
  return this.postAll('order/evaluation', filter);
}

}

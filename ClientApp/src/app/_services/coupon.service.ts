import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Coupon } from '../_models/coupon-model';

@Injectable({ providedIn: 'root' })

export class CouponService extends GenericHttpService<Coupon> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
        return this.postAll('cupom/filter', filter);
      }

      getByCodigo(filter: any) {
        return this.post('cupom/getByCodigo', filter);
      }

    get(id: any) {
        return this.http.get<Coupon>(`${this.getUrlApi()}cupom/${id}`);
    }

    deleteById(id) {
        return this.delete(`cupom/${id}`);
  }

  active(entity) {
    return this.post('cupom/active', entity);
 }

 save(entity) {
    return this.post('cupom/save', entity);
 }

}

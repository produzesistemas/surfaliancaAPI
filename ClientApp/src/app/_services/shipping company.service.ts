import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { ShippingCompany } from '../_models/shipping company-model';

@Injectable({ providedIn: 'root' })
export class ShippingCompanyService extends GenericHttpService<ShippingCompany> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('ShippingCompany/filter', filter);
    }

    getAll() {
      return this.http.get<ShippingCompany[]>(`${this.getUrlApi()}ShippingCompany/getAll`);
    }

    save(entity) {
      return this.post('ShippingCompany/save', entity);
   }

    deleteById(id) {
          return this.delete(`ShippingCompany/${id}`);
    }

    getById(id: any) {
      return this.http.get<ShippingCompany>(`${this.getUrlApi()}ShippingCompany/${id}`);
  }

  active(entity) {
    return this.post('ShippingCompany/active', entity);
}
}

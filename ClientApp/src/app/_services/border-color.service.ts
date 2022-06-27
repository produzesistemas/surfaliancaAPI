import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { BorderColor } from '../_models/border-color-model';

@Injectable({ providedIn: 'root' })
export class BorderColorService extends GenericHttpService<BorderColor> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('borderColor/filter', filter);
    }

    getAll(filter: any) {
      return this.postAll('borderColor/getAll', filter);
    }

    save(entity) {
      return this.post('borderColor/save', entity);
   }

    deleteById(id) {
          return this.delete(`borderColor/${id}`);
    }

    getById(id: any) {
      return this.http.get<BorderColor>(`${this.getUrlApi()}borderColor/${id}`);
  }

  active(entity) {
    return this.post('borderColor/active', entity);
}
}

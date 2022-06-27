import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Color } from '../_models/color-model';

@Injectable({ providedIn: 'root' })
export class ColorService extends GenericHttpService<Color> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('Color/filter', filter);
    }

    getAll(filter: any) {
      return this.postAll('Color/getAll', filter);
    }

    save(entity) {
      return this.post('Color/save', entity);
   }

    deleteById(id) {
          return this.delete(`Color/${id}`);
    }

    getById(id: any) {
      return this.http.get<Color>(`${this.getUrlApi()}Color/${id}`);
  }

  active(entity) {
    return this.post('Color/active', entity);
}
}

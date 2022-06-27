import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Paint } from '../_models/paint-model';

@Injectable({ providedIn: 'root' })
export class PaintService extends GenericHttpService<Paint> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('Paint/filter', filter);
    }

    getAll(filter: any) {
      return this.postAll('Paint/getAll', filter);
    }

    save(entity) {
      return this.post('Paint/save', entity);
   }

    deleteById(id) {
          return this.delete(`Paint/${id}`);
    }

    getById(id: any) {
      return this.http.get<Paint>(`${this.getUrlApi()}Paint/${id}`);
  }

  active(entity) {
    return this.post('Paint/active', entity);
}

getByModel(filter: any) {
  return this.postAll('Paint/getByModel', filter);
}

}

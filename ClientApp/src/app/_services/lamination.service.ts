import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Lamination } from '../_models/lamination-model';

@Injectable({ providedIn: 'root' })
export class LaminationService extends GenericHttpService<Lamination> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('Lamination/filter', filter);
    }

    save(entity) {
      return this.post('Lamination/save', entity);
   }

    deleteById(id) {
          return this.delete(`Lamination/${id}`);
    }

    getById(id: any) {
      return this.http.get<Lamination>(`${this.getUrlApi()}Lamination/${id}`);
  }

  getAll() {
    return this.http.get<Lamination[]>(`${this.getUrlApi()}Lamination/getAll`);
}

active(entity) {
  return this.post('Lamination/active', entity);
}


}

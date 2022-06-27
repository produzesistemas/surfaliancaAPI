import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Stringer } from '../_models/stringer-model';

@Injectable({ providedIn: 'root' })
export class StringerService extends GenericHttpService<Stringer> {
private obj: Stringer = new Stringer();
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('stringer/filter', filter);
    }

    save(entity) {
      return this.post('stringer/save', entity);
   }

    deleteById(id) {
          return this.delete(`stringer/${id}`);
    }

    getById(id: any) {
      return this.http.get<Stringer>(`${this.getUrlApi()}stringer/${id}`);
  }

  getAll() {
    return this.http.get<Stringer[]>(`${this.getUrlApi()}Stringer/getAll`);
}

active(entity) {
  return this.post('Stringer/active', entity);
}


}

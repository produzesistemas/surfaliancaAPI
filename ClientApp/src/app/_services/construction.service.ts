import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Construction } from '../_models/construction-model';

@Injectable({ providedIn: 'root' })
export class ConstructionService extends GenericHttpService<Construction> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('Construction/filter', filter);
    }

    getAll() {
      return this.http.get<Construction[]>(`${this.getUrlApi()}Construction/getAll`);
  }


    save(entity) {
      return this.post('Construction/save', entity);
   }

    deleteById(id) {
          return this.delete(`Construction/${id}`);
    }

    getById(id: any) {
      return this.http.get<Construction>(`${this.getUrlApi()}Construction/${id}`);
  }
  active(entity) {
    return this.post('Construction/active', entity);
  }


}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { Logo } from '../_models/logo-model';

@Injectable({ providedIn: 'root' })
export class LogoService extends GenericHttpService<Logo> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('Logo/filter', filter);
    }

    getAll() {
      return this.http.get<Logo[]>(`${this.getUrlApi()}Logo/getAll`);
    }

    save(entity) {
      return this.post('Logo/save', entity);
   }

    deleteById(id) {
          return this.delete(`Logo/${id}`);
    }

    getById(id: any) {
      return this.http.get<Logo>(`${this.getUrlApi()}Logo/${id}`);
  }

  active(entity) {
    return this.post('Logo/active', entity);
}
}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { GenericHttpService } from './genericHttpService';
import { Tail } from '../_models/tail-model';

@Injectable({ providedIn: 'root' })
export class TailService extends GenericHttpService<Tail>{
    protected baseUrl = `${environment.urlApi}`;

    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
        return this.postAll('Tail/filter', filter);
      }

      save(entity) {
        return this.post('Tail/save', entity);
     }

      deleteById(id) {
            return this.delete(`Tail/${id}`);
      }

      getById(id: any) {
        return this.http.get<Tail>(`${this.getUrlApi()}Tail/${id}`);
    }

    getAll() {
      return this.http.get<Tail[]>(`${this.getUrlApi()}Tail/getAll`);
  }
  active(entity) {
    return this.post('Tail/active', entity);
  }


}


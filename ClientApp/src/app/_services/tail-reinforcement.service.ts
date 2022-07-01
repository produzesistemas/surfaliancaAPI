import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { TailReinforcement } from '../_models/tail-reinforcement-model';

@Injectable({ providedIn: 'root' })
export class TailReinforcementService extends GenericHttpService<TailReinforcement> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('TailReinforcement/filter', filter);
    }

    getAllConstruction() {
      return this.http.get<TailReinforcement[]>(`${this.getUrlApi()}TailReinforcement/getAll`);
    }


    save(entity) {
      return this.post('TailReinforcement/save', entity);
   }

    deleteById(id) {
          return this.delete(`TailReinforcement/${id}`);
    }

    getById(id: any) {
      return this.http.get<TailReinforcement>(`${this.getUrlApi()}TailReinforcement/${id}`);
  }
  active(entity) {
    return this.post('TailReinforcement/active', entity);
  }

  getAll() {
    return this.http.get<TailReinforcement[]>(`${this.getUrlApi()}TailReinforcement/getAll`);
}


}

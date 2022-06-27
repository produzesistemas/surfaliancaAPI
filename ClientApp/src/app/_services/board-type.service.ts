import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { BoardType } from '../_models/board-type-model';

@Injectable({ providedIn: 'root' })
export class BoardTypeService extends GenericHttpService<BoardType> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('boardType/filter', filter);
    }

    save(entity) {
      return this.post('boardType/save', entity);
   }

    deleteById(id) {
          return this.delete(`boardType/${id}`);
    }

    getById(id: any) {
      return this.http.get<BoardType>(`${this.getUrlApi()}boardType/${id}`);
  }

  getAll() {
    return this.http.get<BoardType[]>(`${this.getUrlApi()}BoardType/getAll`);
}

}

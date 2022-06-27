import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { BoardModel } from '../_models/board-model-model';

@Injectable({ providedIn: 'root' })
export class BoardModelService extends GenericHttpService<BoardModel> {
    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
      return this.postAll('boardModel/filter', filter);
    }

    getBoardModelByLogo(filter: any) {
      return this.postAll('boardModel/getBoardModelByLogo', filter);
    }


    save(entity: FormData) {
      return this.post('boardModel/save', entity);
   }

    deleteById(id) {
          return this.delete(`boardModel/${id}`);
    }

    getById(id: any) {
      return this.http.get<BoardModel>(`${this.getUrlApi()}boardModel/${id}`);
  }

  getToOrder(filter: any) {
    return this.post('boardModel/getToOrder', filter);
}

  getSizes(filter: any) {
    return this.postAll('boardModel/getSizes', filter);
  }

  getWidths(filter: any) {
    return this.postAll('boardModel/getWidths', filter);
  }

  getLitigations(filter: any) {
    return this.postAll('boardModel/getLitigations', filter);
  }

  getAll() {
    return this.http.get<BoardModel[]>(`${this.getUrlApi()}boardModel/getAll`);
}
active(entity) {
  return this.post('boardModel/active', entity);
}


}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { GenericHttpService } from './genericHttpService';
import { Bottom } from '../_models/bottom-model';

@Injectable({ providedIn: 'root' })
export class BottomService extends GenericHttpService<Bottom>{
    protected baseUrl = `${environment.urlApi}`;

    constructor(private http: HttpClient) {
        super(http);
    }

    getByFilter(filter: any) {
        return this.postAll('Bottom/filter', filter);
      }
  
      save(store: FormData) {
        return this.post('Bottom/save', store);
    }
  
      deleteById(id) {
            return this.delete(`Bottom/${id}`);
      }
  
      getById(id: any) {
        return this.http.get<Bottom>(`${this.getUrlApi()}Bottom/${id}`);
    }

}


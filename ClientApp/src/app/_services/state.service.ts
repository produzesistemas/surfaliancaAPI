import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { State } from '../_models/state-model';

@Injectable({ providedIn: 'root' })
export class StateService extends GenericHttpService<State> {
    constructor(private http: HttpClient) {
        super(http);
    }

  getAll() {
    return this.http.get<State[]>(`${this.getUrlApi()}state/getAll`);
}


}

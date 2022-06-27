import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { GenericHttpService } from './genericHttpService';
import { Store } from 'src/app/_models/store';
import { ApplicationUser } from 'src/app/_models/application-user';
import { BehaviorSubject } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class StoreService extends GenericHttpService<any>{
    protected baseUrl = `${environment.urlApi}`;

    constructor(private http: HttpClient) {
        super(http);
    }

    get() {
        return this.http.get<Store>(`${this.getUrlApi()}store`);
    }

    getToIndex() {
        return this.http.get<Store>(`${this.getUrlApi()}store/getToIndex`);
    }

    save(store: FormData) {
        return this.post('store/save', store);
    }

    loadStoreSelected() {
        return new BehaviorSubject<any>(JSON.parse(localStorage.getItem('surfalianca_store'))).getValue();
    }

    removeStoreSelected() {
        localStorage.removeItem('surfalianca_store');
    }

    addStoreSelected(loja: any) {
        localStorage.setItem('surfalianca_store', JSON.stringify(loja));
    }

    sendMessage(entity) {
        return this.post('store/sendMessage', entity);
     }


}


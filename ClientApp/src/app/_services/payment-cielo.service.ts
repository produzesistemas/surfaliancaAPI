import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { environment } from 'src/environments/environment';
@Injectable({ providedIn: 'root' })

export class PaymentCieloService extends GenericHttpService<any> {
    // protected baseUrl = `${environment.urlSandboxRequisicaoCielo}`;
    constructor(private http: HttpClient) {
        super(http);
    }

    save(entity) {
        return this.sendCielo(entity);
     }

     getPaymentCielo(id) {
         return this.getCielo(id);
     }
}

import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { GenericHttpService } from './genericHttpService';
import { environment } from 'src/environments/environment';
import { ResponseShippingDeadline } from '../_models/response-shipping-deadline-model';
@Injectable({ providedIn: 'root' })

export class PostOfficesService extends GenericHttpService<any> {
  private errors: any[] = [];
    constructor(private http: HttpClient) {
        super(http);
    }

    calculateFreight(filter: any) {
        return this.postAll('order/calculateFreight', filter);
      }

      searchAddress(filter: any) {
        return this.postAll('order/searchAddress', filter);
      }

}

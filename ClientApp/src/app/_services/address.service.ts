import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })

export class AddressService {

    constructor() {

    }

  load() {
      return new BehaviorSubject<any>(JSON.parse(localStorage.getItem('srf_address'))).getValue();
  }

  update(item) {
    localStorage.removeItem('srf_address');
    localStorage.setItem('srf_address', JSON.stringify(item));
  }

  clear() {
    localStorage.removeItem('srf_address');
  }

}

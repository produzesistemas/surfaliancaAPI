import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { StoreService } from '../_services/store.service';

@Component({
    selector: 'app-delivery-policy-management.component',
    templateUrl: './delivery-policy-management.component.html'
})

export class DeliveryPolicyManagementComponent implements OnInit {
    public loja: any;
    constructor( 
                 private router: Router,
                 private storeService: StoreService) {
    }

    ngOnInit() {
        this.loja = this.storeService.loadStoreSelected();
        if (this.loja === null) {
            return this.router.navigate(['/store-category-product']);
            }
    }

    



}


import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';
import { StoreService } from 'src/app/_services/store.service';
import { environment } from 'src/environments/environment';
// import { ChangePasswordFormComponent } from '../../pages/login/change-password-form/change-password-form.component';
// import { AuthenticationService } from '../../services/authentication.service';
// import { User } from '../../models/user';

@Component({
	selector: 'app-top-bar',
	templateUrl: './top-bar.component.html'
})
export class TopBarComponent implements OnInit {

	public currentUser;
	public store;
    public shoppingCart: any[] = [];
	constructor(
		private authenticationService: AuthenticationService,
		private shoppingCartService: ShoppingCartService,
    private router: Router,
    private toastr: ToastrService,
    private storeService: StoreService) {

	}

	ngOnInit() {
		this.currentUser = this.authenticationService.getCurrentUser();
		this.shoppingCart = this.shoppingCartService.loadCart();
	  }
	
	  logout() {
		this.authenticationService.logout();
		this.currentUser = null;
		return this.router.navigate(['/index']);
		
	}
	
	logged() {
		if (this.currentUser === null) {
			return false;
		} else {
			return true;
		}
	}
	
	clientArea() {
		return this.router.navigate(['/client-area/order']);
	}
	
	onLogin() {
		return this.router.navigate(['/login/0']);
	}
	
	getQuantityItems() {
        if (this.shoppingCart !== null) {
            return this.shoppingCart.map(x => {
                return x
            }).reduce((sum, current) => sum + (current ? current.quantity : 0), 0);
        } else {
            return 0;
        }
    }
    
    openShoppingCart() {
        if ((this.shoppingCart === null) ||
            (this.shoppingCart === undefined) ||
            (this.shoppingCart.length === 0)) {
            return this.toastr.error('O Carrinho está vazio. Adicione produtos');
        }
        this.router.navigate(['/shoppingcart']);
    }

	// filterProductType(Id) {
    //     const filter: FilterDefaultModel = new FilterDefaultModel();
    //     filter.id = Id;
    //     this.productService.getByType(filter).subscribe((result) => {
    //     this.lstProducts = result;
    //     this.isFilter = true;
    //     });
    // }

}

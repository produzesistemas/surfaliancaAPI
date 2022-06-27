import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { AuthenticationService } from './_services/authentication.service';
import { StoreService } from './_services/store.service';
import { ShoppingCartService } from './_services/shopping-cart.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  modalRef: BsModalRef;
	public currentUser;
  public store;
  title = 'Surf Alianca Store';
  public shoppingCart: any[] = [];

  constructor(
		private authenticationService: AuthenticationService,
    private router: Router,
    private toastr: ToastrService,
    private storeService: StoreService,
	private shoppingCartService: ShoppingCartService) {

	}

  ngOnInit() {
		this.currentUser = this.authenticationService.getCurrentUser();
		this.shoppingCart = this.shoppingCartService.loadCart();
	}

	closeModal() {
		this.modalRef.hide();
	}

	logout() {
		this.authenticationService.logout();
		
	}
	
	logged() {
		if (this.currentUser === null) {
			return false;
		} else {
			return true;
		}
	}
	
	clientArea() {
		return this.router.navigate(['/clientarea/order']);
	}
	
	onLogin() {
		return this.router.navigate(['/login/0']);
	}
	
	getLogo() {
	  this.store = this.storeService.loadStoreSelected();
	  return environment.urlImagesLojas + this.store.imageName;
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
}

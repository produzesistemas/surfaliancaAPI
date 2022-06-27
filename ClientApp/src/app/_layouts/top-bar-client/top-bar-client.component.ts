import { Component, OnInit, Input, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { StoreService } from 'src/app/_services/store.service';
import { environment } from 'src/environments/environment';
// import { ChangePasswordFormComponent } from '../../pages/login/change-password-form/change-password-form.component';
// import { AuthenticationService } from '../../services/authentication.service';
// import { User } from '../../models/user';

@Component({
	selector: 'app-top-bar-client',
	templateUrl: './top-bar-client.component.html'
})
export class TopBarClientComponent implements OnInit {

	public currentUser;
	public store;
    public shoppingCart: any[] = [];
	constructor(
		private authenticationService: AuthenticationService,
    private router: Router,
    private toastr: ToastrService,
    private storeService: StoreService) {

	}

	ngOnInit() {
		this.currentUser = this.authenticationService.getCurrentUser();
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
	
	onLogin() {
		return this.router.navigate(['/login/0']);
	}
	
   
	
}

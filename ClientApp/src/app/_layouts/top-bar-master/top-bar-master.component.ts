import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-top-bar-master',
  templateUrl: './top-bar-master.component.html'
})
export class TopBarMasterComponent implements OnInit {

  public menus: any[];
  public currentUser;
  constructor(  private authenticationService: AuthenticationService,
                private router: Router) { }

  ngOnInit() {
    this.currentUser = this.authenticationService.getCurrentUser();
    if (!this.currentUser) {
      this.logout();
      return this.router.navigate(['index']);
    }
  }

  logout() {
    this.authenticationService.logout();
    return this.router.navigate(['index']);
  }

}

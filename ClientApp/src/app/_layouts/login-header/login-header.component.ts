import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'login-header',
  templateUrl: './login-header.component.html'
})
export class LoginHeaderComponent implements OnInit {

  public currentUser;
  constructor(  private authenticationService: AuthenticationService,
                private router: Router) { }

  ngOnInit() {
    this.currentUser = this.authenticationService.getCurrentUser();
    if (this.currentUser) {
        this.router.navigate(['partner-area']);
    }
  }
}

import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-app-layout',
  templateUrl: './app-layout.component.html'
})
export class AppLayoutComponent implements OnInit {
  public currentUser;

  constructor(private authenticationService: AuthenticationService,
              private router: Router) { }

  ngOnInit() {


  }

}

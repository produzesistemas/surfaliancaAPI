import { Component, OnInit } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';


@Component({
    selector: 'app-partner-area-management',
    templateUrl: './partner-area-management.component.html'
})

export class PartnerAreaManagementComponent implements OnInit {

    constructor( private toastr: ToastrService,
                 private router: Router) {
    }

    ngOnInit() {

}

}


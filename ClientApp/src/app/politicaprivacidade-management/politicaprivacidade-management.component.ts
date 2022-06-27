import { Component, OnInit } from '@angular/core';
import {Location} from '@angular/common';

@Component({
    selector: 'app-politica-privacidade',
    templateUrl: './politicaprivacidade-management.component.html'
})

export class PoliticaPrivacidadeComponent implements OnInit {

    constructor(private _location: Location) {
    }

    ngOnInit() {
  }

  onBack() {
    this._location.back();
  }
  

}


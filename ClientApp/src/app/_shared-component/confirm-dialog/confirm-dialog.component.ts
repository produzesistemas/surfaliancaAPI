import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confir-dialog',
  templateUrl: './confirm-dialog.component.html',
})
export class ConfirmationDialogComponent implements OnInit {

  @Input() title: string;
  @Input() message: string;
  @Input() btnOkText: string;
  @Input() btnCancelText: string;

  // tslint:disable-next-line:no-output-native
  @Output() result = new EventEmitter();

  constructor(private activeModal: BsModalRef) { }

  ngOnInit() {
  }

  public decline() {
    this.result.emit(false);
    this.activeModal.hide();
  }

  public accept() {
    this.result.emit(true);
    this.activeModal.hide();
  }

  public dismiss() {
    this.activeModal.hide();
  }

}

import { Injectable } from '@angular/core';
import { BsModalService } from 'ngx-bootstrap/modal';
import { ConfirmationDialogComponent } from './confirm-dialog.component';

@Injectable({ providedIn: 'root' })
export class ConfirmationDialogService {

  constructor(private modalService: BsModalService) { }

  public confirm(
    title: string,
    message: string,
    btnOkText: string = 'OK',
    btnCancelText: string = 'Cancel',
    dialogSize: 'md'|'sm'|'lg' = 'sm'): Promise<boolean> {

    const init = {
        title, message, btnOkText, btnCancelText
    };
    const modalRef = this.modalService.show(ConfirmationDialogComponent, {  initialState: init, class: `modal-${dialogSize}` });
    const promise = new Promise<boolean>((resolve, reject) => {
      modalRef.content.result.subscribe((confirmed) => {
        if (confirmed) {
          resolve(true);
        }
      });
    });

    return promise;
  }

}

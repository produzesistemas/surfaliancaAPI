import { ToastrService } from 'ngx-toastr';
import { Injector } from '@angular/core';
import { GenericHttpService } from '../../_services/genericHttpService';
import { ConfirmationDialogService } from '../confirm-dialog/confirm-dialog.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AuthenticationService } from 'src/app/_services/authentication.service';

export abstract class BaseResourceListComponent<T> {

  resources: T[] = [];
  protected modalRef: BsModalRef;
  protected modalService: BsModalService;
  protected confirmationDialog: ConfirmationDialogService;
  protected toast: ToastrService;

  constructor(
    protected injector: Injector,
    protected resourceService: GenericHttpService<T>,
    protected url: string,
    protected resource: T,
    protected user: T
  ) {
    this.confirmationDialog = injector.get(ConfirmationDialogService);
    this.modalRef = injector.get(BsModalRef);
    this.modalService = injector.get(BsModalService);
    this.toast = injector.get(ToastrService);
  }

  edit(resource: T, component: any, styleClass?: string) {

    if (styleClass === undefined || styleClass === '' || styleClass === null) {
      styleClass = 'modal-md';
    }
    const initialState = {
      resource,
      isEdit: true,
      currentAction: 'edit'
    };
    this.modalRef = this.modalService.show(component, { initialState, class: styleClass });
    this.modalRef.content.action.subscribe(result => {
      if (result) {
        this.update();
      }
    });
  }

  view(resource: T, component: any, styleClass?: string) {
    if (typeof styleClass === 'undefined' || styleClass === '') {
      styleClass = 'modal-md';
    }
    const initialState = {
      resource,
      isEdit: false,
      currentAction: 'view'
    };
    this.modalRef = this.modalService.show(component, { initialState, class: styleClass });
  }

  onNew(component: any, styleClass?: string ){
    if (typeof styleClass === 'undefined' || styleClass === '') {
      styleClass = 'modal-md';
    }
    const initialState = {
      currentAction: 'new'
    };
    this.modalRef = this.modalService.show(component, {initialState, class: styleClass });
    this.modalRef.content.action.subscribe ((result) => {
      if (result) {
        this.update();
      }
    });
  }

  update() {

    this.resourceService.postAll(this.url + '/filter', this.user).subscribe(
      result => {
        this.resources = result;
      }
    );
  }

//   delete(id: number, formName: string) {
//        this.confirmationDialog
//       .confirm(
//         'Exclusão de ' + formName,
//         'Tem certeza que deseja excluir este ' + formName + '?',
//         'Sim',
//         'Não',
//         'sm'
//       )
//       .then(
//         () => {
//           this.resourceService.delete(this.url + '/' + id).
//           subscribe(
//             result => {
//               this.update();
//               this.toast.success('Registro excluído com sucesso!');
//             }
//           );
//         }
//       );
//     }

}


import { OnInit, Injector, Output, EventEmitter } from '@angular/core';
import {FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router} from '@angular/router';
import { GenericHttpService } from 'src/app/_services/genericHttpService';
import { BsModalRef } from 'ngx-bootstrap/modal';

export abstract class BaseResourceFormComponent <T> implements OnInit {

  resourceForm: FormGroup;
  currentAction: string;
  public route: ActivatedRoute;
  public router: Router;
  public formBuilder: FormBuilder;
  public modalRef: BsModalRef;
  public isEdit: boolean;
  public disabled: boolean;
  @Output() action = new EventEmitter();

  constructor(
    protected injector: Injector,
    public resource: T,
    protected resourceService: GenericHttpService<T>,
    protected url: string,
    protected jsonDataToResourceFn: (jsonData) => T
  ) {
    this.route = injector.get(ActivatedRoute);
    this.router = injector.get(Router);
    this.formBuilder = injector.get(FormBuilder);
    this.modalRef = injector.get(BsModalRef);
  }

  ngOnInit() {
    this.setCurrentAction();
    this.buildResourceForm();
    this.loadResource();
  }

  update() {
    this.update();
  }

  protected setCurrentAction() {
    if (typeof this.currentAction === 'undefined' || this.currentAction == null || this.currentAction == '') {
      if (this.isEdit === false) {
        this.currentAction = 'new';
      } else {
        this.currentAction = 'edit';
      }
    }
  }

  protected loadResource() {
    if (this.currentAction === 'edit') {
      this.resourceForm.patchValue(this.resource);
    }
  }

  set isDisabled(value: boolean) {
    this.isEdit = !value;
    this.disabled = value;
  }

  protected abstract buildResourceForm(): void;

}

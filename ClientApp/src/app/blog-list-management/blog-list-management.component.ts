import { Component, OnInit, TemplateRef, Output, EventEmitter, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import * as moment from 'moment';
import { environment } from '../../environments/environment';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { BlogService } from 'src/app/_services/blog.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { ShoppingCartService } from 'src/app/_services/shopping-cart.service';

@Component({
  selector: 'app-blog-list-management',
  templateUrl: './blog-list-management.component.html'
})

export class BlogListManagementComponent implements OnInit {
  modalRef: BsModalRef;
  modalDelete: BsModalRef;
  lstBlogs = [];
  blog: any;
  public shoppingCart: any[] = [];

  @ViewChild('modal') public templateref: TemplateRef<any>;


  constructor(
    private modalService: BsModalService,
    private toastr: ToastrService,
    private authenticationService: AuthenticationService,
    private blogService: BlogService,
    private router: Router,
    private shoppingCartService: ShoppingCartService,

  ) {
  }

  ngOnInit() {
    this.onSubmit();
  }


  onSubmit() {
    const filter: FilterDefaultModel = new FilterDefaultModel();
    this.blogService.getAllByFilter(filter).subscribe(
      result => {
        this.lstBlogs = result;
      }
    );
  }

  getImage(nomeImage) {
    return environment.urlImagesLojas + nomeImage;
}

closeModal() {
  this.modalRef.hide();
}

openModal(blog) {
  this.blog = blog;
      this.modalRef = this.modalService.show(this.templateref, { class: 'modal-xl' });
}

getQuantityItems() {
  if (this.shoppingCart !== null) {
      return this.shoppingCart.map(x => {
          return x
      }).reduce((sum, current) => sum + (current ? current.quantity : 0), 0);
  } else {
      return 0;
  }
}
openShoppingCart() {
  if ((this.shoppingCart === null) ||
      (this.shoppingCart === undefined) ||
      (this.shoppingCart.length === 0)) {
      // return this.toastr.error('O Carrinho está vazio. Adicione produtos');
  }
  this.router.navigate(['/shoppingcart']);
}



}

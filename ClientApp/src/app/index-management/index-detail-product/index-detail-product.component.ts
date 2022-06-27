import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { ProductService } from 'src/app/_services/product.service';
import { ToastrService } from 'ngx-toastr';
import { environment } from 'src/environments/environment';
import { Product } from 'src/app/_models/product-model';
import { ShoppingCartService } from '../../_services/shopping-cart.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FilterDefaultModel } from 'src/app/_models/filter-default-model';

@Component({
  selector: 'app-index-detail-product',
  templateUrl: './index-detail-product.component.html'
})
export class IndexDetailProductComponent implements OnInit {
  modalRef: BsModalRef;
  formAdd: FormGroup;
  submitted = false;
  public product: any = {};
  public shoppingCart: any[] = [];
  public itemCart;
  @ViewChild('modal') public templateref: TemplateRef<any>;
  constructor(
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private router: Router,
    private toastr: ToastrService,
    private shoppingCartService: ShoppingCartService,
    private route: ActivatedRoute,
    private productService: ProductService
  ) { }

  get f() { return this.formAdd.controls; }

  ngOnInit() {
    this.formAdd = this.formBuilder.group({
      id: [0],
      description: ['', [Validators.required]],
      value: ['', [Validators.required]],
    });

    this.route.params.subscribe(params => {
      if (params.id > 0) {
        this.product.id = Number(params.id);
        this.load();
      }
    });
  }

  load() {
    const filter: FilterDefaultModel = new FilterDefaultModel();
    filter.id = this.product.id;
     this.productService.getDetails(filter).subscribe(product => {
      this.product = product;
      this.setControls(this.product);
    });
  }

  setControls(item: Product) {
    this.formAdd.controls.id.setValue(item.id);
    this.formAdd.controls.description.setValue(item.description);
    this.formAdd.controls.value.setValue(item.value);
  }

  onCancel() {
    this.router.navigate(['/index']);
  }

getImage(nomeImage) {
  return environment.urlImagesProducts + nomeImage;
}

addShoppingCart(product) {
  this.shoppingCart = this.shoppingCartService.loadCart();
  if (this.shoppingCart !== null) {
      const item = this.shoppingCart.find(x => x.productId === product.id);
      if (item) {
          item.quantity = item.quantity + 1;
          this.shoppingCartService.updateCart(this.shoppingCart);
          this.openModal();
      } else {
          this.itemCart = {};
          this.itemCart.productId = product.id;
          this.itemCart.quantity = 1;
          this.itemCart.name = product.name;
          this.itemCart.productTypeId = product.productTypeId;
          this.itemCart.productStatusId = product.productStatusId;
          this.itemCart.typeSaleId = product.typeSaleId;
          if (product.isPromotion) {
            this.itemCart.value = product.valuePromotion;
          } else {
            this.itemCart.value = product.value;
          }

          this.itemCart.imageName = product.imageName;
          this.shoppingCart.push(this.itemCart);
          this.shoppingCartService.updateCart(this.shoppingCart);
          this.openModal();
      }
  } else {
      this.shoppingCart = [];
      this.itemCart = {};
      this.itemCart.productId = product.id;
      this.itemCart.quantity = 1;
      this.itemCart.name = product.name;
      this.itemCart.productTypeId = product.productTypeId;
      this.itemCart.productStatusId = product.productStatusId;
      this.itemCart.typeSaleId = product.typeSaleId;
      if (product.isPromotion) {
        this.itemCart.value = product.valuePromotion;
      } else {
        this.itemCart.value = product.value;
      }
      this.itemCart.imageName = product.imageName;
      this.shoppingCart.push(this.itemCart);
      this.shoppingCartService.updateCart(this.shoppingCart);
      this.openModal();
  }
}

        closeModal() {
          this.modalRef.hide();
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


          onContinueBuy() {
            this.closeModal();
            return this.router.navigate([`/index`]);
          }

          openShoppingCart() {
            this.closeModal();
              if ((this.shoppingCart === null) ||
                  (this.shoppingCart === undefined) ||
                  (this.shoppingCart.length === 0)) {
                  return this.toastr.error('O Carrinho está vazio. Adicione produtos');
              }

              this.router.navigate(['/shoppingcart']);
          }

          openModal() {
            this.modalRef = this.modalService.show(this.templateref, { class: 'modal-md' });
          }

}


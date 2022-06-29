import { Component, OnInit, OnDestroy, ViewChild, TemplateRef } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { ShoppingCartService } from '../_services/shopping-cart.service';
import { StoreService } from '../_services/store.service';
import { AuthenticationService } from '../_services/authentication.service';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService } from 'angularx-social-login';
import { ApplicationUser } from '../_models/application-user';
import { Order } from '../_models/order-model';
import { OrderProductOrdered } from '../_models/order-product-ordered-model';
import { OrderProduct } from '../_models/order-product-model';
import { AddressViaCep } from '../_models/address-viacep-model';
import { NgxViacepService } from '@brunoc/ngx-viacep';
import { CouponService } from '../_services/coupon.service';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { OrderService } from '../_services/order.service';
import { PostOfficesService } from '../_services/post-offices.service';

@Component({
  selector: 'app-shopping-cart',
  templateUrl: './shopping-cart.component.html'
})

export class ShoppingCartComponent implements OnInit {

  modalLogin: BsModalRef;
  modalAddress: BsModalRef;
  isSubmitted = false;
  isSubmittedAddress = false;

  public store;
  public cep;
  public currentUser;
  public shoppingCart: any[] = [];
  public itemCart;
  public orderDispatched: any;
  public totalValue: number;
  public coupon: any;
  form: FormGroup;
  formCoupon: FormGroup;
  formFreight: FormGroup;
  formOptionsDelivery: FormGroup;
  public submitted = false;
  public submittedCoupon = false;
  public submittedOptionsDelivery = false;
  public submittedFreight = false;
  public submittedAddress = false;
  public erroCoupon = false;
  public isPostOffices = false;
  public erroMessage;
  public address: AddressViaCep = new AddressViaCep();
  formAddress: FormGroup;
  formPostalCode: FormGroup;
  @ViewChild('modalAddress') public templaterefAddress: TemplateRef<any>;
  @ViewChild('modalLogin', { read: TemplateRef }) templateLogin: TemplateRef<any>;
  isSubmittedPostalCode: boolean;
  currencyPipe: any;


  constructor(private toastr: ToastrService,
    private storeService: StoreService,
    private shoppingCartService: ShoppingCartService,
    private authenticationService: AuthenticationService,
    private viacep: NgxViacepService,
    private couponService: CouponService,
    private postOfficesService: PostOfficesService,
    private formBuilder: FormBuilder,
    private modalService: BsModalService,
    private orderService: OrderService,
    private authService: SocialAuthService,
    private router: Router) {
  }

  get fc() { return this.formCoupon.controls; }
  get fa() { return this.formAddress.controls; }
  get ff() { return this.formFreight.controls; }
    get fd() { return this.formOptionsDelivery.controls; }

  ngOnInit() {
    this.store = this.storeService.loadStoreSelected();
    this.shoppingCart = this.shoppingCartService.loadCart();
    if (!this.shoppingCart) {
      return this.router.navigate([`/index`]);
    }
    this.currentUser = this.authenticationService.getCurrentUser();
    if (this.currentUser) {
      this.orderService.getLastOrderByUser().subscribe(result => {
        if (result) {
          this.setAddressByOrder(result);
        }
      });
    }
    this.form = this.formBuilder.group({
      valueMinimum: ['']
    });
    this.formAddress = this.formBuilder.group({
      description: ['', Validators.required],
      city: ['', Validators.required],
      district: ['', Validators.required],
      reference: [''],
      complement: [''],
      state: ['', Validators.required],
    });

    this.formPostalCode = this.formBuilder.group({
      postalCode: ['', Validators.required],
    });

    this.formFreight = this.formBuilder.group({
      cep: ['', Validators.required]
  });

  this.formOptionsDelivery = this.formBuilder.group({
    freightValue: ['', Validators.required],
    deliveryDate: ['', Validators.required],
    type: ['']
});

    this.formCoupon = this.formBuilder.group({
      coupon: ['', Validators.required]
    });

    this.formOptionsDelivery.controls.deliveryDate.disable();
    this.formOptionsDelivery.controls.freightValue.disable();
  }

  get f() { return this.formAddress.controls; }
  get fp() { return this.formPostalCode.controls; }

  getImage(item) {
    if (item) {
      if (item.typeSaleId === 1) {
        return environment.urlImagesLojas + item.imageName;
      }

      if (item.typeSaleId === 2) {
        return environment.urlImagesProducts + item.imageName;
      }

    }
    return null;
  }

  getTotalSale() {
    let totalValue = this.getTotalItems();
    if (this.coupon) {
      if (this.coupon.tipo) {
        totalValue -= this.coupon.valor;
      } else {
        totalValue -= (totalValue * this.coupon.valor) / 100;
      }
    }

    //  if (this.getValorFrete()) {
    //      totalValue += this.getValorFrete();
    //  }
    return totalValue;
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

  getTotalItems() {
    this.totalValue = 0;
    if (this.shoppingCart !== null) {
    this.shoppingCart.forEach((item) => {
      this.totalValue += (item.value * item.quantity);
    });
  }
    return this.totalValue;
  }

  getValueMinimum() {
    return this.store.valueMinimum;
  }

  clearCart() {
    this.shoppingCartService.clearCart();
    return this.router.navigate(['/index']);
  }


  onContinueBuy() {
    return this.router.navigate([`/index`]);
  }

  finishCart() {
    if (this.checkFinishCart()) {
      this.submitted = true;
      return this.form.controls.valueMinimum.setErrors({ incorrect: true })
    }

    if (!this.currentUser) {
      return this.modalLogin = this.modalService.show(this.templateLogin, { class: 'modal-md' });
    }

    if (this.currentUser) {
      this.onCheckout();
    }

    this.submittedOptionsDelivery = true;
        if (this.formOptionsDelivery.invalid) {
            return;
        }
  }

  checkFinishCart() {
    if (this.store.valueMinimum < this.getTotalItems()) {
      return false;
    }
    return true;
  }

  getSubtotal(item) {
    return item.value * item.quantity;
  }

  getImageStore(nomeImage) {
    return environment.urlImagesLojas + nomeImage;
  }

  deleteItemCart(item) {
    this.shoppingCartService.removeCartProduct(item);
    this.shoppingCart = this.shoppingCartService.loadCart();
  }

  incrementItemCart(item) {
    item.quantity = item.quantity + 1;
    this.shoppingCartService.updateCart(this.shoppingCart);
    this.shoppingCart = this.shoppingCartService.loadCart();
  }

  decrementItemCart(item) {
    item.quantity = item.quantity - 1;
    if (item.quantity === 0) {
      this.deleteItemCart(item);
      return this.shoppingCart = this.shoppingCartService.loadCart();
    }
    this.shoppingCartService.updateCart(this.shoppingCart);
    this.shoppingCart = this.shoppingCartService.loadCart();
  }

  onLogin() {
    return this.modalLogin = this.modalService.show(this.templateLogin, { class: 'modal-md' });
  }

  signInWithGoogle(): void {
    this.authService.signIn(GoogleLoginProvider.PROVIDER_ID).then(socialuser => {
      this.authenticationService.clearUser();
      this.authenticationService.addCurrenUser(socialuser);
      console.log(socialuser);
      const user = new ApplicationUser();
      user.email = socialuser.email;
      user.emailConfirmed = true;
      user.phoneNumberConfirmed = false;
      user.userName = socialuser.firstName;
      user.firstName = socialuser.firstName;
      user.lastName = socialuser.lastName;
      user.provider = 'GOOGLE';
      user.providerId = socialuser.id;
      user.name = socialuser.name;
      user.nomeImagem = socialuser.photoUrl;
      this.authenticationService.registerClient(user)
        .subscribe(
          result => {
            this.authenticationService.clearUser();
            this.authenticationService.addCurrenUser(result);
            this.currentUser = result;
            this.closeModalLogin();
          }
        );
    });
  }

  facebookSignin(): void {
    this.authService.signIn(FacebookLoginProvider.PROVIDER_ID).then(socialuser => {
      this.authenticationService.clearUser();
      this.authenticationService.addCurrenUser(socialuser);
      console.log(socialuser);
      const user = new ApplicationUser();
      user.email = socialuser.email;
      user.emailConfirmed = true;
      user.phoneNumberConfirmed = false;
      user.userName = socialuser.firstName;
      user.firstName = socialuser.firstName;
      user.lastName = socialuser.lastName;
      user.provider = 'Facebook'
      user.providerId = socialuser.id;
      user.name = socialuser.name;
      user.nomeImagem = socialuser.photoUrl;
      this.authenticationService.registerClient(user)
        .subscribe(
          result => {
            this.authenticationService.clearUser();
            this.authenticationService.addCurrenUser(result);
            this.currentUser = result;
            this.closeModalLogin();
          }
        );
    });
  }

  closeModalLogin() {
    this.modalLogin.hide();
  }

  resetAddress() {
    this.formAddress.controls.description.reset();
    this.formAddress.controls.city.reset();
    this.formAddress.controls.district.reset();
    this.formAddress.controls.reference.reset();
    this.formAddress.controls.complement.reset();
    this.formPostalCode.controls.postalCode.reset();
    this.formAddress.controls.state.reset();
  }

  closeModalAddress() {
    this.modalAddress.hide();
  }

  openModalAddress() {
    this.modalAddress = this.modalService.show(this.templaterefAddress, { class: 'modal-md' });
  }

  setAddress(address) {
    this.formAddress.controls.description.setValue(address.logradouro);
    this.formAddress.controls.district.setValue(address.bairro);
    this.formAddress.controls.city.setValue(address.localidade);
    this.formAddress.controls.state.setValue(address.uf);
    this.formAddress.controls.complement.setValue(address.complemento);
    this.formAddress.controls.reference.setValue(address.referencia);
    this.formPostalCode.controls.postalCode.setValue(address.cep);
  }

  setAddressByOrder(order) {
    this.address.logradouro = order.address;
    this.address.bairro =  order.district;
    this.address.localidade = order.city;
    this.address.uf = order.state;
    this.address.complemento = order.complement;
    this.address.referencia = order.reference;
    this.address.cep = order.postalCode;
  }

  onSearchLocation() {
    this.isSubmittedPostalCode = true;
    if (this.formPostalCode.invalid) {
      return;
    }
    this.viacep.buscarPorCep(this.formPostalCode.controls.postalCode.value).then(result => {
      if (result !== undefined) {
        this.isSubmittedPostalCode = false;
        this.setAddress(result);
      }
    });

  }

  onConfirmAddress() {
    this.isSubmittedAddress = true;
    this.isSubmittedPostalCode = true;
    if (this.formAddress.invalid) {
      return;
    }
    if (this.formPostalCode.invalid) {
      return;
    }
    this.address.logradouro = this.formAddress.controls.description.value;
    this.address.localidade = this.formAddress.controls.city.value;
    this.address.bairro = this.formAddress.controls.district.value;
    this.address.cep = this.formPostalCode.controls.postalCode.value;
    this.address.complemento = this.formAddress.controls.complement.value;
    this.address.referencia = this.formAddress.controls.reference.value;
    this.address.uf = this.formAddress.controls.state.value;
    this.closeModalAddress();
  }

  searchCoupon() {
    this.erroCoupon = false;
    this.submittedCoupon = true;
    if (this.formCoupon.invalid) {
      return;
    }
    if (this.logged()) {
      this.onLogin();
    } else {
      let filter: FilterDefaultModel = new FilterDefaultModel();
      filter.name = this.formCoupon.controls.coupon.value;
      this.couponService.getByCodigo(filter).subscribe(coupon => {
        if (coupon === null) {
          this.toastr.error('Cupom não encontrado!')
        } else {

          if (this.getTotalItems() < coupon.valueMinimum) {
            return this.toastr.error('O valor mínimo do pedido para uso deste cupom é de: ' + this.currencyPipe.transform(coupon.valueMinimum, 'R$'));
          }
          this.coupon = coupon;
          console.log(this.coupon);
          return this.toastr.success('Cupom aplicado com sucesso!');

        }
      });
    }

  }

  logged() {
    if (this.currentUser) {
      return false;
    } else {
      return true;
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

  onCheckout() {
    this.isSubmitted = true;
    if (!this.address.logradouro ||
      !this.address.localidade ||
      !this.address.bairro ||
      !this.address.cep) {
      return this.toastr.error('Informe o endereço de entrega!');
    }

    // if (this.getTotalItems() < this.getValueMinimum()) {
    //   this.form.controls['valueMinimum'].setErrors({ 'incorrect': true });
    //   return;
    // }

    let order = new Order();
    order.address = this.address.logradouro;
    order.city = this.address.localidade;
    order.complement = this.address.complemento;
    order.district = this.address.bairro;
    // order.paymentConditionId = 1;
    order.postalCode = this.address.cep;
    order.state = this.address.uf;
    order.taxValue = 0;
    order.reference = this.address.referencia;
    order.deliveryDate = new Date();
    this.shoppingCart.forEach(cart => {
      if (cart.typeSaleId === 1) {
        let opo = new OrderProductOrdered();
        opo.qtd = cart.quantity;
        opo.value = cart.value;
        opo.boardModelId = cart.boardModelId;
        opo.paintId = cart.paintId;
        opo.constructionId = cart.constructionId;
        opo.laminationId = cart.laminationId;
        opo.tailId = cart.tailId;
        opo.levelId = cart.levelId;
        opo.orderId = cart.orderId;
        opo.otherDimensions = cart.otherDimensions;
        opo.boardModelDimensionId = cart.boardModelDimensionId;
        order.orderProductOrdered.push(opo);
      }
      if (cart.typeSaleId === 2) {
        let op = new OrderProduct();
        op.productId = cart.productId;
        op.productValue = cart.value;
        op.qtd = cart.quantity;
        order.orderProduct.push(op);
      }
    });
    this.orderService.save(order).subscribe(result => {
      if (result) {
        this.orderDispatched = result;
        this.shoppingCartService.clearCart();
        this.router.navigate([`shoppingcart/${this.orderDispatched.id}/0`]);

      }
    });
  }

  checkPostOffices() {
    return this.isPostOffices;
}

getShippingDeadline() {
  this.submittedFreight = true;
  if (this.formFreight.invalid) {
      return;
  }
}


getShippingDeadlineByCode(evt) {
  this.submittedFreight = true;
  if (this.formFreight.invalid) {
      return;
  }
}

getDeliveryForecast() {
  return this.formOptionsDelivery.controls.deliveryDate.value;
}

getFreightValue() {
  return this.formOptionsDelivery.controls.freightValue.value;
}

clearField() {
  this.formOptionsDelivery.controls.deliveryDate.reset();
  this.formOptionsDelivery.controls.freightValue.reset();
  this.formOptionsDelivery.controls.type.reset();
  this.isPostOffices = false;
  this.formAddress.controls.logradouro.reset();
  this.formAddress.controls.cidade.reset();
  this.formAddress.controls.uf.reset();
  this.formAddress.controls.pontoReferencia.reset();
}



}


import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { StoreService } from '../_services/store.service';
import { ProductService } from '../_services/product.service';
import { BoardModelService } from '../_services/board-model.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { FilterDefaultModel } from '../_models/filter-default-model';
import { forkJoin } from 'rxjs';
import { BlogService } from '../_services/blog.service';
import { AuthenticationService } from 'src/app/_services/authentication.service';
import { FacebookLoginProvider, GoogleLoginProvider, SocialAuthService } from 'angularx-social-login';
import { ShoppingCartService } from '../_services/shopping-cart.service';
import { ApplicationUser } from '../_models/application-user';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'app-index-management',
    templateUrl: './index-management.component.html'
})

export class IndexManagementComponent implements OnInit {
    modalRef: BsModalRef;
    modalLogin: BsModalRef;
    modalDeliveryPolicy: BsModalRef;
    modalExchangePolicy: BsModalRef;
    modalWhoWeAre: BsModalRef;
    public shoppingCart: any[] = [];
    public lstProducts = [];
    public lstPromotionSpotlight = [];
    public lstAcessorios = [];
    public lstBoardModels = [];
    public lstBoards = [];
    public itemCart;
    public boardModel: any;
    public store;
    public currentUser;
    public isFilter: boolean = false;
    public lstBlogs = [];
    safeURL: any;
    blog: any;


    @ViewChild('modalBoardModel') public templateref: TemplateRef<any>;
    @ViewChild('modalDeliveryPolicy') public templaterefDeliveryPolicy: TemplateRef<any>;
    @ViewChild('modalExchangePolicy') public templaterefExchangePolicy: TemplateRef<any>;
    @ViewChild('modalWhoWeAre') public templaterefWhoWeAre: TemplateRef<any>;
    @ViewChild('modal') public templaterefBlog: TemplateRef<any>;
    @ViewChild('modalLogin', { read: TemplateRef }) templateLogin: TemplateRef<any>;

    constructor(
        private boardModelService: BoardModelService,
        private modalService: BsModalService,
        private storeService: StoreService,
        private blogService: BlogService,
        private productService: ProductService,
        private _sanitizer: DomSanitizer,
        private authService: SocialAuthService,
        private shoppingCartService: ShoppingCartService,
        private authenticationService: AuthenticationService,
        private router: Router) {

    }

    ngOnInit() {
        if (this.authenticationService.getCurrentUser()) {
            this.authenticationService.getCurrentUser().role === 'Cliente' ? this.currentUser = this.authenticationService.getCurrentUser() : null;
        }
        this.shoppingCart = this.shoppingCartService.loadCart();
        const filter: FilterDefaultModel = new FilterDefaultModel();
        filter.id = 1;
        const filterAc: FilterDefaultModel = new FilterDefaultModel();
        filterAc.id = 2;
        const filterBlog: FilterDefaultModel = new FilterDefaultModel();

        forkJoin(
            this.productService.getByType(filterAc),
            this.storeService.get(),
            this.boardModelService.getAll(),
            this.productService.getByType(filter),
            this.blogService.getAllByFilter(filter)
        ).subscribe((result) => {
            this.lstAcessorios = result[0];
            this.store = result[1];
            this.storeService.addStoreSelected(this.store);
            this.lstBoardModels = result[2];
            this.lstBoards = result[3];
            this.lstBlogs = result[4];

        });
    }





    getImageProduct(nomeImage) {
        return environment.urlImagesProducts + nomeImage;
    }

    getImageBoardModel(nomeImage) {
        return environment.urlImagesLojas + nomeImage;
    }


    getImageBlog(nomeImage) {
        return environment.urlImagesLojas + nomeImage;
    }


    getImageWhoWeAre(nomeImage) {
        return environment.urlImagesLojas + nomeImage;
    }

    onDetail(product) {
            this.router.navigate([`index/${product.id}`]);
    }


    closeModal() {
        this.modalRef.hide();
    }

    closeModalDeliveryPolicy() {
        this.modalDeliveryPolicy.hide();
    }

    closeModalExchangePolicy() {
        this.modalExchangePolicy.hide();
    }

    closeModalWhoWeAre() {
        this.modalWhoWeAre.hide();
    }

    openModalBoardModel(boardModel) {
        this.router.navigate([`order/${boardModel.id}`]);
    }

    openModal(blog) {
        this.blog = blog;
            this.modalRef = this.modalService.show(this.templaterefBlog, { class: 'modal-xl' });
      }


    openModalDeliveryPolicy() {
            this.modalDeliveryPolicy = this.modalService.show(this.templaterefDeliveryPolicy, { class: 'modal-xl' });
    }

    openModalExchangePolicy() {
        this.modalExchangePolicy = this.modalService.show(this.templaterefExchangePolicy, { class: 'modal-xl' });
    }

    openModalWhoWeAre() {
        this.modalWhoWeAre = this.modalService.show(this.templaterefWhoWeAre, { class: 'modal-xl' });
    }

    filterProductType(Id) {
        const filter: FilterDefaultModel = new FilterDefaultModel();
        filter.id = Id;
        this.productService.getByType(filter).subscribe((result) => {
        this.lstProducts = result;
        this.isFilter = true;
        });
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

    logout() {
        this.authenticationService.logout();
        this.currentUser = this.authenticationService.getCurrentUser();
      }

      logged() {
        if (this.currentUser) {
            return false;
        } else {
            return true;
        }
       }

	clientArea() {
		return this.router.navigate(['/client-area/order']);
	}

	// onLogin() {
	// 	return this.router.navigate(['/login/0']);
	// }

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


}

import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { GenericHttpService } from './genericHttpService';
import { Product } from '../_models/product-model';
import { ProductStatus } from '../_models/product-status-model';
import { ProductType } from '../_models/product-type-model';

@Injectable({ providedIn: 'root' })
export class ProductService extends GenericHttpService<Product>{
    protected baseUrl = `${environment.urlApi}`;

    constructor(private http: HttpClient) {
        super(http);
    }

    get(id: any) {
        return this.http.get<Product>(`${this.getUrlApi()}product/${id}`);
    }

    save(store: FormData) {
        return this.post('Product/save', store);
    }

    getByFilter(filter: any) {
        return this.postAll('Product/filter', filter);
      }

      getDetails(filter: any) {
        return this.postAll('Product/getDetails', filter);
      }

      deleteById(id) {
        return this.delete(`Product/${id}`);
      }

    active(entity) {
        return this.post('Product/active', entity);
    }

    getProductStatus() {
        return this.http.get<ProductStatus[]>(`${this.getUrlApi()}Product/getproductStatus`);
    }

    getProductType() {
        return this.http.get<ProductType[]>(`${this.getUrlApi()}Product/getProductType`);
    }

    getAllProduct() {
        return this.http.get<Product[]>(`${this.getUrlApi()}Product/getAllProduct`);
    }

    getPromotionSpotlight() {
        return this.http.get<Product[]>(`${this.getUrlApi()}Product/getPromotionSpotlight`);
    }

    getByType(filter: any) {
        return this.postAll('Product/getByType', filter);
      }

      evaluation(filter: any) {
        return this.postAll('produto/avaliacao', filter);
      }



}


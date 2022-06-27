import { ProductStatus } from "./product-status-model";
import { ProductType } from "./product-type-model";


export class Product {
    description: string;
    name: string;
    id: number;
    value: number;
    imageName: string;
    imageName1: string;
    imageName2: string;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;


    typeSaleId: number;
    productTypeId: number;
    productStatusId: number;

    bottomId?: number;
    shaperId?: number;
    laminationId?: number;
    litigationId?: number;
    boardTypeId?: number;
    boardModelId?: number;
    constructionId?: number;
    sizeId?: number;
    widthId?: number;
    finSystemId?: number;
    tailId?: number;

    valuePromotion?: number;
    isPromotion: boolean;
    isSpotlight: boolean;
    active: boolean;

    productStatus: ProductStatus;
    productType: ProductType;

    public constructor(init?: Partial<Product>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): Product {
        return Object.assign(new Product(), jsonData);
    }

}
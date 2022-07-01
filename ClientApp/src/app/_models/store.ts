import { Product } from './product-model';
export class Store {
    id: number;
    name: string;
    description: string;
    cnpj: string;
    contact: string;
    phone: string;
    exchangePolicy: string;
    deliveryPolicy: string;
    warranty: string;
    valueMinimum: number;
    freeShipping: number;
    numberInstallmentsCard: number;

    street: string;
    district: string;
    number: string;
    postalCode: string;
    city: string;

    offPix: string;
    keyPix: string;

    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;

    promotionAndSpotlight: [] = [];

    public constructor(init?: Partial<Store>) {
        Object.assign(this, init);
    }
}

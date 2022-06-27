import { Product } from './product-model';
export class Store {
    id: number;
    name: string;
    imageName: string;
    description: string;
    cnpj: string;
    contact: string;
    phone: string;
    exchangePolicy: string;
    deliveryPolicy: string;
    aspNetUsersId: string;
    valueMinimum: number;
    numberInstallmentsCard: number;

    street: string;
    district: string;
    number: string;
    cep: string;
    nameCity: string;

    aspNetUsers: any;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;

    promotionAndSpotlight: [] = [];

    public constructor(init?: Partial<Store>) {
        Object.assign(this, init);
    }
}

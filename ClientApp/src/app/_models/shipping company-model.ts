import { ShippingCompanyState } from "./shipping-company-state-model";

export class ShippingCompany {
  name: string;
  value: number;
  id: number;
  imageName: string;
  criadoPor: string;
  alteradoPor: string;
  createDate: Date;
  updateDate: Date;
  shippingCompanyStates: ShippingCompanyState[] = [];

  public constructor(init?: Partial<ShippingCompany>) {
      Object.assign(this, init);
  }

  static fromJson(jsonData: any): ShippingCompany {
      return Object.assign(new ShippingCompany(), jsonData);
  }
}

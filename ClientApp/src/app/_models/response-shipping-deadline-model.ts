export class ResponseShippingDeadline {
  msgError: string;
  error: string;
  valorFreight: number;
  deliveryDate: Date;

  public constructor(init?: Partial<ResponseShippingDeadline>) {
      Object.assign(this, init);
  }
}

export class ShoppingCart {
    productId: number;
    quantity: number;
    value: number;
    obs: string;
    boardModelId?: number;
    productType: number;


    public constructor(init?: Partial<ShoppingCart>) {
        Object.assign(this, init);
    }
}

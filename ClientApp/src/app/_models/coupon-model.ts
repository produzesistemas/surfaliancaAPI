export class Coupon {
    id: number;
    description: string;
    code: string;
    quantity: number;
    clientId?: number;
    type: boolean;
    general: boolean;
    active: boolean;
    value: number;
    valueMinimum: number;
    initialDate: Date;
    finalDate: Date;

    public constructor(init?: Partial<Coupon>) {
        Object.assign(this, init);
    }
}

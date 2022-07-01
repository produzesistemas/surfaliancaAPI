export class TailReinforcement {
    name: string;
    details: string;
    value: number;
    id: number;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;

    public constructor(init?: Partial<TailReinforcement>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): TailReinforcement {
        return Object.assign(new TailReinforcement(), jsonData);
    }
}

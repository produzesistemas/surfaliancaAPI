export class Logo {
    name: string;
    value: number;
    id: number;
    imageName: string;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;

    public constructor(init?: Partial<Logo>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): Logo {
        return Object.assign(new Logo(), jsonData);
    }
}
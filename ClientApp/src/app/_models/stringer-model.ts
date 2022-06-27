
export class Stringer {
    name: string;
    details: string;
    id: number;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;
    imageName: string;
    value:number;


    public constructor(init?: Partial<Stringer>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): Stringer {
        return Object.assign(new Stringer(), jsonData);
    }
}

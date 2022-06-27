export class BorderColor {
    name: string;
    value: number;
    id: number;
    imageName: string;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;

    public constructor(init?: Partial<BorderColor>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): BorderColor {
        return Object.assign(new BorderColor(), jsonData);
    }
}
export class Color {
    name: string;
    id: number;
    value: number;
    imageName: string;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;

    public constructor(init?: Partial<Color>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): Color {
        return Object.assign(new Color(), jsonData);
    }
}
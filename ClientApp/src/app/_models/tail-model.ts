export class Tail {
    id: number;
    name: string;
    imageName: string;

    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;

    public constructor(init?: Partial<Tail>) {
        Object.assign(this, init);
    }

}
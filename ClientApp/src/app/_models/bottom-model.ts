export class Bottom {
    id: number;
    name: string;
    imageName: string;

    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;

    public constructor(init?: Partial<Bottom>) {
        Object.assign(this, init);
    }

}
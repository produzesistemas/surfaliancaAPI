export class Construction {
    name: string;
    details: string;
    value: number;
    id: number;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;
    imageName: string;
    imageName1: string;
    imageName2: string;
    imageName3: string;
    urlMovie: string;


    public constructor(init?: Partial<Construction>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): Construction {
        return Object.assign(new Construction(), jsonData);
    }
}

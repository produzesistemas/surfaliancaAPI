export class Blog {
    typeBlogId: number;
    description: string;
    details: string;
    id: number;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;
    imageName: string;



    public constructor(init?: Partial<Blog>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): Blog {
        return Object.assign(new Blog(), jsonData);
    }
}

export class Paint {
    boardModel(boardModel: any) {
      throw new Error('Method not implemented.');
    }
    name: string;
    value: number;
    id: number;
    imageName: string;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;
    boardModelId: number;

    public constructor(init?: Partial<Paint>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): Paint {
        return Object.assign(new Paint(), jsonData);
    }
}

import { FinSystemColor } from './fin-system-color-model';

export class FinSystem {
    name: string;
    details: string;
    id: number;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;
    imageName: string;


    public constructor(init?: Partial<FinSystem>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): FinSystem {
        return Object.assign(new FinSystem(), jsonData);
    }
}

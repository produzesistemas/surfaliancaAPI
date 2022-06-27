import { BoardModelDimensions } from "./board-model-dimensions-model";

export class BoardModel {
    description: string;
    name: string;
    urlMovie: string;
    id: number;
    logoId: number;
    daysProduction: number;
    value: number;
    logo: string;
    imageName: string;
    imageName1: string;
    imageName2: string;
    imageName3: string;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;
    boardModelDimensions: BoardModelDimensions[] = [];
  boardModelId: any;


    public constructor(init?: Partial<BoardModel>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): BoardModel {
        return Object.assign(new BoardModel(), jsonData);
    }
}

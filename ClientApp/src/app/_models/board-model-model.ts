import { BoardModelBottom } from "./board-model-bottom-model";
import { BoardModelConstruction } from "./board-model-construction-model";
import { BoardModelDimensions } from "./board-model-dimensions-model";
import { BoardModelFinSystem } from "./board-model-fin-system-model";
import { BoardModelLamination } from "./board-model-lamination-model";
import { BoardModelTail } from "./board-model-tail-model";

export class BoardModel {
    description: string;
    name: string;
    urlMovie: string;
    id: number;
    daysProduction: number;
    value: number;
    imageName: string;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;
    boardModelDimensions: BoardModelDimensions[] = [];
    boardModelBottoms: BoardModelBottom[] = [];
    boardModelConstructions: BoardModelConstruction[] = [];
    boardModelLaminations: BoardModelLamination[] = [];
    boardModelTails: BoardModelTail[] = [];
    boardModelFinSystems: BoardModelFinSystem[] = [];

    public constructor(init?: Partial<BoardModel>) {
        Object.assign(this, init);
    }

    static fromJson(jsonData: any): BoardModel {
        return Object.assign(new BoardModel(), jsonData);
    }
}

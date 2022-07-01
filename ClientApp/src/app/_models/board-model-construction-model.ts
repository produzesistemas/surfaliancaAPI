import { Construction } from "./construction-model";

export class BoardModelConstruction {
    id: number; 
    boardModelId: number;
    constructionId: number;
    construction: Construction = new Construction();
}
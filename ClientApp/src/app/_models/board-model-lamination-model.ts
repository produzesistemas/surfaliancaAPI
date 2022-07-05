import { Lamination } from "./lamination-model";

export class BoardModelLamination {
    id: number;
    boardModelId: number;
    laminationId: number;
    lamination: Lamination = new Lamination();
}
import { FinSystem } from "./fin-system-model";

export class BoardModelFinSystem {
    id: number;
    boardModelId: number;
    finSystemId: number;
    finSystem: FinSystem = new FinSystem();
}
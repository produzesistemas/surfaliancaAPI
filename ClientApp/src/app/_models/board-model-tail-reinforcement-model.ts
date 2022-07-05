import { TailReinforcement } from "./tail-reinforcement-model";

export class BoardModelTailReinforcement {
    id: number;
    boardModelId: number;
    tailReinforcementId: number;
    tailReinforcement: TailReinforcement = new TailReinforcement();
}
import { Tail } from "./tail-model";

export class BoardModelTail {
    id: number; 
    boardModelId: number;
    tailId: number;
    tail: Tail = new Tail();
}
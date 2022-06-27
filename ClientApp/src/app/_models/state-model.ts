export class State {
  id: number;
  description: string;
  sg: string;
  isSelected: boolean;
  taxValue: number;

  criadoPor: string;
  alteradoPor: string;
  createDate: Date;
  updateDate: Date;

  public constructor(init?: Partial<State>) {
      Object.assign(this, init);
  }

}

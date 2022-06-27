export class Team {
    name: string;
    id: number;
    description: string;
    criadoPor: string;
    alteradoPor: string;
    createDate: Date;
    updateDate: Date;
    public constructor(init?: Partial<Team>) {
        Object.assign(this, init);
    }

    teamImages: any[] = [];
}

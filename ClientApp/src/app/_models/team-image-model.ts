export class TeamImage {
    id: number;
    imageName: string;
    teamId: number;
    public constructor(init?: Partial<TeamImage>) {
        Object.assign(this, init);
    }

}

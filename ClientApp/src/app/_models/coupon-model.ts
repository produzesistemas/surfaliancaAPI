export class Coupon {
    id: number;
    descricao: string;
    codigo: string;
    quantidade: number;
    clienteId?: number;
    tipo: boolean;
    geral: boolean;
    ativo: boolean;
    value: number;
    valorMinimo: number;
    initialDate: Date;
    finalDate: Date;

    public constructor(init?: Partial<Coupon>) {
        Object.assign(this, init);
    }
}

export interface createReceiptsDTO {
    provider: string;
    amount: number;
    date: Date;
    comment: string;
    currencyId: number;
}


export interface receiptsDTO {
    id: number;
    provider: string;
    amount: number;
    date: Date;
    comment: string;
    currencyId: number;
}

export interface currencyDTO {
    id:  number;
    code: string;
}

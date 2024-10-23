import {Dayjs} from "dayjs";

export interface IProduct {
    id: string;
    name: string;
    price: number;
    image: string;
}

export interface ICartItem {
    productId: string;
    quantity: number;
}

export interface ICheckoutRequest {
    items: ICartItem[];
    dateCreated: Dayjs;
}

export interface ICheckoutResponse {
    sum: number;
    appliedSales: ISalesResponse[]
}

export interface ISalesResponse {
    description: string;
}

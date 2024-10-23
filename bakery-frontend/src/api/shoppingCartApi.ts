import {ICheckoutResponse, ICheckoutRequest} from "./types";
import {axiosClient} from "./axiosClient";
import {ApiUrls} from "../AppConstants";
import {AxiosResponse} from "axios";

export const checkout = async(cart: ICheckoutRequest): Promise<ICheckoutResponse> => {
    const resp = await axiosClient
        .post<ICheckoutRequest, AxiosResponse<ICheckoutResponse>>(ApiUrls.ShoppingCartCheckout, cart);

    return resp.data
}

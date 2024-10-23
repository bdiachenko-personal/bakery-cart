import {IProduct} from "./types";
import {axiosClient} from "./axiosClient";
import {ApiUrls} from "../AppConstants";

export const getProducts = async(): Promise<IProduct[]> => {
    const resp = await axiosClient.get<IProduct[]>(ApiUrls.Products);
    return resp.data
}


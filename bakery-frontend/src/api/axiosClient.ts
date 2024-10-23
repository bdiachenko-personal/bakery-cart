import {ApiUrls} from "../AppConstants";
import Axios from "axios";

export const axiosClient = Axios.create({
    baseURL: ApiUrls.BaseApi
});

axiosClient.defaults.headers.common['Content-Type'] = 'application/json';

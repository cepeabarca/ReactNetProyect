import { obtenerToken } from "./auth/JWTManager";
import axios from "axios";

export function configurarInterceptor(){
    axios.interceptors.request.use(
        function (config){
            const token = obtenerToken();
            if (token){
                config.headers.Authorization = `Bearer ${token}`;
            }

            return config;
        },
        function (error){
            return Promise.reject(error);
        }
    )
}
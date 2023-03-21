import axios, {AxiosError} from "axios";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { urlCuentas } from "../Endpoints";
import ShowErrors from "../ShowErrors";
import AutenticacionContext from "./AuthenticationContext";
import { credencialesUsuario, respuestaAutenticacion } from "./auth.model";
import FormAuth from "./FormAuth";
import { guardarTokenLocalStorage, obtenerClaims } from "./JWTManager";

export default function Registro() {
    const [errores, setErrores] = useState<string[]>([]);
    const {actualizar} = useContext(AutenticacionContext);
    const navigate = useNavigate();
    
    async function registrar(credenciales: credencialesUsuario) {
        try {
            const respuesta = await axios
                .post<respuestaAutenticacion>(`${urlCuentas}/Create`, credenciales);
                guardarTokenLocalStorage(respuesta.data);
                actualizar(obtenerClaims());
                navigate("/");
            console.log(respuesta.data);
        } catch (error: any) {
            setErrores(error.response.data);
        }
    }
    return (
        <>
            <h3>Registro</h3>
            <ShowErrors errores={errores} />
            <FormAuth modelo={{ email: '', password: '' }}
                onSubmit={async valores => await registrar(valores)}
            />
        </>

    )
}
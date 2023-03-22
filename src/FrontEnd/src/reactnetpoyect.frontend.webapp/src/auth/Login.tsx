import axios from "axios";
import { useContext, useState } from "react";
import { useNavigate } from "react-router-dom";
import { urlCuentas } from "../Endpoints";
import MostrarErrores from "../ShowErrors";
import AutenticacionContext from "./AuthenticationContext";
import { credencialesUsuario, respuestaAutenticacion } from "./auth.model";
import FormularioAuth from "./FormAuth";
import { guardarTokenLocalStorage, obtenerClaims } from "./JWTManager";

export default function Login() {

    const {actualizar} = useContext(AutenticacionContext);
    const [errores, setErrores] = useState<string[]>([]);
    const Navigate = useNavigate();
    
    async function login(credenciales: credencialesUsuario) {
        try {
            const respuesta = await
                axios.post<respuestaAutenticacion>(`${urlCuentas}/login`, credenciales);
            
                guardarTokenLocalStorage(respuesta.data);
                actualizar(obtenerClaims());
                Navigate('/');
            console.log(respuesta);
        }
        catch (error) {
            console.log(error);
            //setErrores(error);
        }
    }

    return (
        <>
            <h3>Login</h3>
            <MostrarErrores errores={errores} />
            <FormularioAuth
                modelo={{ email: '', password: '' }}
                onSubmit={async valores => await login(valores)}
            />
        </>
    )
}
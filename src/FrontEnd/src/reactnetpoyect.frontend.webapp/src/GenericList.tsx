import { ReactElement } from "react";
import Loading from "./Loading";

export default function GenericList(props: listadoGenericoProps){
    if (!props.listado){
        if (props.cargandoUI){
            return props.cargandoUI;
        }
        return <Loading />
    } else if (props.listado.length === 0){
        if (props.listadoVacioUI){
            return props.listadoVacioUI;
        }
        return <>No hay elementos para mostrar</>
    } else{
        return props.children;
    }
}

interface listadoGenericoProps{
    listado: any;
    children: ReactElement;
    cargandoUI?: ReactElement;
    listadoVacioUI?: ReactElement;
}
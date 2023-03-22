import axios from 'axios'
import { useState } from 'react';
import { useNavigate } from "react-router-dom";
import { urlReceipts } from '../Endpoints'
import MostrarErrores from '../MostrarErrores';
import { createReceiptsDTO } from './receipts.model'
import ReceiptForm from './ReceiptForm';
export default function CrearActores() {

    const [errores, setErrores] = useState<string[]>([]);
    const navigate = useNavigate();

    async function crear(receipt: createReceiptsDTO){
        try{
           
            await axios({
                method: 'post',
                url: urlReceipts,
                data: receipt,
                //headers: {'Content-Type': 'multipart/form-data'}
            });
            debugger;
            navigate('/receipts');
        }
        catch(error: any){
            setErrores(error.response.data);
        }
    }

    return (
        <>
            <h3>Crear Recibos</h3>
            <MostrarErrores errores={errores} />
            <ReceiptForm
                modelo={{provider: '',amount:0, date: new Date(),comment: '', currencyId: 0}}
                onSubmit={async valores => await crear(valores)}
            />
        </>

    )
}
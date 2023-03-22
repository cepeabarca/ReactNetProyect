import { urlReceipts, urlCurrency } from "../Endpoints";
import axios, { AxiosResponse } from "axios";
import { useEffect, useState } from "react";
import EntityIndex from "../EntityIndex";
import { receiptsDTO, currencyDTO } from "./receipts.model";


export default function ReceiptIndex() {
    const [currencies, setCurrencies] = useState<currencyDTO[]>([]);

    useEffect(() => {
        cargarDatos();
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])
    
    function cargarDatos() {
        axios.get(`${urlCurrency}`, {
        })
            .then((respuesta: AxiosResponse<currencyDTO[]>) => {
                setCurrencies(respuesta.data);
            })
    }
    return (
        <>
            <EntityIndex<receiptsDTO>
                url={urlReceipts} urlCrear="/receipts/create" titulo="Recibos"
                nombreEntidad="Recibos"
            >
                {(receipts, botones) => <>
                    <thead>
                        <tr>
                            <th></th>
                            <th>Proveedor</th>
                            <th>Cantidad</th>
                            <th>Fecha</th>
                            <th>Comentario</th>
                            <th>Moneda</th>
                        </tr>
                    </thead>
                    <tbody>
                        {receipts?.map(receipt =>
                            <tr key={receipt.id}>
                                <td>
                                    {botones(`/receipts/edit/${receipt.id}`, receipt.id)}
                                </td>
                                <td>
                                    <div>{receipt.provider}</div>
                                </td>
                                <td>
                                    <div>{receipt.amount}</div>
                                </td>
                                <td>
                                {new Date(receipt.date).toLocaleDateString()}
                                </td>
                                <td>
                                    <div>{receipt.comment}</div>
                                </td>
                                <td>
                                    {currencies.find(c => c.id === receipt.currencyId)?.code}
                                </td>
                            </tr>)}
                    </tbody>
                </>}

            </EntityIndex>

        </>

    )
}
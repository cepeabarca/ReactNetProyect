import { urlReceipts } from '../Endpoints'
import { createReceiptsDTO, receiptsDTO } from "./receipts.model";
import ReceiptForm from './ReceiptForm'
import EditarEntidad from '../EditEntity'
//import { convertirActorAFormData } from 'utils/formDataUtils'

export default function EditarActores() {

    const transformar = (receipt: receiptsDTO) => {
        return {
            provider: receipt.provider,
            amount: receipt.amount,
            date: new Date(receipt.date),
            comment: receipt.comment,
            currencyId: receipt.currencyId
        }
    }

    return (
        <>
            <EditarEntidad<createReceiptsDTO, receiptsDTO>
                url={urlReceipts} urlIndice="/receipts" nombreEntidad="Receipts"
                transformar={transformar}
                >
                {(entidad, editar) =>
                    <ReceiptForm
                        modelo={entidad}
                        onSubmit={async valores => await editar(valores)}
                    />}
            </EditarEntidad>
        </>

    )
}
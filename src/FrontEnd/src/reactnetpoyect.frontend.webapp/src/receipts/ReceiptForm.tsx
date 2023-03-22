import { ErrorMessage, Field, Form, Formik, FormikHelpers } from "formik";
import { createReceiptsDTO, currencyDTO, receiptsDTO } from "./receipts.model";
import axios, { AxiosResponse } from "axios";
import FormGroupText from './../FormGroupText'
import Button from '../Button'
import { Link } from "react-router-dom";
import * as Yup from 'yup'
import FormGroupFecha from './../FormGroupFecha'
import MostrarErrorCampo from "../MostrarErrorCampo";
import { useEffect, useState } from "react";
import { urlCurrency } from "../Endpoints";


export default function ReceiptForm(props: receiptFormProps) {
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
        <Formik
            initialValues={props.modelo}
            onSubmit={props.onSubmit}  
            validationSchema={Yup.object({
                provider: Yup.string().required('Este campo es requerido'),
                amount: Yup.number().required('Este campo es requerido'),
                date: Yup.date().required('Este campo es requerido'),
                comment: Yup.string().required('Este campo es requerido')
            })}
            >
            {(formikProps) => (
                <Form>
                    <FormGroupText campo="provider" label="Proveedor" />

                    <div className="form-group">
                        <label htmlFor="amount">Cantidad</label>
                        <Field type="number" name="amount" className="form-control" campo="amount"  />
                        <ErrorMessage name="amount">{mensaje =>
                            <MostrarErrorCampo mensaje={mensaje} />
                        }</ErrorMessage>
                    </div>      

                    <FormGroupFecha label="Fecha" campo="date" />
                    <FormGroupText campo="comment" label="Comentario" />
                    <div className="form-group">
                        <label htmlFor="currencyId">Moneda</label>
                        <Field as="select" name="currencyId" className="form-control" campo="currencyId">
                        <option value='0' disabled>Seleccione una moneda</option>
                            {currencies.map((currency) => (
                            <option key={currency.id} value={currency.id}>
                            {currency.code}
                        </option> ))}
                        </Field>
                    </div>
                    <Button disabled={formikProps.isSubmitting}
                        type="submit"
                    >Salvar</Button>
                    <Link className="btn btn-secondary" to="/receipts">Cancelar</Link>
                </Form>
            )}
        </Formik>
    )
}

interface receiptFormProps {
    modelo: createReceiptsDTO;
    onSubmit(valores: createReceiptsDTO, acciones: FormikHelpers<createReceiptsDTO>): void;
}
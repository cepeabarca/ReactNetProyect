import axios from "axios";
import Swal from "sweetalert2";
import Button from "../Button";
import confirmar from "../Confirm";
import { urlCuentas } from "../Endpoints";
import EntityIndex from "../EntityIndex";
import { usuarioDTO } from "./auth.model";

export default function UserIndex() {

    async function hacerAdmin(id: string) {
        await editarAdmin(`${urlCuentas}/hacerAdmin`, id);
    }

    async function removerAdmin(id: string) {
        await editarAdmin(`${urlCuentas}/removerAdmin`, id);
    }

    async function editarAdmin(url: string, id: string) {
        await axios.post(url, JSON.stringify(id),
            {
                headers: { 'Content-Type': 'application/json' }
            }
        )

        Swal.fire({
            title: 'Exito',
            text: 'Operación realizada con éxito',
            icon: 'success'
        })
    }

    return (
        <EntityIndex<usuarioDTO>
            url={`${urlCuentas}/ListUsers`}
            titulo="Usuarios"
            >

            {usuarios => <>
                <thead>
                    <tr>
                        <th></th>
                        <th>Nombre</th>
                    </tr>
                </thead>
                <tbody>
                    {usuarios?.map(usuario => <tr key={usuario.id}>
                        <td>
                            <Button
                                onClick={() => confirmar(() => hacerAdmin(usuario.id),
                                    `¿Desea hacer a ${usuario.email} admin?`, 'Realizar')}>
                                Hacer Admin</Button>
                            <Button className="btn btn-danger" style={{ marginLeft: '1rem' }}
                                onClick={() => confirmar(() => removerAdmin(usuario.id),
                                    `¿Desea remover a ${usuario.email} como admin?`, 'Realizar')}>
                                Remover Admin</Button>
                        </td>
                        <td>
                            {usuario.email}
                        </td>
                    </tr>)}
                </tbody>
            </>}
        </EntityIndex>
    )
}
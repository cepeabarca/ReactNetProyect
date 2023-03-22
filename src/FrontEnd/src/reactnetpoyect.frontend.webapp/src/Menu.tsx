import AutenticacionContext from './auth/AuthenticationContext';
import Autorizado from './auth/Authorized';
import { logout } from './auth/JWTManager';
import { useContext } from 'react';
import { Link, NavLink } from 'react-router-dom'
import Button from './Button';

export default function Menu() {
    const {actualizar, claims} = useContext(AutenticacionContext);

    function obtenerNombreUsuario(): string {
        return claims.filter(x => x.nombre === "email")[0]?.valor;
    }

    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light">
            <div className="container-fluid">
                
                {/* <NavLink className="navbar-brand"
                    to="/receipts">Recibos</NavLink> */}
                <div className="collapse navbar-collapse" 
                style={{display: 'flex', justifyContent: 'space-between' }}>
                    <ul className="navbar-nav me-auto mb-2 mb-lg-0">
                        <Autorizado
                        autorizado={<>
                            
                            <NavLink className="navbar-brand"
                            to="/receipts">Recibos</NavLink>
                            </>}
                            
                        />
                        <Autorizado role="admin"
                            autorizado={
                                <>
                                    <li className="nav-item">
                                        <NavLink className="nav-link" 
                                            to="/users">
                                            Usuarios
                            </NavLink>
                                    </li>
                                </>
                            }
                        />


                    </ul>

                    <div className="d-flex">
                        <Autorizado
                            autorizado={<>
                            <span className="nav-link">Hola, {obtenerNombreUsuario()}</span>
                            <Button 
                            onClick={() => {
                                logout();
                                actualizar([]);
                            }}
                            className="btn btn-default">Log out</Button>
                            </>}
                            noAutorizado={<>
                                <NavLink to="/registro" className="btn btn-default">
                                    Registro
                                        </NavLink>
                                <NavLink to="/login" className="btn btn-default">
                                    Login
                                        </NavLink>
                            </>}
                        />
                    </div>

                </div>
            </div>
        </nav>
    )
}
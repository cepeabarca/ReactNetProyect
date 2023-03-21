// import IndiceGeneros from "./generos/IndiceGeneros";
 import LandingPage from "./LandingPage";
// import CrearGenero from "./generos/CrearGenero"
// import EditarGenero from "./generos/EditarGenero"


 import RedireccionarALanding from './RedirectLandingPage'
import Registro from './auth/Register';
import Login from "./auth/Login";
import UserIndex from './auth/UserIndex';

const rutas = [
    // {path: '/generos/crear', componente: CrearGenero, esAdmin: true},
    // {path: '/generos/editar/:id(\\d+)', componente: EditarGenero, esAdmin: true},
    // {path: '/generos', componente: IndiceGeneros, exact: true, esAdmin: true},

    // {path: '/actores/crear', componente: CrearActores, esAdmin: true},


    {path: '/registro', componente: Registro},
    {path: '/login', componente: Login},
    {path: '/usuarios', componente: UserIndex, esAdmin: true},

     {path: '/', componente: LandingPage, exact: true},
     {path: '*', componente: RedireccionarALanding}
];

export default rutas;
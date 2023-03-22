 import ReceiptsIndex from "./receipts/ReceiptIndex";
 import LandingPage from "./LandingPage";
 import CreateReceipt from "./receipts/CreateReceipt"
 import EditReceipt from "./receipts/EditReceipt"


 import RedireccionarALanding from './RedirectLandingPage'
import Registro from './auth/Register';
import Login from "./auth/Login";
import UserIndex from './auth/UserIndex';

const rutas = [
    {path: '/registro', componente: Registro},
    {path: '/login', componente: Login},
    {path: '/users', componente: UserIndex, esAdmin: true},

     

    {path: '/receipts/create', componente: CreateReceipt},
    {path: '/receipts/edit/:id', componente: EditReceipt},
    {path: '/receipts', componente: ReceiptsIndex, exact: true},

    {path: '/', componente: LandingPage, exact: true},
    {path: '*', componente: RedireccionarALanding},

];

export default rutas;
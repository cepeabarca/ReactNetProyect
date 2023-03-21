import React, { useEffect, useState } from 'react';
import { Routes ,Route } from 'react-router-dom';
import { BrowserRouter } from 'react-router-dom';
import './App.css';
import Menu from './Menu';
import rutas from './route-config'

import AuthenticationContext from './auth/AuthenticationContext'
import { claim } from './auth/auth.model';
import { obtenerClaims } from './auth/JWTManager';
import { configurarInterceptor } from './Interceptor';
import Registro from './auth/Register';


configurarInterceptor();

function App() {
  const [claims, setClaims] = useState<claim[]>([]);

  useEffect(() => {
    setClaims(obtenerClaims());
  }, [])

  function actualizar(claims: claim[]) {
    setClaims(claims);
  }

  function esAdmin() {
    return claims.findIndex(claim => claim.nombre === 'role' && claim.valor === 'admin') > -1;
  }

  return (
    <>
      <BrowserRouter>

        <AuthenticationContext.Provider value={{ claims, actualizar }}>
          <Menu />
          <div className="container">
            <Routes>
              {rutas.map(ruta =>
                <Route key={ruta.path} path={ruta.path}>
                  {ruta.esAdmin && !esAdmin() ? <>
                    No tiene permiso para acceder a este componente
                    </> : <Route path={ruta.path} element={<ruta.componente />} />}
                </Route>)}
            </Routes>
          </div>
        </AuthenticationContext.Provider>

      </BrowserRouter>
    </>

  );
}

export default App;

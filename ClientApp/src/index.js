import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { BrowserRouter } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { Auth0Provider } from '@auth0/auth0-react';

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

ReactDOM.render(
  <Auth0Provider
    domain='dotnetnorth-dev.eu.auth0.com'
    clientId='bDsSu2ouIi4m61m0hJzRxuM9PNuVDDy9'
    redirectUri={window.location.origin}
    audience='https://localhost:44362/api'
    scope='read:current_user update:current_user_metadata'
  >
    <BrowserRouter basename={baseUrl}>
      <App />
    </BrowserRouter>
  </Auth0Provider>,
  rootElement
);

registerServiceWorker();

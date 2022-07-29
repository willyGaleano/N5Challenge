import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App'
import { Router as RouterHistory } from "react-router-dom";
import history from './utils/history';
import './index.css'
import { store } from './redux/store';
import { Provider } from 'react-redux';

ReactDOM.createRoot(document.getElementById('root')).render(
  <RouterHistory history={history}>
   <Provider store={store}>
      <App />
    </Provider>
  </RouterHistory>
)

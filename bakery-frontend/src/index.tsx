import './index.css'
import ReactDOM from 'react-dom/client';
import {RouterProvider, createBrowserRouter} from "react-router-dom";
import {AppRoutes} from "./AppRoutes";
import {StrictMode} from "react";
import {CssBaseline, ThemeProvider} from "@mui/material";
import theme from "./theme";
import {CartContextProvider} from "./context/CartContext";

const root = ReactDOM.createRoot(
    document.getElementById('root') as HTMLElement
);

const router = createBrowserRouter(AppRoutes)

root.render(
    <StrictMode>
        <ThemeProvider theme={theme}>
            <CartContextProvider>
                <CssBaseline/>
                <RouterProvider router={router}/>
            </CartContextProvider>
        </ThemeProvider>
    </StrictMode>
);

import {Navigate, RouteObject} from "react-router-dom";
import {ShoppingCart, ProductCatalog} from "./components/pages";
import NavBar from "./components/layout/NavBar";

export const AppRoutes: RouteObject[] = [
    {
        path: '*',
        element: <Navigate to='' />
    },
    {
        path: '',
        element: <NavBar />,
        children: [
            {
                path: '',
                element: <ProductCatalog />
            },
            {
                path: 'cart',
                element: <ShoppingCart />
            },
        ]
    }
]

import {FC, ReactNode, createContext, useState, useEffect, useContext} from 'react'
import {ICartItem} from "../api/types";

interface CartContextType {
    cartItems: ICartItem[];
    addToCart: (productId: string, quantity: number) => void;
    decreaseQuantity: (productId: string) => void;
    removeFromCart: (productId: string) => void;
    clearCart: () => void;
    itemsCount: number;
}

export const CartContext = createContext<CartContextType | null>(null);

export const useCartContext = () => {
    const context = useContext(CartContext);

    if (context === null) {
        throw new Error("useCartContext must be used within a CartContextProvider");
    }

    return context;
}

export const CartContextProvider: FC<{children: ReactNode}> = ({ children }) => {
    const [itemsCount, setItemsCount] = useState(0);
    const [cartItems, setCartItems] = useState<ICartItem[]>(() => {
        const storedItems = localStorage.getItem('cartItems');
        return storedItems ? JSON.parse(storedItems) : [];
    });

    const addToCart = (productId: string, quantity: number) => {
        const isItemInCart = !!cartItems.find(cartItem => cartItem.productId === productId);

        if (isItemInCart) {
            setCartItems(cartItems.map((cartItem) =>
                    cartItem.productId === productId
                        ? { ...cartItem, quantity: cartItem.quantity + quantity }
                        : cartItem
                )
            );
        } else {
            setCartItems([...cartItems, { productId, quantity }]);
        }
    };

    const decreaseQuantity = (productId: string) => {
        setCartItems(cartItems.map(cartItem =>
            cartItem.productId === productId && cartItem.quantity > 1
                ? { ...cartItem, quantity: cartItem.quantity - 1 }
                : cartItem
        ));
    };

    const clearCart = () => {
        setCartItems([]);
    };


    const removeFromCart = (productId: string) => {
        setCartItems(cartItems.filter((cartItem) => cartItem.productId !== productId));
    };

    useEffect(() => {
        localStorage.setItem("cartItems", JSON.stringify(cartItems));
        setItemsCount(cartItems.reduce((sum, item) => sum + item.quantity, 0))
    }, [cartItems]);

    return (
        <CartContext.Provider
            value={{
                cartItems,
                addToCart,
                removeFromCart,
                decreaseQuantity,
                clearCart,
                itemsCount,
            }}
        >
            {children}
        </CartContext.Provider>
    );
};

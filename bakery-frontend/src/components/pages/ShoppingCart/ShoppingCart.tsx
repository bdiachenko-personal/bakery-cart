import {useCartContext} from "../../../context/CartContext";
import {Typography, Button, IconButton, CardMedia, Divider, Box} from "@mui/material";
import {Row, Column} from "../../shared/FlexContainer";
import RemoveIcon from "@mui/icons-material/Remove";
import AddIcon from "@mui/icons-material/Add";
import DeleteIcon from "@mui/icons-material/Delete";
import {useEffect, useState} from "react";
import {ICartItem, ICheckoutRequest, ICheckoutResponse, IProduct} from "../../../api/types";
import {getProducts} from "../../../api/productsApi";
import {DatePicker, LocalizationProvider} from "@mui/x-date-pickers";
import {AdapterDayjs} from "@mui/x-date-pickers/AdapterDayjs";
import {checkout} from "../../../api/shoppingCartApi";
import dayjs, {Dayjs} from "dayjs";
import utc from 'dayjs/plugin/utc';

dayjs.extend(utc);

const ShoppingCart = () => {
    const [products, setProducts] = useState<IProduct[]>();
    const [orderDate, setOrderDate] = useState<Dayjs | null>(null);
    const [checkoutResponse, setCheckoutResponse] = useState<ICheckoutResponse | null>(null);
    const {cartItems, addToCart, decreaseQuantity, removeFromCart, clearCart} = useCartContext();

    const handleIncreaseQuantity = (productId: string) => {
        addToCart(productId, 1);
    };

    const handleDecreaseQuantity = (productId: string) => {
        decreaseQuantity(productId);
    };

    const handleRemoveFromCart = (productId: string) => {
        removeFromCart(productId);
    };

    const handleClearCart = () => {
        setOrderDate(null);
        setCheckoutResponse(null)
        clearCart()
    }

    const fetchProducts = async () => {
        const products = await getProducts();
        setProducts(products);
    };

    useEffect(() => {
        fetchProducts();
    }, []);

    const renderProduct = (cartItem: ICartItem) => {
        const product = products && products.find((x) => x.id === cartItem.productId);

        if (!product) {
            return null;
        }

        const itemTotalPrice = product.price * cartItem.quantity;

        return (
            <Row key={product.id} justifyContent="space-between" alignItems="center" gap="1rem" padding="1rem"
                 sx={{borderBottom: '1px solid #e0e0e0'}}>
                <Row alignItems="center" gap="1rem">
                    <CardMedia
                        component="img"
                        image={product.image}
                        alt={product.name}
                        sx={{width: 80, height: 80, objectFit: 'cover', borderRadius: '8px'}}
                    />
                    <Column>
                        <Typography variant="body1">{product.name}</Typography>
                        <Typography variant="body2" color="text.secondary">
                            ${product.price.toFixed(2)} x {cartItem.quantity} = ${itemTotalPrice.toFixed(2)}
                        </Typography>
                    </Column>
                </Row>
                <Row alignItems="center" gap="1rem">
                    <IconButton
                        onClick={() => handleDecreaseQuantity(cartItem.productId)}
                        disabled={cartItem.quantity <= 1}
                        sx={{backgroundColor: '#f5f5f5'}}
                    >
                        <RemoveIcon/>
                    </IconButton>
                    <Typography>{cartItem.quantity}</Typography>
                    <IconButton onClick={() => handleIncreaseQuantity(cartItem.productId)}
                                sx={{backgroundColor: '#f5f5f5'}}>
                        <AddIcon/>
                    </IconButton>
                    <IconButton onClick={() => handleRemoveFromCart(cartItem.productId)}
                                sx={{backgroundColor: '#ffebee', color: '#f44336'}}>
                        <DeleteIcon/>
                    </IconButton>
                </Row>
            </Row>
        );
    };

    const handleCheckout = async () => {
        if (!orderDate || cartItems.length === 0) return;

        const request: ICheckoutRequest = {
            dateCreated: orderDate,
            items: cartItems
        };

        try {
            const response = await checkout(request);
            setCheckoutResponse(response);
        } catch (error) {
            console.error("Checkout error", error);
        }
    };

    return (
        <LocalizationProvider dateAdapter={AdapterDayjs}>
            <Column padding="2rem" gap="2rem">
                <Typography variant="h4">Shopping Cart</Typography>
                {cartItems.length === 0 && <Typography>Your cart is empty</Typography>}
                {cartItems.length > 0 && (
                    <>
                        <Column gap="1rem">
                            {cartItems.map(renderProduct)}
                        </Column>
                        <Divider sx={{margin: '2rem 0'}}/>
                        <Row alignItems="center" justifyContent='space-between'>
                            <Row alignItems="center" gap="1rem">
                                <DatePicker
                                    timezone="UTC"
                                    label="Order Date"
                                    value={orderDate}
                                    onChange={(newValue) => setOrderDate(newValue)}
                                />
                                <Button
                                    variant="contained"
                                    size="large"
                                    disabled={!orderDate || cartItems.length === 0}
                                    sx={{ backgroundColor: '#FE7170' }}
                                    onClick={handleCheckout}
                                >
                                    Checkout
                                </Button>
                            </Row>
                            <Button
                                variant="contained"
                                size="large"
                                disabled={cartItems.length === 0}
                                sx={{ backgroundColor: 'red' }}
                                onClick={handleClearCart}
                            >
                                Clear cart
                            </Button>
                        </Row>
                        {checkoutResponse && (
                            <Column>
                                <Typography variant="h6">Order Total: ${checkoutResponse.sum.toFixed(2)}</Typography>
                                {Array.isArray(checkoutResponse?.appliedSales) && checkoutResponse.appliedSales.length > 0 && (
                                    <Box>
                                        <Typography variant="body2" color="text.secondary">
                                            Applied Sales:
                                        </Typography>
                                        {checkoutResponse.appliedSales.map((x, index) => (
                                            <Typography key={index}>{x.description}</Typography>
                                        ))}
                                    </Box>
                                )}
                            </Column>
                        )}
                    </>
                )}
            </Column>
        </LocalizationProvider>
    );
};

export default ShoppingCart;

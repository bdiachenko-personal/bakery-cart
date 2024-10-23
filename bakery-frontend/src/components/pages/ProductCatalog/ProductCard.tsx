import {useState} from 'react';
import { Card, CardMedia, CardContent, Typography, Button } from '@mui/material';
import { IProduct } from '../../../api/types';
import {Row} from "../../shared/FlexContainer";
import IconButton from "@mui/material/IconButton";
import RemoveIcon from '@mui/icons-material/Remove';
import AddIcon from '@mui/icons-material/Add';
import {useCartContext} from "../../../context/CartContext";

interface IProps {
    product: IProduct;
}

const ProductCard = ({ product }: IProps) => {
    const [quantity, setQuantity] = useState(1);
    const {addToCart} = useCartContext();

    const handleDecrease = () => {
        setQuantity(quantity - 1);
    };

    const handleIncrease = () => {
        setQuantity(quantity + 1);
    };

    const handleAddToCart = () => {
        addToCart(product.id, quantity)
        setQuantity(1);
    };

    return (
        <Card sx={{ width: 500 }}>
            <CardMedia
                component="img"
                height="280"
                image={product.image}
                alt={product.name}
            />
            <CardContent sx={{ display: 'flex', flexDirection: 'column', gap: '1rem' }}>
                <Typography variant="h5">
                    {product.name}
                </Typography>
                <Typography variant="body1">
                    Price: ${product.price.toFixed(2)}
                </Typography>
                <Row justifyContent="space-between" alignItems="center" gap="2rem">
                    <Row alignItems="center" gap="1rem">
                        <IconButton
                            onClick={handleDecrease}
                            disabled={quantity <= 1}
                        >
                            <RemoveIcon />
                        </IconButton>
                        <Typography>{quantity}</Typography>
                        <IconButton onClick={handleIncrease}>
                            <AddIcon />
                        </IconButton>
                    </Row>
                    <Button variant="contained" onClick={handleAddToCart} sx={{ backgroundColor: '#FE7170' }}>
                        Add to Cart
                    </Button>
                </Row>
            </CardContent>
        </Card>
    );
};

export default ProductCard;

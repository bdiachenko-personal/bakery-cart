import {IProduct} from "../../../api/types";
import {useEffect, useState} from "react";
import {getProducts} from "../../../api/productsApi";
import Typography from "@mui/material/Typography";
import {Column, Row} from "../../shared/FlexContainer";
import ProductCard from "./ProductCard";

const ProductCatalog = () => {
    const [products, setProducts] = useState<IProduct[]>([]);

    useEffect(() => {
        fetchProducts()
    }, [])

    async function fetchProducts(){
        const products = await getProducts();
        setProducts([...products]);
    }

    return (
        <Column padding='2rem' gap='2rem'>
            <Typography variant="h4">Product Catalog</Typography>
            <Row gap='3rem' flexWrap='wrap'>
                {products.map(product => <ProductCard key={product.id} product={product} />)}
            </Row>
        </Column>
    )
}

export default ProductCatalog;

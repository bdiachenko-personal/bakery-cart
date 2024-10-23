import {Outlet} from 'react-router-dom';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import ShoppingCartIcon from '@mui/icons-material/ShoppingCart';
import {Link} from 'react-router-dom';
import {styled} from '@mui/material/styles';
import {FC} from "react";
import {Badge, Box} from "@mui/material";
import {useCartContext} from "../../context/CartContext";
import {Row} from "../shared/FlexContainer";

const StyledAppBar = styled(AppBar)({
    backgroundColor: '#f5deb3',
});

const LogoImage = styled('img')({
    height: '120px',
    marginRight: '10px',
});

const OutletWrapper = styled(Box)({
    backgroundColor: '#FCE4E4',
    marginTop: '120px',
    height: 'calc(100vh - 120px)'
});

const NavBar: FC = () => {
    const {itemsCount} = useCartContext();

    return (
        <>
            <StyledAppBar position="fixed">
                <Toolbar sx={{justifyContent: 'space-between'}}>
                    <Box display='flex' flexDirection='row' alignItems='center' component={Link} to={''}
                         sx={{textDecoration: 'none'}}>
                        <LogoImage
                            src="https://png.pngtree.com/png-clipart/20230301/ourmid/pngtree-bakery-logo-baker-illustration-png-image_6625246.png"
                            alt="Bakery Logo"/>
                        <Typography variant="h3" sx={{color: '#CB4E4D'}}>
                            Bakery
                        </Typography>
                    </Box>
                    <IconButton size='large' color="inherit" component={Link} to="/cart" sx={{color: '#CB4E4D'}}>
                        <Badge badgeContent={itemsCount} color="secondary">
                            <ShoppingCartIcon fontSize="large"/>
                        </Badge>
                    </IconButton>
                </Toolbar>
            </StyledAppBar>
            <OutletWrapper>
                <Outlet/>
            </OutletWrapper>
        </>
    );
};

export default NavBar;

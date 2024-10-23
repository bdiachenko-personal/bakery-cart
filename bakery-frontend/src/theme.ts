import { createTheme } from '@mui/material/styles';

const theme = createTheme({
    typography: {
        fontFamily: '"Poppins", sans-serif',
    },
    components: {
        MuiCssBaseline: {
            styleOverrides: {
                '@global': {
                    body: {
                        fontFamily: '"Poppins", sans-serif',
                        margin: 0,
                        backgroundColor: '#FCE4E4',
                    },
                    '*': {
                        boxSizing: 'border-box',
                    },
                },
            },
        },
    },
});

export default theme;

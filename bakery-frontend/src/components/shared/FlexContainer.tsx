import {Box, BoxProps} from '@mui/material';
import { styled } from '@mui/system';
import {ElementType} from "react";

export const Row = styled(Box)<BoxProps & { component?: ElementType }>({
    display: 'flex',
    flexDirection: 'row',
});

export const Column = styled(Box)({
    display: 'flex',
    flexDirection: 'column',
});

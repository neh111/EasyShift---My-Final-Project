//צפיה בתוצאות השיבוץ
import { Button } from "@mui/material";
import { Grid } from "@mui/material";
import { Input } from "@material-ui/core";
import { useForm } from 'react-hook-form';
import employee from "../../classes/employee";
import axios from "axios";
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import Box from '@mui/material/Box';
import Avatar from '@mui/material/Avatar';
import Typography from '@mui/material/Typography';
import TextField from '@mui/material/TextField';
import { Navigate } from "react-router-dom";
import { useNavigate } from 'react-router-dom';
import React from "react";
import swal from 'sweetalert';
import DirectorMenu from "./DirectorMenu";

export default function DirectorMy2(): JSX.Element {
    const url = "https://localhost:44336/";
    const [employ, setEmploy] = React.useState<employee>({} as employee)
    const onChange = (key: string, value: string | number) => setEmploy(x => ({ ...x, [key]: value }))
    let navigate = useNavigate();
    const GetemployeeByPassAndId = () => {
        const data = employ
        if (data.employee_id_str == "2" && data.employee_id == 70) {
            swal(" OK!", "Hi director!you connect succesfuly!", "success");
            navigate('/DirectorMenu');
        }
        else
            navigate('/');
    }
    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <Box
                sx={{
                    marginTop: 8,
                    display: 'flex',
                    flexDirection: 'column',
                    alignItems: 'center',
                }} 
            >
                <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
                </Avatar>
                <Typography component="h1" variant="h5">
                    Sign in
                </Typography>
                <Box component="form" sx={{ mt: 1 }} onSubmit={(e: any) => { GetemployeeByPassAndId() }}>
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        onChange={(e) => onChange('employee_id', e.target.value)}
                        type="text" placeholder="סיסמא" />
                    <TextField
                        margin="normal"
                        required
                        fullWidth
                        onChange={(e) => onChange('employee_id_str', e.target.value)}
                        type="text" placeholder=" תעודת זהות"
                    />
                    <Button
                        type="submit"
                        fullWidth
                        variant="contained"
                        sx={{ mt: 3, mb: 2 }}
                    // onClick={() => { GetemployeeByPassAndId() }}
                    >
                        Sign in
                    </Button>
                </Box>
            </Box>
        </Container>
    );
}
import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Grid from '@mui/material/Grid';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import swal from 'sweetalert';
import { useNavigate } from 'react-router-dom';
import employee from "../../classes/employee";
import axios from "axios";
import { useForm, useFormState } from 'react-hook-form';
import ShowPosition from '../../ShowPosition';
import PriorityList2 from '../../PriorityList2';
import { da } from 'date-fns/locale';
function Copyright(props: any) {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'EasyShift © '}
            <Link color="inherit" href="#">
                Your Website
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

const theme = createTheme();

export default function SignUp(): JSX.Element {
    const url = "https://localhost:44336/";
    let navigate = useNavigate()
    const [employ, setEmploy] = React.useState<employee>({} as employee)
    const onChange = (key: string, value: string | number | Date) => { setEmploy(x => ({ ...x, [key]: value }))}

    const addEmployee = async (e: any) => {
        e.preventDefault()
        const data = employ;
        const JobId = JSON.parse(localStorage.getItem("JobId") || '{}');
        data.job_id = JobId;
        const emp=data.employee_id;
        localStorage.setItem("empId",JSON.stringify(emp));
        data.job_id = JobId;
        let employeePromise = axios.post(url + "api/Employee/AddEmp", data);
        let response = await employeePromise;
        if (response.status === 200) {
            swal("OK!", `you connect succesfuly! your password is ${response.data}`);
            navigate('/EmployeeMenu');
        }
    }
    return (
        <ThemeProvider theme={theme}>
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
                        Sign up
                    </Typography>
                    <Box component="form" sx={{ mt: 1 }} onSubmit={(e: any) => { addEmployee(e) }}>
                        <TextField
                            value={employ.first_name || ""}
                            onChange={(e) => onChange('first_name', e.target.value)}
                            type="text" placeholder="שם פרטי"
                            margin="normal"
                            required
                            fullWidth
                        />
                        <TextField
                            value={employ.last_name || ""}
                            onChange={(e) => onChange('last_name', e.target.value)}
                            margin="normal"
                            required
                            fullWidth
                            type="text" placeholder="שם משפחה"
                        />
                        <TextField
                            value={employ.employee_id_str || ""}
                            onChange={(e) => onChange('employee_id_str', e.target.value)}
                            margin="normal"
                            required
                            fullWidth
                            type="text" placeholder="תעודת זהות"
                        // { required: true, minLength: 3 }
                        // {...errors.employee_id_str?.type === "required" && <span>תעודת זהות חסרה</span>}
                        // {...errors.employee_id_str?.type === "minLength" && <span>תעודת זהות קצרה...</span>}
                        /> <TextField
                            value={employ.mail || ""}
                            onChange={(e) => onChange('mail', e.target.value)}
                            margin="normal"
                            required
                            fullWidth
                            type="text" placeholder="מייל"
                        />
                        <TextField
                            value={employ.cellphone_number || ""}
                            onChange={(e) => onChange('cellphone_number', e.target.value)}
                            margin="normal"
                            required
                            fullWidth
                            type="text" placeholder="פלאפון"
                        />
                        <TextField
                            value={employ.date_start_job || ""}
                            onChange={(e) => onChange('date_start_job', e.target.value)}
                            margin="normal"
                            required
                            fullWidth
                            type="date" placeholder="תאריך תחילת עבודה"
                        // { max: 120 }
                        />
                        <TextField
                            value={employ.num_shifts_in_week || ""}
                            onChange={(e) => onChange('num_shifts_in_week', e.target.value)}
                            margin="normal"
                            required
                            fullWidth
                            type="number" placeholder="מספר משמרות בשבוע"
                        //  { required: true, max: 7 })
                        />
                        <ShowPosition />
                        {/* <PriorityList2 /> */}
                        <Button
                            fullWidth
                            variant="contained"
                            sx={{ mt: 3, mb: 2 }}
                            // onClick={() => { addEmployee() }}
                            type="submit"
                        >
                            Sign up
                        </Button>
                    </Box>
                </Box>
                <Copyright sx={{ mt: 8, mb: 4 }} />
            </Container>
        </ThemeProvider>
    );
}
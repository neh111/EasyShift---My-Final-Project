import * as React from 'react';
import Checkbox from '@mui/material/Checkbox';
import TextField from '@mui/material/TextField';
import { } from "module";
// import { NumberInput } from 'react-admin';
// import PhoneInput from 'react-phone-number-input':
import { ChangeEvent } from 'react';
import ChangeShift from './changeShift';
import { daysToWeeks } from 'date-fns';
import { Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';

export interface numeric {
    numb: number | string | undefined
}

export default function Checkboxes() {
    let navigate = useNavigate()
    const back = () => {
        navigate('/DirectorMenu');
    }
    const [num, setNum] = React.useState<number | string>(3);
    const [show, setShow] = React.useState(false)
    return (
        <>
            <br />
            <br />
            הכנס מספר משמרות ליום
            <TextField
                value={num || ""}
                onChange={(e) => setNum(e.target.value)}
                margin="normal"
                required
                fullWidth
                type="number" placeholder="מספר משמרות בכל יום "
            />
            <Button onClick={() => { setShow(true) }}>שלח</Button>
            {show && <ChangeShift numb={num} />}
            <Button variant="outlined"style={{background:"blue",color:"yellow"}} type="submit" onClick={back}>
                חזרה
            </Button>
        </>
    );
}



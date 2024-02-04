import * as React from 'react';
import Stack from '@mui/material/Stack';
import TextField from '@mui/material/TextField';
import { AdapterDateFns } from '@mui/x-date-pickers/AdapterDateFns';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DesktopDatePicker } from '@mui/x-date-pickers/DesktopDatePicker';
import { Button } from '@mui/material';
import axios from 'axios';
import ShiftType_Dto from "../../classes/ShiftType_Dto";
import { useNavigate } from 'react-router-dom';

export interface numeric {
    numb: number | string | undefined
}
const ChangeShift = ({ numb = 2 }: numeric) => {
    let navigate = useNavigate()
    const back = () => {
        navigate('/DirectorMenu');
    }
    var data: number[] = [];
    var arr1: ShiftType_Dto[] = [];

    var length = numb; // user defined length
    for (var i = 0; i < length; i++) {
        data.push(i);
    }
    for (var i = 0; i < length; i++) {
        // arr.push({ start: "", end: "", name: "" });
        arr1.push({ shift_type_id: 1, beginning_time: "", end_time: "", description: "" });
    }
    const [newArray, setNewArray] = React.useState(arr1);
    const sendData = async () => {
        let arr = localStorage.getItem("arrResultDays") || [];
        console.log(newArray);
        console.log(arr);
        axios.post(`https://localhost:44336/api/Employee/GoToSaveShiftStructure?days=${arr[0]}&days=${arr[1]}&days=${arr[2]}&days=${arr[3]}&days=${arr[4]}&days=${arr[5]}&days=${arr[6]}&numShiftsInDays=${numb}`, newArray)
    }
    return (
        <>
            {data.map((item, index) => {
                return <> <TextField
                    onChange={(e) => { let tmp = [...newArray]; tmp[index].beginning_time = e.target.value; setNewArray(tmp) }}
                    margin="normal"
                    required
                    fullWidth
                    type="time" placeholder=" הכנס שעת התחלה"
                    key={index}
                />
                    <TextField
                        onChange={(e) => { let tmp = [...newArray]; tmp[index].end_time = e.target.value; setNewArray(tmp) }}
                        margin="normal"
                        required
                        fullWidth
                        type="time" placeholder=" הכנס שעת סיום"
                        key={index}
                    />
                    <TextField
                        onChange={(e) => { let tmp = [...newArray]; tmp[index].description = e.target.value; setNewArray(tmp) }}
                        margin="normal"
                        required
                        fullWidth
                        type="text" placeholder=" הכנס שם משמרת "
                        key={index}
                    />
                </>
            }
            )}
            <Button onClick={sendData}>sand all</Button>
            {console.log(newArray)}
            <Button variant="outlined"style={{background:"blue",color:"yellow"}} type="submit" onClick={back}>
                חזרה
            </Button>
        </>
    );
}

export default ChangeShift

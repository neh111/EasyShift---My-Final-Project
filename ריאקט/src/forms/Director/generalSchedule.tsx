import * as React from 'react';
import { useEffect } from 'react';
import axios from "axios";
import Box from '@mui/material/Box';
import Collapse from '@mui/material/Collapse';
import IconButton from '@mui/material/IconButton';
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from '@mui/material/Typography';
import Paper from '@mui/material/Paper';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import { useNavigate } from 'react-router-dom';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';
import { Button } from '@material-ui/core';

function RowShifts(props: {
    shiftType: ReturnType<typeof String>,
    workersInshift: ReturnType<typeof Array>,
    index: ReturnType<typeof Number>
}) {

    const { shiftType } = props;
    const { workersInshift } = props;
    const { index } = props;
    const currentWorkers = workersInshift[index];
    if (Array.isArray(currentWorkers))
        console.log("currentWorkers ", currentWorkers)
    const [open, setOpen] = React.useState(false);

    return (
        <React.Fragment>
            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
                <TableCell>
                    <IconButton
                        aria-label="expand row"
                        size="small"
                        onClick={() => setOpen(!open)}
                    >
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </TableCell>
                <TableCell component="th" scope="row">
                    {shiftType}
                </TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                            <Typography variant="h6" gutterBottom component="div">
                                עובדים
                            </Typography>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                    <TableRow>
                                        <TableCell>שם </TableCell>
                                        <TableCell>תחום</TableCell>
                                    </TableRow>

                                </TableHead>
                                <TableBody>
                                    {Array(currentWorkers).map((worker) => (
                                        Array.isArray(worker) && worker.map(w => (
                                            <TableRow >
                                                <>
                                                    <TableCell component="th" scope="row">
                                                        {w[0]}
                                                    </TableCell>
                                                    <TableCell>
                                                        {w[2]}
                                                    </TableCell>
                                                </>
                                            </TableRow>
                                        ))
                                    ))}
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}

function Row(props: { row: ReturnType<typeof String>, shifts: ReturnType<typeof Array>, workers: ReturnType<typeof Array> }) {


    const { row } = props;
    const { shifts } = props;
    const { workers } = props;

    const [open, setOpen] = React.useState(false);
    return (
        <React.Fragment>
            <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
                <TableCell>
                    <IconButton
                        aria-label="expand row"
                        size="small"
                        onClick={() => setOpen(!open)}
                    >
                        {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
                    </IconButton>
                </TableCell>
                <TableCell component="th" scope="row">
                    {row}
                </TableCell>
            </TableRow>
            <TableRow>
                <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
                    <Collapse in={open} timeout="auto" unmountOnExit>
                        <Box sx={{ margin: 1 }}>
                            <Typography variant="h6" gutterBottom component="div">
                                משמרות
                            </Typography>
                            <TableBody>
                                {shifts && shifts.map((shift, index) => (
                                    <RowShifts shiftType={String(shift)} workersInshift={workers} index={index} />
                                ))}
                            </TableBody>
                            <Table size="small" aria-label="purchases">
                                <TableHead>
                                </TableHead>
                                <TableBody>
                                </TableBody>
                            </Table>
                        </Box>
                    </Collapse>
                </TableCell>
            </TableRow>
        </React.Fragment>
    );
}

interface Props {
    start: Date | null;
    end: Date | null;
}
export default function GeneralSchedule({ start, end }: Props) {
    function createData(
        name: string
    ) {
        return {
            name,
            shifts: 'result'
            //  [
            //     {
            //         position: 'Doctor',
            //         name: "Moshe",
            //     },
            //     {
            //         position: 'Cleaner',
            //         name: "Levi",
            //     },
            // ],
            // history2: [
            //     {
            //         position: 'Doctor',
            //         name: "iiii",
            //     },
            //     {
            //         position: 'Cleaner',
            //         name: "Almoni",
            //     },
            // ],
        };

    }
    const [daysInWeek, setDaysInWeek] = React.useState(new Array());
    const [workers, setWorkers] = React.useState([]);
    const [shiftsArr, setShiftsArr] = React.useState([])

    let navigate = useNavigate()
    const back = () => {
        navigate('/DirectorMenu');
    }
    React.useEffect(() => {
        const url = "https://localhost:44336/";
        axios.get("https://localhost:44336/api/Employee/GoToGetAllShiftsType").
            then(res => {
                console.log('day in week: ', res.data);
                setShiftsArr(res.data);
                // let shifts = [];
                // let arr = result.map(r => shifts.push(r))
            })
        if (shiftsArr)
            axios.get(url + "api/Employee/GoToDaysNames").
                then(res => {
                    // console.log('day in week: ', res.data)
                    setDaysInWeek(res.data);
                    // var array = new Array();
                    // let data = daysInWeek.map(d => array.push(createData(d)))
                    // console.log('array ', array);
                    // if (array.length)
                    //     setDaysInWeek(array);
                });

        axios.get(url + `api/PlacementResult/DoPlacement?startDate=${start?.toISOString()}&endDate=${end?.toISOString()}`).
            then(res => {
                console.log('all workers: ', res.data)
                setWorkers(res.data)
            });
    }, [])
    return (
        <>
            <TableContainer component={Paper}>
                <Table aria-label="collapsible table">
                    <TableHead>
                        <TableRow>
                            <TableCell />
                            <TableCell>ימים</TableCell>
                            <TableCell></TableCell>
                            <TableCell></TableCell>
                            <TableCell></TableCell>
                            <TableCell></TableCell>
                            <TableCell></TableCell>
                        </TableRow>
                    </TableHead>
                    <TableBody>
                        {daysInWeek?.map((day, index) => (
                            <Row row={day} shifts={shiftsArr} workers={workers[index]} key={day.name} />
                        ))}
                    </TableBody>
                </Table>
            </TableContainer>
            <Button variant="outlined"style={{background:"blue",color:"yellow"}} type="submit" onClick={back}>
                חזרה
            </Button>
        </>
    );
}

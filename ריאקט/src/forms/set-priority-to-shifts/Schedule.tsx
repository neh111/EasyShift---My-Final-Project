import React from "react";
import axios from "axios"
import { DataGrid, GridActionsCellItem, GridRowId, GridColumns, GridRowParams, DataGridProps, GridRowModel, GridCellEditStopReasons, GridCellEditCommitParams, GridValueGetterParams, GridCellParams, GridRenderEditCellParams, useGridApiContext } from '@mui/x-data-grid';
import { useNavigate } from 'react-router-dom';
import { Button } from '@material-ui/core';

interface Props {
    start: Date | null;
    end: Date | null;
}
export interface RowData {
    id: number,
    day: string,
}
export interface Col {
    field: string,
    width: number,
    editable: boolean,
    headerName: any
}
const initialRows: RowData[] = [
    {
        id: 1,
        day: 'Sunday'
    },
    {
        id: 2,
        day: 'Monday'
    },
    {
        id: 3,
        day: 'Tuesday',
    },
    {
        id: 4,
        day: 'Wednesday'
    },
    {
        id: 5,
        day: 'Thursday'
    },
    {
        id: 6,
        day: 'Friaday'
    },
    {
        id: 7,
        day: 'Satarday'
    },
];

const initialCol: Col[] = [
    {
        field: 'day',
        width: 150,
        editable: true,
        headerName: 'day'
    },]

const Schedule = ({ start, end }: Props) => {
    const [matFromData, setMatFromData] = React.useState([]);
    const [rows, setRows] = React.useState<RowData[]>(initialRows);
    const [columns, setColumns] = React.useState<Col[]>(initialCol);
    const url = "https://localhost:44336/api/Employee/"
    let navigate = useNavigate()
    const back = () => {
        navigate('/EmployeeMenu');
    }
    React.useEffect(() => {
        let startDate = new String;
        let endDate = new String;
        if (start && end) {
            startDate = `${start.getFullYear()}-${start.getMonth() + 1}-${start.getDate()}`;
            endDate = `${end.getFullYear()}-${end.getMonth() + 1}-${end.getDate()}`;
        }
        const empId = JSON.parse(localStorage.getItem("empId") || '{}');
        axios.get(url + `GoToPlacementResultPerEmployee?employeeId=${empId}&startDate=${startDate}&endDate=${endDate}`)
            .then(res => {
                setMatFromData(res.data);
                console.log('matFromData ', matFromData[0])
            })
        let shifrArrTimes = ["jnj"];
        let shifrArrDays = ["s"]
        axios.get(url + 'GoToDaysNames').then(res => { console.log(res.data); shifrArrDays = res.data }).then(i => {
            axios.get(url + 'GoToGetAllShiftsType').then(res => { console.log(res.data); shifrArrTimes = res.data }).then(x => {
                let pri2: { value: string, label: string }[] = []
                let data1: Array<Col> = [{
                    field: 'day', width: 200, editable: true, headerName: 'ימים'
                }]
                for (let i = 0; i < shifrArrTimes.length; i++) {
                    const element: Col = {
                        field: i.toString(),
                        headerName: shifrArrTimes[i],
                        width: 120,
                        editable: false,
                    }
                    data1.push(element)
                }
                setColumns(data1)
                console.log('columns', data1)
                // setColumns((prevRows) => prevRows.filter((col, i) => i !== 0));
                let s: RowData = { id: 2, day: "dcfg" }
                let data: RowData[] | null | string = [s];
                for (let i = 0; i < shifrArrDays.length; i++) {
                    const element: RowData = {
                        id: i,
                        day: shifrArrDays[i],
                    }
                    data.push(element)
                }
                console.log(data)
                setRows(data)
                console.log('rows', rows)
                setRows((prevRows) => prevRows.filter((row, i) => i !== 0));

            })
        })
    }, [])
    return (
        <>
            <div style={{ height: 500, width: '100%' }}>
                {/* <DataGrid
                    columns={columns}
                    rows={rows}
                // experimentalFeatures={{ newEditingApi: true }}
                /> */}

                <table>
                    <tr>{columns.map((col, colIndex) => (
                        <th>{col.headerName}</th>
                    ))}
                    </tr>
                    {rows.map((row, rowIndex) => (
                        <tr>
                            <td>{row.day}</td>
                            {columns.slice(1).map((c, index) => (
                                <td>{matFromData[rowIndex] ? matFromData[rowIndex][index] : null}</td>
                            ))}
                        </tr>
                    ))}
                </table>
            </div>
            <Button variant="outlined"style={{background:"blue",color:"yellow"}} type="submit" onClick={back}>
                חזרה
            </Button>
        </>
    )
}
export default Schedule;

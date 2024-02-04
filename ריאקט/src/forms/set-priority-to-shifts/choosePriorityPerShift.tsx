import * as React from 'react';
import { DataGrid, GridActionsCellItem, GridRowId, GridColumns, GridRowParams, DataGridProps, GridRowModel, GridCellEditStopReasons, GridCellEditCommitParams, GridValueGetterParams, GridCellParams, GridRenderEditCellParams, useGridApiContext } from '@mui/x-data-grid';
import DeleteIcon from '@mui/icons-material/Delete';
import SecurityIcon from '@mui/icons-material/Security';
import FileCopyIcon from '@mui/icons-material/FileCopy';
import { randomCreatedDate, randomUpdatedDate } from '@mui/x-data-grid-generator';
import axios from 'axios';
import Priority from '../../classes/priority';
import { Button } from '@material-ui/core';
import { useGridApiRef } from '@mui/x-data-grid';
import PriorityList2 from '../../PriorityList2';
import { AnyNsRecord } from 'dns';
// import { rowsMetaStateInitializer } from '@mui/x-data-grid/internals';
import { useNavigate } from 'react-router-dom';

interface ShiftChoosenOption {
  id: number,
  name: string,
}

export interface RowData {
  id: number,
  day: string,
  shift: number[]
}
export interface Col {
  field: string,
  width: number,
  editable: boolean,
  valueOptions: { value: string, label: string }[],
  type: string,
  headerName: any,
  renderEditCell: any
}
const initialCol: Col[] = [{
  field: 'day',
  width: 150,
  editable: true,
  valueOptions: [{ value: 'pp', label: 'll' }],
  type: 'text',
  headerName: 'day',
  renderEditCell: [{
    priority_description: "לא יכול",
    priority_id: 10
  }]
}
]
const initialRows: RowData[] = [
  {
    id: 1,
    day: 'Sunday',
    shift: [1, 2, 1]
  },
  {
    id: 2,
    day: 'Monday',
    shift: [0, 0, 0]

  },
  {
    id: 3,
    day: 'Tuesday',
    shift: [0, 0, 0]
  },
  {
    id: 4,
    day: 'Wednesday',
    shift: [0, 0, 0]

  },
  {
    id: 5,
    day: 'Thursday',
    shift: [0, 0, 0]

  },
  {
    id: 6,
    day: 'Friaday',
    shift: [0, 0, 0]

  },
  {
    id: 7,
    day: 'Satarday',
    shift: [0, 0, 0]
  },
];
export default function ColumnTypesGrid() {
  //const apiRef = useGridApiContext();
  const apiRef = useGridApiRef();
  const url = "https://localhost:44336/api/Employee/";

  const sendData = async () => {
    let mat = localStorage.getItem('matrix');
    console.log(mat);
    if (mat)
      mat = JSON.parse(mat);
    console.log('mat', mat)
    const empId = JSON.parse(localStorage.getItem("empId") || '{}');
    let saveMat = axios.post(`https://localhost:44336/api/Employee/GetPriorityTable?empid=${empId}`, mat)
    let response = await saveMat;
    if (response.status === 200) {
      console.log("OK!, your change succefuly");
    }
    localStorage.removeItem('matrix');
  }
  const [rows, setRows] = React.useState<RowData[]>(initialRows);
  const [columns, setColumns] = React.useState<Col[]>(initialCol);
  const s: Priority = { priority_id: 1, priority_description: "יכול" };
  const [arr, setArr] = React.useState([s])

  React.useEffect(
    () => {
      const url = "https://localhost:44336/api/Employee/";
      let employeePromise = axios.get(url + "GetAllPriority").
        then(res => {
          setArr(res.data)
          localStorage.setItem("priorityList", JSON.stringify(arr))
        });
      let shifrArrTimes = ["jnj"];
      let shifrArrDays = ["s"]
      axios.get(url + 'GoToDaysNames').then(res => { console.log(res.data); shifrArrDays = res.data }).then(i => {
        axios.get(url + 'GoToGetAllShiftsType').then(res => { console.log(res.data); shifrArrTimes = res.data }).then(x => {
          const pri = JSON.parse(localStorage.getItem("priorityList") || '{}');
          let pri2: { value: string, label: string }[] = []
          pri.map((item: Priority, index: number) => { pri2.push({ value: item.priority_id.toString() || '', label: item.priority_description }) })
          let data1: Array<Col> = [{
            field: 'day', width: 250, editable: true, valueOptions: [{ value: 'pp', label: 'll' }], type: 'text',
            headerName: 'string',
            renderEditCell: [{
              priority_description: "לא יכול",
              priority_id: 10
            }]
          }]
          for (let i = 0; i < shifrArrTimes.length; i++) {
            const element: Col = {
              field: i.toString(),
              headerName: shifrArrTimes[i],
              width: 120,
              editable: true,
              type: 'singleSelect',
              valueOptions: pri2,
              renderEditCell: (params: GridRenderEditCellParams) => <PriorityList2 {...params} />,
            }
            data1.push(element)
          }
          setColumns(data1)
          let s: RowData = { id: 0, day: "dcfg", shift: [0, 0, 0] }
          let data: RowData[] | null | string = [s];
          for (let i = 0; i < shifrArrDays.length; i++) {
            const element: RowData = {
              id: i,
              day: shifrArrDays[i],
              shift: [0, 0]
            }
            data.push(element)
          }
          console.log(data)
          setRows(data)
          setRows((prevRows) => prevRows.filter((row, i) => i !== 0));
        })
      })
    },
    [],
  );
  let navigate = useNavigate()
  const back = () => {
    navigate('/EmployeeMenu');
  }
  const onCellEditCommit1 = (params: GridCellParams) => {
    setRows((prevRows) =>
      prevRows.map((row) => {
        if (row.id === params.id) {
          const n = Number(params.field)

          const a = row.shift.map((x, i) => i + 1 === n ? params.value : x)
          return ({ ...row, shift: a })

        } return row
      }
      ),
    );
  }

  return (
    <>
      <div style={{ height: 500, width: '100%' }}>
        <DataGrid columns={columns}
          rows={rows}
        // experimentalFeatures={{ newEditingApi: true }}
        // onCellEditStop={onCellEditCommit1}
        // onCellEditCommit={(p: GridCellEditCommitParams)=>{
        // }}
        />
      </div>
      <Button variant="outlined" style={{ background: "blue", color: "yellow" }} type="submit" onClick={sendData}>
        טבלת משמרות אישור
      </Button>
      <Button variant="outlined" style={{ background: "blue", color: "yellow" }} type="submit" onClick={back}>
        חזרה
      </Button>
    </>
  );
}
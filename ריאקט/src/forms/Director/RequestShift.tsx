
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
import { DataGridPro } from '@mui/x-data-grid-pro';
import { PartyMode } from '@mui/icons-material';
// import { rowsMetaStateInitializer } from '@mui/x-data-grid/internals';
import { useNavigate } from 'react-router-dom';
import ShowPosition from '../../ShowPosition';
interface ShiftChoosenOption {
  id: number,
  name: string,
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
const initialCol: Col[] = [{
  field: 'day',
  width: 150,
  editable: true,
  headerName: 'day'
}
]
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
const RequestTable = () => {
  //const apiRef = useGridApiContext();
  const apiRef = useGridApiRef();
  const url = "https://localhost:44336/api/Employee/";
  const [rows, setRows] = React.useState<RowData[]>(initialRows);
  const [columns, setColumns] = React.useState<Col[]>(initialCol);
  //let shifrArrTimes = ["jnj"];
  // let shifrArrDays = days1;
  // React.useEffect(
  //   () => {
  const days1 = JSON.parse(localStorage.getItem("ArrDays") || '{}');
  const times2 = JSON.parse(localStorage.getItem("ArrTimes") || '{}');
  let shifrArrTimes = times2;
  let shifrArrDays = days1;
  // shifrArrDays = days1, shifrArrTimes = times2;

  let pri2: { value: string, label: string }[] = []
  let data1: Array<Col> = [{
    field: 'day', width: 200, editable: true, headerName: 'ימים'
  }]
  React.useEffect(() => {
    for (let i = 0; i < shifrArrTimes.length; i++) {
      const element: Col = {
        field: i.toString(),
        headerName: shifrArrTimes[i],
        width: 120,
        editable: true,
      }
      data1.push(element)

      setColumns(data1)
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
      setRows((prevRows) => prevRows.filter((row, i) => i !== 0));
    }
  }, [])

  const sendData = async () => {
    console.log(matrix);
    const JobId = localStorage.getItem("JobId");
    let employeePromise = axios.post(`https://localhost:44336/api/Employee/AddRequst?jobId=${JobId}`, matrix)
    let response = await employeePromise;
    if (response.status === 200) {
      console.log("OK!, your change succefuly");
    }
  }
  //   }
  // );
  const onCellEditCommit1 = (params: GridCellParams) => {
    setRows((prevRows) =>
      prevRows.map((row) => {
        if (row.id === params.id) {
          const n = Number(params.field)
          return ({ ...row })

        } return row
      }
      ),
    );
  }
  debugger;
  const days = JSON.parse(localStorage.getItem("DaysLenght") || '{}');
  const times = JSON.parse(localStorage.getItem("TimesLenght") || '{}');
  const [matrix, setMatrix] = React.useState(Array.from({ length: days }, v => Array.from({ length: times }, v => null)));
  console.log('mattttttttt', matrix)
  
  let navigate = useNavigate()
  const back = () => {
    navigate('/DirectorMenu');
  }
  const handleRowEditCommit = React.useCallback(
    (params: GridCellEditCommitParams) => {
      debugger;
      const row = params.id;
      const column = params.field;
      const value = params.value;
      let helpMat = [...matrix];
      helpMat[Number(row)][Number(column)] = value;
      console.log(helpMat)
      setMatrix(helpMat);
    },
    []
  );

  return (
    <>
      <br />
      <br />
      בחר תפקיד
      <ShowPosition />
      <div style={{ height: 500, width: '100%' }}>
        <DataGrid
          columns={columns}
          rows={rows}
          onCellEditCommit={handleRowEditCommit}
        // experimentalFeatures={{ newEditingApi: true }}

        />
      </div>
      <Button variant="outlined" style={{ background: "blue", color: "yellow" }} type="submit" onClick={sendData}>
        אישור בקשות לשיבוץ
      </Button>
      <Button variant="outlined" style={{ background: "blue", color: "yellow" }} type="submit" onClick={back}>
        חזרה
      </Button>
    </>
  );
}
export default RequestTable;




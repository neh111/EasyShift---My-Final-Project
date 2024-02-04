import React, { useEffect, useState } from "react";
import axios from "axios";
import { Select } from "@material-ui/core";
import { MenuItem } from "@material-ui/core";
import Priority from './classes/priority';
import { GridRenderEditCellParams, useGridApiContext, GridCellEditCommitParams, GridCellParams } from "@mui/x-data-grid";
import { RowData } from "./forms/set-priority-to-shifts/choosePriorityPerShift";
import { ObjectType } from "typescript";

const PriorityList2 = (props: GridRenderEditCellParams) => {
    const apiRef = useGridApiContext();

    const s: Priority = { priority_id: 1, priority_description: "יכול" };
    const [arr, setArr] = useState([s])
    // const [matrix, setMatrix] = React.useState(Array.from({ length: 7 }, v => Array.from({ length: 3 }, v => null)));
    // const [matrix, setMatrix] = useState([
    //     [null, null, null],
    //     [null, null, null],
    //     [null, null, null],
    //     [null, null, null],
    //     [null, null, null],
    //     [null, null, null],
    //     [null, null, null]
    // ]);

    const days = JSON.parse(localStorage.getItem("DaysLenght") || '{}');
    const times = JSON.parse(localStorage.getItem("TimesLenght") || '{}');
    const [choice, setChoice] = useState((props.row as RowData).shift[Number(props.field)]);
    const [matrix, setMatrix] = React.useState(Array.from({ length: days }, v => Array.from({ length: times }, v => null)));
    console.log( "in Priority", matrix);

    React.useEffect(
        () => {
            const url = "https://localhost:44336/api/Employee/";
            let employeePromise = axios.get(url + "GetAllPriority").
                then(res => {
                    setArr(res.data)
                    localStorage.setItem("priorityList", JSON.stringify(arr))
                });
        },
        [],
    );
    //localStorage.setItem("priorityList", JSON.stringify(arr))
    //  setArr(JSON.parse((localStorage.getItem("priorityList"))||'{}'))
    // useEffect(() => {
    //     const url = "https://localhost:44336/api/Employee/";
    //     let employeePromise = axios.get(url + "GetAllPriority").
    //         then(res => {
    //             setArr(res.data)
    //         }).then(a =>
    //             axios.get(url + 'GoToDaysNames').then(res => { shifrArrDays = res.data; console.log(res.data) }).
    //                 then(i => {
    //                     axios.get(url + 'GoToGetAllShiftsType').
    //                         then(res => { console.log(res.data); shifrArrTimes = res.data; console.log("shifttimes", shifrArrTimes) }).
    //                         then(x => { setMatrix(Array.from({ length: shifrArrDays.length }, v => Array.from({ length: shifrArrTimes.length }, v => null))) })
    //                 })
    //         )
    // }, []);
    const handleSelect = React.useCallback(
        (params: GridCellParams) => {
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
    const handleSelect1 = (value: Object) => {
        var storedSelect;
        const row = props.id;
        const column = props.field;
        let helpMap
        if (!(localStorage.getItem('matrix')))
            localStorage.setItem('matrix', JSON.stringify(matrix));
        helpMap = localStorage.getItem('matrix');
        if (helpMap)
            helpMap = JSON.parse(helpMap);
        if (Array.isArray(helpMap))
            helpMap[Number(row)][Number(column)] = value;
        localStorage.setItem('matrix', JSON.stringify(helpMap));
        console.log(helpMap);
    }
    return (

        <Select
            labelId="demo-simple-select-label"
            id="demo-simple-select"
            value={choice}
            defaultValue={"בחר עדיפות"}
            label="Choose template"
            onChange={(e) => {
                handleSelect1(Object(e.target).value);
                apiRef.current.setEditCellValue({ id: props.id, field: props.field, value: e.target.value })
            }}
        >
            {
                arr.map((p: Priority, index) => <MenuItem key={p.priority_id} value={p.priority_id}>{p.priority_description}</MenuItem>)
            }
        </Select>
    );
}

export default PriorityList2;
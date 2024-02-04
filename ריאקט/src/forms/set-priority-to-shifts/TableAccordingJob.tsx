import React from "react";
import { CSmartTable } from '@coreui/react-pro'
import { useEffect, useState } from "react";
import axios from "axios";
import { useNavigate } from 'react-router-dom';
import { Button } from '@material-ui/core';

interface Props {
    start: Date | null;
    end: Date | null;
}
const A = ({ start, end }: Props) => {
    const [users, setUsers] = useState([])
    const JobId = localStorage.getItem("JobId");
    const [shifrArrTimes, setShifrArrTimes] = useState<string[]>(["day"]);
    let navigate = useNavigate()
    const back = () => {
        navigate('/EmployeeMenu');
    }
    React.useEffect(() => {
        const url = "https://localhost:44336/api/Employee/";
        let shifrArrTimes = ["jnj"];
        let shifrArrDays = ["s"]
        axios.get(url + 'GoToDaysNames').then(res => { shifrArrDays = res.data; console.log(res.data) }).
            then(i => { axios.get(url + 'GoToGetAllShiftsType').then(res => { console.log(res.data); shifrArrTimes = res.data }) })
            .then(a =>
                axios.get(url + `GoToGetJobsToShiftByJob?jobId=${JobId}&start=${start?.toISOString()}&end=${end?.toISOString()}`).
                    then((res) => {
                        console.log(res.data)
                        let arr: any = [];
                        let d = 0;
                        for (let i = 0; i < shifrArrDays.length; i++) {
                            const element: any = {}
                            element['day'] = shifrArrDays[i]
                            for (let j = 0; j < shifrArrTimes.length; j++) {
                                element[shifrArrTimes[j]] = res.data[i]
                            }
                            arr.push(element)
                            d++;
                        }
                        console.log(arr)
                        setUsers(arr)

                    }))
    }, [])
    return (
        <>
            <CSmartTable
                items={users}
                columnFilter
                columnSorter
                pagination
                tableProps={{
                    hover: true,
                    responsive: true,
                }}
            />
            <Button variant="outlined" style={{ background: "blue", color: "yellow" }} type="submit" onClick={back}>
                חזרה
            </Button>
        </>
    )

}
export default A;
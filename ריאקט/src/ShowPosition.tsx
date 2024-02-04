import React, { useEffect, useState } from "react";
import axios from "axios";
import { Select } from "@material-ui/core";
import { MenuItem } from "@material-ui/core";
import Jobs_Dto from "./classes/Jobs_Dto";

const ShowPosition = () => {
    const s: Jobs_Dto = { job_id: 1, jobType_id: 5, description: "אחות", isSplit: 1 };
    const [arr, setArr] = useState([s])
    const [choice, setChoice] = useState<unknown>();
    const url = "https://localhost:44336/";
    const GetAllJobs = async () => {
        let employeePromise = axios.get(url + "api/Employee/GetAllJob");
        let response = await employeePromise;
        setArr(response.data);
    }
    const func = () => {
        let employeePromise = axios.get(`https://localhost:44336/api/Employee/GoToGetEmployeeJobIdByDescription?desc=${choice}`).
            then(res => localStorage.setItem("JobId", res.data)
            );
    }
    const selectChange = (e: React.ChangeEvent<{ value: unknown }>) => {
        const value = e.target.value;
        setChoice(value);
        localStorage.setItem("JobId", JSON.stringify(value))
        const JobId = JSON.parse(localStorage.getItem("JobId") || '{}');
        const d= JobId+1;
        localStorage.setItem("JobId",d);
    }

    return (
        <>
            <Select onOpen={GetAllJobs} onChange={selectChange}
                labelId="demo-simple-select-label"
                id="demo-simple-select"
                value={choice}
                defaultValue={"בחר תפקיד"}
                label="Choose template"
            >
                {
                    arr.map((p: Jobs_Dto, index=1) => <MenuItem key={p.job_id} value={index}>{p.description}</MenuItem>)
                }
            </Select>
        </>
    );
}

export default ShowPosition;

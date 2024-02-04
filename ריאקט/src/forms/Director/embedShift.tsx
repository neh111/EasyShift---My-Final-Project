import React from "react";
import axios from "axios";
import DelayingAppearance from "./DelayingAppearance";

const EmbedShift = () => {
    const checkUser = async (e: any) => {
        let employeePromise = axios.post("https://localhost:44336/api/Employee/");/*להוסיף שם פונקציה */
        let response = await employeePromise;
    }
    return (
        <DelayingAppearance/>
    )
}
export default EmbedShift;
import axios   from "axios";

export default axios.create({
    baseURL:'https://localhost:44336/',
    headers:{
        "Contect-type":"application/json"
    }
})
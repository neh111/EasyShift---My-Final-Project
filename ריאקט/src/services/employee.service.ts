import http from './http-common';
import Jobs_Dto from '../classes/Jobs_Dto';
 class EmployeeService {
    getJobs() {
        return http.get<Jobs_Dto[]>("/GetAllJob");
    }
    CheckPassEmp(){
        return http.get<Jobs_Dto>("/")
    }
}

export default new EmployeeService();
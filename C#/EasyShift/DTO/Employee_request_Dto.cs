using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
namespace DTO
{
    public class Employee_request_Dto
    {
        public int employee_request_id { get; set; }
        public int employee_id { get; set; }
        public int shift_id { get; set; }
        public int job_id { get; set; }
        public int priority_id { get; set; }
        public int status { get; set; }
        public System.DateTime request_date { get; set; }

        public Employee_request_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Employee_request_Dto, Employee_request_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Employee_request_tbl>(this);
        }


        public static Employee_request_Dto DalToDto(Employee_request_tbl e)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Employee_request_tbl, Employee_request_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Employee_request_Dto>(e);
        }
        public override bool Equals(object e)
        {
            Employee_request_Dto convertE = (Employee_request_Dto)e;
            return this.employee_request_id == convertE.employee_request_id &&
                this.employee_id == convertE.employee_id &&
                this.shift_id == convertE.shift_id &&
                this.job_id == convertE.job_id &&
                this.priority_id == convertE.priority_id &&
                this.status == convertE.status;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
 // [Serializable]
    public class Employee_Dto
    {
        public int employee_id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public int seniority_years { get; set; }
        public string employee_id_str { get; set; }
        public string mail { get; set; }
        public string cellphone_number { get; set; }
        public int num_shifts_in_week { get; set; }
        public int num_shifts_left_to_placement { get; set; }
        public int job_id { get; set; }
        public System.DateTime date_start_job { get; set; }
        
        public Employee_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Employee_Dto, Employee_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Employee_tbl>(this);
        }


        public static Employee_Dto DalToDto(Employee_tbl e)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Employee_tbl, Employee_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Employee_Dto>(e);
        }


    }
}

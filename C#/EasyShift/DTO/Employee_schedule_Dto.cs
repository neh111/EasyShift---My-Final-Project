using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
    class Employee_schedule_Dto
    {
        public int employee_schedule_id { get; set; }
        public int employee_id { get; set; }
        public int day { get; set; }
        public int shift_type_id { get; set; }
        public int priority { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime end_date { get; set; }
        public Employee_schedule_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Employee_schedule_Dto, Employee_schedule_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Employee_schedule_tbl>(this);
        }


        public static Employee_schedule_Dto DalToDto(Employee_schedule_tbl e)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Employee_schedule_tbl, Employee_schedule_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Employee_schedule_Dto>(e);
        }
    }
}

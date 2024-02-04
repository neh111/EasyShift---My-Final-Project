using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;
namespace DTO
{
    class Emoloyee_request_Dto
    {
        public int employee_request_id { get; set; }
        public int employee_id { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime end_date { get; set; }
        public int priority { get; set; }
        public Employee_request_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Emoloyee_request_Dto, Employee_request_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Employee_request_tbl>(this);
        }


        public static Emoloyee_request_Dto DalToDto(Employee_request_tbl e)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Employee_request_tbl, Emoloyee_request_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Emoloyee_request_Dto>(e);
        }

    }
}

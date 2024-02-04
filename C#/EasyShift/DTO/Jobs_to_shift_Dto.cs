using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
    public class Jobs_to_shift_Dto
    {
        public int jobs_to_shift_id { get; set; }
        public int job_id { get; set; }
        public int shift_id { get; set; }
        public int num_employees_requierd { get; set; }
        public System.DateTime request_date { get; set; }
        public Jobs_to_shift_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Jobs_to_shift_Dto, Jobs_to_shift_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Jobs_to_shift_tbl>(this);
        }


        public static Jobs_to_shift_Dto DalToDto(Jobs_to_shift_tbl s)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Jobs_to_shift_tbl, Jobs_to_shift_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Jobs_to_shift_Dto>(s);
        }
    }
}

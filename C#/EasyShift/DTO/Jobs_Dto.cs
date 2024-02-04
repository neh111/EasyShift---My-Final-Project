using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL;

namespace DTO
{
    public class Jobs_Dto
    {
        public int job_id { get; set; }
        public string description { get; set; }

        public Jobs_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Jobs_Dto, Jobs_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Jobs_tbl>(this);
        }


        public static Jobs_Dto DalToDto(Jobs_tbl e)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Jobs_tbl, Jobs_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Jobs_Dto>(e);
        }
    }
}

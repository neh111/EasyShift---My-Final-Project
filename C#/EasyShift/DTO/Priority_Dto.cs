using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
    public class Priority_Dto
    {
        public int priority_id { get; set; }
        public string priority_description { get; set; }

        public Priority_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Priority_Dto, Priority_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Priority_tbl>(this);
        }


        public static Priority_Dto DalToDto(Priority_tbl s)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Priority_tbl, Priority_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Priority_Dto>(s);
        }
    }
}

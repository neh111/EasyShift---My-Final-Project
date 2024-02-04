using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
    class Types_request_description_Dto
    {
        public int types_request_description_id { get; set; }
        public string description_text { get; set; }
        public Types_request_description_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Types_request_description_Dto, Types_request_description_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Types_request_description_tbl>(this);
        }


        public static Types_request_description_Dto DalToDto(Types_request_description_tbl t)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Types_request_description_tbl, Types_request_description_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Types_request_description_Dto>(t);
        }


    }
}

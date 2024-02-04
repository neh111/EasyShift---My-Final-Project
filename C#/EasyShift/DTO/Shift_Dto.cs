using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
     public class Shift_Dto
     {
        public int shift_id { get; set; }
        public int day { get; set; }
        public int shift_type_id { get; set; }
        public int IsPermittedSequence { get; set; }
        public Shift_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Shift_Dto, Shift_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Shift_tbl>(this);
        }


        public static Shift_Dto DalToDto(Shift_tbl s)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Shift_tbl, Shift_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Shift_Dto>(s);
        }
    }
}

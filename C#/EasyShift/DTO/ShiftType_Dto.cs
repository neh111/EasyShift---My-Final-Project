using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
   public class ShiftType_Dto
    {
        public int shift_type_id { get; set; }
        public System.TimeSpan beginning_time { get; set; }
        public System.TimeSpan end_time { get; set; }
        public string description { get; set; }
        public int IsPermittedSequence { get; set; }
        public ShiftType_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<ShiftType_Dto, ShiftType_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<ShiftType_tbl>(this);
        }


        public static ShiftType_Dto DalToDto(ShiftType_tbl s)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<ShiftType_tbl, ShiftType_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<ShiftType_Dto>(s);
        }
    }
}

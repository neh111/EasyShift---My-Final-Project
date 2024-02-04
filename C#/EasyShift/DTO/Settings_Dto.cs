using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
    public class Settings_Dto
    {
        public int num_paths { get; set; }
        public int num_employees { get; set; }

        public Settings_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<Settings_Dto, Settings_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<Settings_tbl>(this);
        }


        public static Settings_Dto DalToDto(Settings_tbl e)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<Settings_tbl, Settings_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<Settings_Dto>(e);
        }
    }
}

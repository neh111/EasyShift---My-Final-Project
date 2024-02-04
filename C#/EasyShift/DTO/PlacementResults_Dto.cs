using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using AutoMapper;

namespace DTO
{
    public class PlacementResults_Dto
    {
        public int result_id { get; set; }
        public int shift_id { get; set; }
        public int employee_id { get; set; }
        public int job_id { get; set; }
        public int statisfaction_level { get; set; }
        public System.DateTime placement_date { get; set; }
        public PlacementResults_tbl DtoTODal()
        {
            var config = new MapperConfiguration(cfg =>
                   cfg.CreateMap<PlacementResults_Dto, PlacementResults_tbl>()
               );
            var mapper = new Mapper(config);
            return mapper.Map<PlacementResults_tbl>(this);
        }


        public static PlacementResults_Dto DalToDto(PlacementResults_tbl p)
        {
            var config = new MapperConfiguration(cfg =>
                 cfg.CreateMap<PlacementResults_tbl, PlacementResults_Dto>()
             );
            var mapper = new Mapper(config);
            return mapper.Map<PlacementResults_Dto>(p);
        }
    }
}

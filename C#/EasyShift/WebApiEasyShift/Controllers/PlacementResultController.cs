using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL;
using DTO;


namespace WebApiEasyShift.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PlacementResultController : ApiController
    {
        ClassDB classDB = new ClassDB();
       
        [HttpGet]
        public List<List<string>>[,] DoPlacement(DateTime startDate, DateTime endDate)
        {
            return classDB.FinalPlacement(startDate, endDate);
        }
        [HttpGet]


        //קונטרולר להפעלת שיבוץ
        public int StartPlacement()
        {
            //להביא תאריך של היום
            Algorithm a = new Algorithm();
            a.RunAlgorithm();
            return 1;
        }
    }
}

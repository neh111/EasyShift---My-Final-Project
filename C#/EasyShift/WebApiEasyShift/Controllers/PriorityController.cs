using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using BLL;
using DTO;

namespace WebApiEasyShift.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PriorityController : ApiController
    {
        ClassDB classDB = new ClassDB();
        public RequestResult Get()
        {
            RequestResult r = classDB.GetAllPriorities();
            return r;
        }
    }
}
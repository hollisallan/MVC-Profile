using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class DivisionTypesController : ApiController
    {
        ListFillers lf = new ListFillers();
        // GET api/divisiontypes/5
        public List<DivisionType> Get()
        {
            return lf.GetDivisions();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class LocationTypesController : ApiController
    {
        ListFillers lf = new ListFillers();
        // GET api/locationtypes/5
        public LocationType Get(int LocTypeNbr, string LocTypeCode)
        {
            return lf.GetLocations(LocTypeNbr, LocTypeCode);
        }
       
    }
}

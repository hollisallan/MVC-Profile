using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class RegionTypesController : ApiController
    {
        ListFillers lf = new ListFillers();
        // GET api/regiontypes/5
        public RegionType Get(int RegionNbr)
        {            
            return lf.GetRegions(RegionNbr);
        }    
    }
}

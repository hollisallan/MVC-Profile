using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class ResourceTypesController : ApiController
    {
        ListFillers lf = new ListFillers();    
        // GET api/resourcetypes/5
        public ResourceType Get(int ResTypeNbr, string ResTypeCode)
        {
            return lf.GetResources(ResTypeNbr, ResTypeCode);
        }
    }
}

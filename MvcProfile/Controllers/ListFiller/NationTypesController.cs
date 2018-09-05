using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class NationTypesController : ApiController
    {
        ListFillers lf = new ListFillers();
        // GET api/nationtypes/5
        public NationType Get(int NtnNbr, bool ShowHidden)
        {             
            return lf.GetNations(NtnNbr, ShowHidden);
        }        
    }
}

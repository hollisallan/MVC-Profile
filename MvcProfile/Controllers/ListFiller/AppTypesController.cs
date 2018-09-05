using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{    
    public class AppTypesController : ApiController
    {
        ListFillers lf = new ListFillers();
        // GET api/default1/5
        public AppType Get(int AppNbr, string AppCode)
        {
            return lf.GetApps(AppNbr, AppCode);
        }     
    }
}

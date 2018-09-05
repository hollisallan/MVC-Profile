using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class PrivilegeTypesController : ApiController
    {
        ListFillers lf = new ListFillers();
        // GET api/privilegetypes/5
        public PrivilegeType Get(int PrivTypeNbr, string PrivTypeCode)
        {
            return lf.GetPrivileges(PrivTypeNbr, PrivTypeCode);
        }
        
    }
}

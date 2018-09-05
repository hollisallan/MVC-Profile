using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Company
{
    public class CoListRolesController : ApiController
    {
        MyCompany mc = new MyCompany();
        
        // GET api/listroles/5
        public CompanyValues Get(int VndNbr)
        {
            return mc.ListRoles(VndNbr);
        }        
    }
}

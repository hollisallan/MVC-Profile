using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Company
{
    public class CoDelController : ApiController
    {
        MyCompany mc = new MyCompany();
        // GET http://localhost:57559/api/CompanyDel/?dunsNbr=50143
        public CompanyValues Get(string dunsNbr)
        {
            return mc.DelCompany(dunsNbr);
        }     
    }
}

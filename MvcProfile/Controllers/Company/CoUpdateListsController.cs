using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Company
{
    public class CoUpdateListsController : ApiController
    {
        MyCompany mc = new MyCompany();

        // GET api/CoUpdateLists?vndNbr={vndNbr}
        //public  CompanyValues  Get(int vndNbr, [FromUri] List<CompanyRoles> roles,[FromUri] List<CompanyLocations> addresses, [FromUri] List<CompanyAccount> accounts )
        public CompanyValues Get(int vndNbr, [FromUri] string[] roles, [FromUri] string[] addresses, [FromUri] string[] accounts)
        {
            return mc.UpdateLists(vndNbr,  roles, addresses, accounts);
        }        
    }
}

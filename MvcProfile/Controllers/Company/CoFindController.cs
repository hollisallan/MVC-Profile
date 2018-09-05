using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Company
{
    public class CoFindController : ApiController
    {
        MyCompany c = new MyCompany();

        // GET api/cofind/5
        public List<FindCompany> Get(string VendorName, string DunsNbr, string AccountName, string AccountNbr, string VendorRoleNbr, string IncludeDisabled)
        {
            return c.Find(VendorName, DunsNbr, AccountName, AccountNbr, VendorRoleNbr, IncludeDisabled);
        }
    }
}

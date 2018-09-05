using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Company
{
    public class CoAddController : ApiController
    {
        MyCompany c = new MyCompany();

        // GET http://ma001xsweb90/mvcprofile/api/CompanyAdd/?name=Fake Company 01&dunsNbr=99123456789&iFlag=false
        public CompanyValues Get(string name, string dunsNbr, bool iFlag)
        {
            return c.AddCompany(name, dunsNbr, iFlag);
        }
  
    }
}

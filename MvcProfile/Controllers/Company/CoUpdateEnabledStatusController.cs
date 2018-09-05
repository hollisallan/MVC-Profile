using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Company
{
    public class CoUpdateEnabledStatusController : ApiController
    {
        MyCompany mc = new MyCompany();
        // http://localhost:57559/api/CoUpdateEnabledStatus/?VndNbr=2340&bEnabled=false
        public CompanyValues Get(int VndNbr, bool bEnabled)
        {
            return mc.UpdateEnabledStatus(VndNbr, bEnabled);
        }        
    }
}

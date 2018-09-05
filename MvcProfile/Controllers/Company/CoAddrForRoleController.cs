using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Company
{
    public class CoAddrForRoleController : ApiController
    {
        MyCompany mc = new MyCompany();

        // http://localhost:57559/api/CoAddrForRole/?VndNbr=41145&RoleNbr=2
        public List<CompanyLocations> Get(int VndNbr, int RoleNbr)
        {
            return mc.ListAddressForRole(VndNbr, RoleNbr);
        }
    }
}

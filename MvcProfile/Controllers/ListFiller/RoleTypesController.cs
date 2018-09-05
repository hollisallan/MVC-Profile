using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class RoleTypesController : ApiController
    {
        ListFillers lf = new ListFillers();
        // GET api/roletypes/5
        public RoleType Get(int VndRoleNbr, int PersonContactNbr)
        {
            return lf.GetRoles(VndRoleNbr, PersonContactNbr);
        }
        
    }
}

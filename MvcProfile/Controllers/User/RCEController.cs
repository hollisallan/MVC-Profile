using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.User
{
    public class RCEController : ApiController
    {
        MyUser mu = new MyUser();
        // GET api/rce/5
        public UserType Get(int PersonNbr)
        {
            return mu.RequiresCompanyEmail(PersonNbr);
        }

        
    }
}

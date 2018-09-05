using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.User
{
    public class SRAController : ApiController
    {
        MyUser mu = new MyUser();
        // GET api/sra/5
        public UserType Get(int UserID)
        {
            return mu.SetRecommendedAppRoles(UserID);
        }    
    }
}

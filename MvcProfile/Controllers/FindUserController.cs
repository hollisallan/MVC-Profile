using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;
using System.Data;

namespace MvcProfile.Controllers
{
    public class FindUserController : ApiController
    {
        FindUser fu = new FindUser();
     

        // GET api/finduser/5
        public FindUserProfile Get(string username)
        {
            return fu.SearchUser(username);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.User
{
    public class UESController : ApiController
    {
        MyUser mu = new MyUser();
        // GET api/ues/5
        public UserType Get(int UserID, bool Enabled)
        {
            return mu.UpdateEnabledStatus(UserID, Enabled);
        }
    }
}

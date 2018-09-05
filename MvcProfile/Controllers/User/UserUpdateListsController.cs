using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.User
{
    public class UserUpdateListsController : ApiController
    {
        MyUser u = new MyUser();
        // GET api/updatelists/5
        public UserType Get(int UserID, [FromUri] string[] AppRole, [FromUri] string[] Privileges, [FromUri] string[] Contacts)
        {
            return u.UpdateLists(UserID, AppRole, Privileges, Contacts);
        }        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.User
{
    public class UserFindController : ApiController
    {
        MyUser mu = new MyUser();
        // GET api/finduser/5
        public FindUserType Get(int PersonNbr, string UserName, string Email, string LastName, string VendorName, string Phone, string AccountName,
           string AccountNbr, int VendorRoleNbr, int PersonContactNbr, int EnabledFlag, int RowLimit, int SortBy)
        {
            return mu.FindUser(PersonNbr, UserName, Email, LastName, VendorName, Phone, AccountName, AccountNbr, VendorRoleNbr, PersonContactNbr, EnabledFlag, RowLimit, SortBy);
        }

       
    }
}

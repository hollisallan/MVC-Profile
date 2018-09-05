using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.User
{
    public class UserCopyAcctController : ApiController
    {
        MyUser mu = new MyUser();
        // GET api/usercopyacct/5
        public UserType Get(string FromUsername, int ToUserID, string AcctNbr)
        {
            return mu.CopyAccountsWithValidation(FromUsername, ToUserID, AcctNbr);             
        }
        
    }
}

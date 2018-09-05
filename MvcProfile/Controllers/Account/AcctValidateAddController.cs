using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Account
{
    public class AcctValidateAddController : ApiController
    {
        Accounts ac = new Accounts();
        // GET api/acctvalidateadd/5
        public string Get(string AccountID, string AccountNbr, [FromUri] string[] Companies, [FromUri] string[] Users)
        {
            return ac.ValidatedAdd(AccountID, AccountNbr, Companies, Users); ;
        }

        
    }
}

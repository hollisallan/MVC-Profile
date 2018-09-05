using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Account
{
    public class AcctFindController : ApiController
    {
        Accounts ac = new Accounts();        
        // GET api/acctfind/5
        public FindAccounts Get(string acctID, string acctTypeCd, string acctNbr, string acctName, string vndName, string dunsNbr)
        {
            return ac.FindMyAcct(acctID, acctTypeCd,acctNbr, acctName, vndName, dunsNbr);
        }

        
    }
}

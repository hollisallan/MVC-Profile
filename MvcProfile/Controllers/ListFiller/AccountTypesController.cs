using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class AccountTypesController : ApiController
    {
        ListFillers lf = new ListFillers();
        // GET api/accounttypes/5
        public AccountType Get(string AcctTypeCode)
        {
            return lf.GetAccounts(AcctTypeCode);
        }

       
    }
}

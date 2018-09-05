using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers
{
    public class ListAccountsController : ApiController
    {
        Accounts mla = new Accounts();

        // GET api/listaccounts
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/listaccounts/5
        public MyListAccount  Get(int id, string appcode)
        {
            return mla.SearchUser(id, appcode);
        }

        // POST api/listaccounts
        public void Post([FromBody]string value)
        {
        }

        // PUT api/listaccounts/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/listaccounts/5
        public void Delete(int id)
        {
        }
    }
}

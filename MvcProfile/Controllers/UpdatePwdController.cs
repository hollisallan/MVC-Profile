using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers
{
    public class UpdatePwdController : ApiController
    {
        // GET api/updatepwd
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/updatepwd/5
        public string Get(string userName, string oldPwd, string newPwd)
        {
            UpdatePass updatepass = new UpdatePass();

            return updatepass.UpdatePassword(userName, oldPwd, newPwd); ;
        }

        // POST api/updatepwd
        public void Post([FromBody]string value)
        {
        }

        // PUT api/updatepwd/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/updatepwd/5
        public void Delete(int id)
        {
        }
    }
}

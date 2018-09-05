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
    public class RolesController : ApiController
    {
        // GET api/roles
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET ma001xsweb90/mvcprofile/api/roles/?userID=49451&appCode=SRM
        public System.Data.DataSet Get(string userID, string appCode)
        {
            DataSet ds = new DataSet();

            try
            {
                Roles roles = new Roles();
                int USERID = Convert.ToInt32(userID);
                return roles.GetRoles(USERID, appCode);
            }
            catch { return ds; }            
        }

        // POST api/roles
        public void Post([FromBody]string value)
        {
        }

        // PUT api/roles/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/roles/5
        public void Delete(int id)
        {
        }
    }
}

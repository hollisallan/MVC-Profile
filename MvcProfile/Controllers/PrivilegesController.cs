﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;
using System.Data;

namespace MvcProfile.Controllers
{
    public class PrivilegesController : ApiController
    {        
        // GET api/privelages
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET ma001xsweb90/mvcprofile/api/roles/?userID=49451&appCode=SRM
        public DataSet Get(int id, string appCode)
        {
            DataSet ds = new DataSet();
            Privelages priv = new Privelages();

            ds = priv.GetPrivelages(id, appCode);
            return ds;
        }

        // POST api/privelages
        public void Post([FromBody]string value)
        {
        }

        // PUT api/privelages/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/privelages/5
        public void Delete(int id)
        {
        }
    }
}

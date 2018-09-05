using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class ContactTypesController : ApiController
    {
        ListFillers lf = new ListFillers(); 
        // GET api/contacttypes/5
        public List<ContactType> Get(string RoleCodes)
        {
            var ct = lf.GetContacts(RoleCodes);
            return ct;
        }       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.ListFiller
{
    public class ListUserTypesController : ApiController
    {
        ListFillers lf = new ListFillers();

        // GET api/listusertypes/5
        public PersonType Get(short PersonTypeNbr)
        {
            PersonType pt = new PersonType();
            pt = lf.Get(0);
            return pt;
        }

   
    }
}

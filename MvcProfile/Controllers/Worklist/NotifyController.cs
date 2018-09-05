using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Worklist
{
    public class NotifyController : ApiController
    {
        Worklists wl = new Worklists();
        // GET api/notify/5
        public WorkList Get(int OrderNbr, int NotifyType )
        {
            return wl.Notify(OrderNbr , NotifyType);
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.Worklist
{
    public class WLInsertController : ApiController
    {
        Worklists wl = new Worklists();
        // GET api/wlinsert/5
        public WorkList Get(int OrderNbr, string Instructions, string Comments, int AdminID, bool CompleteFlag)
        {
            return wl.WorkListIns(OrderNbr, Instructions, Comments, AdminID, CompleteFlag);
        }
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MvcProfile.Models;

namespace MvcProfile.Controllers.User
{    
    public class UserAddController : ApiController
    {
        MyUser mu = new MyUser();
        // GET api/useradd/5
        public string Get(string UserName, string Password, string FirstName, string MiddleName,
                            string LastName, string Title, string Status, string Email, string Phone, string PhoneExt, string AltPhone, string AltPhoneExt,
                            string Fax, int PersonTypeNbr, bool MustChgPswdFlag, bool NoChgPswdFlag, bool NoEmailFlag)
        {

            return mu.AddUser(UserName, Password, FirstName, MiddleName, LastName, Title, Status, Email, Phone, PhoneExt, AltPhone, AltPhoneExt, Fax,
                        PersonTypeNbr, MustChgPswdFlag, NoChgPswdFlag, NoEmailFlag);
            ;
        }        
    }
}

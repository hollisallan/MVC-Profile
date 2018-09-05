using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using MvcProfile.Models;

namespace MvcProfile
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Uri uri = new Uri("http://localhost:57559/api/login/?id=roint1&pwd=routing1");
            Response.Write(GetPageAsString(uri));
        }
        public static string GetPageAsString(Uri address)
        {
            string result = "";

            // Create the web request  
            HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
            

            //// Get response  
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                // Get the response stream  
                StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);                
                JavaScriptSerializer js = new JavaScriptSerializer();

                // Read the whole contents and return as a string  
                result = reader.ReadToEnd();
                // Convert to UserProfile
                Profile.UserProfile userprofile = (Profile.UserProfile)js.Deserialize(result, typeof(Profile.UserProfile));
            };

            return result;
        }



        
    }
}
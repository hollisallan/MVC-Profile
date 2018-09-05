using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Configuration;

namespace MvcProfile.Models
{
    public class Profile
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

        // CHANGES WERE MADE TO S_LOGIN STORED PROC
        public UserProfile Login(string login, string pwd)
        {

            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_LoginUser", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure                
            })
            {
                command.Parameters.AddWithValue("@UserName", login);
                command.Parameters.AddWithValue("@Password", pwd);
                var returnParameter = command.Parameters.Add("@RetVal", System.Data.SqlDbType.Int);
                returnParameter.Direction = System.Data.ParameterDirection.Output;
                var returnPwdFlag = command.Parameters.Add("@PwdFlag", System.Data.SqlDbType.Int);
                returnPwdFlag.Direction = System.Data.ParameterDirection.Output;

                conn.Open();
                command.ExecuteNonQuery();

                UserProfile userprofile = new UserProfile();
                userprofile.id = returnParameter.Value.ToString();
                userprofile.loginFlag = returnPwdFlag.Value.ToString();
                return userprofile;
                //return GetJSON(returnParameter.Value.ToString(), returnPwdFlag.Value.ToString()) ;
                //var serializer = new JavaScriptSerializer();
                // var data = serializer.Deserialize<UserProfile>(result);
                
            }

            
        }

        private string GetJSON(string personNbr, string pwdFlag)
        {
            var myObj = new UserProfile
            {
                id = personNbr,
                loginFlag = pwdFlag
            };
            return new JavaScriptSerializer().Serialize(myObj);
        }

        public class UserProfile
        {
            public string id;
            public string loginFlag;
        }
    }   
}
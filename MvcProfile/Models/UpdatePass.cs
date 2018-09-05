using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;

namespace MvcProfile.Models
{
    public class UpdatePass
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

        public string UpdatePassword(string userName, string oldPass, string newPass)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_UpdateUserPassword", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {                
                try
                {
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@OldPswd", oldPass);
                    command.Parameters.AddWithValue("@NewPswd", newPass);
                    var returnParameter = command.Parameters.Add("@RetVal", System.Data.SqlDbType.Int);
                    returnParameter.Direction = System.Data.ParameterDirection.Output;

                    conn.Open();
                    command.ExecuteNonQuery();

                    return returnParameter.Value.ToString();
                }
                catch(Exception ex)
                {
                    return ex.Message.ToString();
                }
                
            }
        }
    }
}
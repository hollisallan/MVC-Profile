using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace MvcProfile.Models
{
    public class Roles
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

        public DataSet GetRoles(int userID, string appCode)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetUserEnabledAppRoles", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                
                    command.Parameters.AddWithValue("@PersonNbr", userID);
                    command.Parameters.AddWithValue("@AppCode", appCode);                                       
                    conn.Open();
                    var adapter = new SqlDataAdapter(command);
                    var ds = new DataSet();
                    adapter.Fill(ds);                     

                    return ds;
                

            }
        }



    }
}
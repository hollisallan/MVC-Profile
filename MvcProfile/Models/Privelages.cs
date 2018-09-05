using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace MvcProfile.Models
{
    public class Privelages
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

        public DataSet GetPrivelages(int personNbr, string appCode)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetUserEnabledPrivs", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {

                command.Parameters.AddWithValue("@PersonNbr", personNbr);
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
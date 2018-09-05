using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace MvcProfile.Models
{
    public class FindUser
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

        public FindUserProfile SearchUser(string username)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_FindUsers", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                FindUserProfile fup = new FindUserProfile();
                command.Parameters.AddWithValue("@UserName", username);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                fup.username = ds.Tables[0].Rows[0][1].ToString();
                fup.password = ds.Tables[0].Rows[0][2].ToString();
                return fup;


            }
        }
    }

    public class FindUserProfile
    {
        public string username;
        public string password;
    }
}
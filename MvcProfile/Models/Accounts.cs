using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Oracle.DataAccess.Client; // ODP.NET Oracle managed provider
using Oracle.DataAccess.Types; 

namespace MvcProfile.Models
{
    public class Accounts
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;
        private static readonly string ORA_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["oraConnection"].ConnectionString;

        public MyListAccount SearchUser(int id, string appcode)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetUserAccounts", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                MyListAccount mla = new MyListAccount();
                command.Parameters.AddWithValue("@PersonNbr", id);
                command.Parameters.AddWithValue("@AcctTypeCode", appcode);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    mla.clientid = ds.Tables[0].Rows[0][2].ToString();                
                }                
                return mla;
            }
        }

        public FindAccounts FindMyAcct(string acctID, string acctTypeCd, string acctNbr, string acctName, string vndName, string dunsNbr)
        {//http://localhost:57559/api/AcctFind/?acctID=154&acctTypeCd=DUNS&acctNbr=00003506090&acctName=&vndName=&dunsNbr
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_FindAccounts", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                FindAccounts fa = new FindAccounts();
                command.Parameters.AddWithValue("@AccountID", Convert.ToInt32( acctID ) );
                command.Parameters.AddWithValue("@AcctTypeCode", acctTypeCd);
                command.Parameters.AddWithValue("@AccountNbr", acctNbr);
                command.Parameters.AddWithValue("@AccountName", acctName);
                command.Parameters.AddWithValue("@VndName", vndName);
                command.Parameters.AddWithValue("@DunsNbr", dunsNbr);

                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    fa.VND_ACCT_ID = ds.Tables[0].Rows[0][0].ToString();
                    fa.ACCT_TYPE_CODE = ds.Tables[0].Rows[0][1].ToString(); ;
                    fa.ACCT_NBR = ds.Tables[0].Rows[0][2].ToString(); ;
                    fa.ACCOUNT_NAME = ds.Tables[0].Rows[0][3].ToString();
                    fa.ENABLED_FLAG = ds.Tables[0].Rows[0][4].ToString();
                    fa.VND_NAME = ds.Tables[0].Rows[0][5].ToString();
                    fa.DUNS_NBR = ds.Tables[0].Rows[0][6].ToString();      
                }
                
                return fa;
            }        
        }

        private void TestOra()
        {
            string oradb = "Data Source=MPM;User Id=FARCVM1 ;Password=FARCVM1";
            OracleConnection conn = new OracleConnection(oradb);  // C#
            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "SELECT vnd_duns_nbr, vnd_name, vnd_nbr, version_nbr, 'ACCOUNT_CONNECTION_STRING' from vendor where rownum < 1";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            Console.Write ( dr.GetString(0) );
            conn.Dispose();
        }
        //MAKING AN ORACLE CALL FOR DATA
        //string[] Companies - MUST HAVE AN ACTION CODE AS THE FIRST PARAMETER
        //string[] Users - MUST HAVE AN ACTION CODE AS THE FIRST PARAMETER
        public string ValidatedAdd(string AccountID, string AccountNbr, string[] Companies, string[] Users)
        {//20162699  http://localhost:57559/api/AcctValidateAdd/?AccountID=2016269990&AccountNbr=2016269990&Companies[]=A,1,1&Companies[]=D,2,5896
            //TestOra();          
            string sQuery = "SELECT vnd_duns_nbr, vnd_name, vnd_nbr, version_nbr, 'ACCOUNT_CONNECTION_STRING' ";
            sQuery += " AS SRC FROM vendor WHERE vnd_duns_nbr =" + AccountNbr.Substring(0, (AccountNbr.Length -2));
            using (var conn = new OracleConnection(ORA_CONNECTION_STRING))
            using (var command = new OracleCommand(sQuery, conn)
            {
                CommandType = System.Data.CommandType.Text
            })
            {
                FindAccounts fa = new FindAccounts();
                try
                {
                    conn.Open();
                    var adapter = new OracleDataAdapter(command);
                    var ds = new DataSet();
                    adapter.Fill(ds);

                    if (ds.Tables[0].Rows.Count > 0)
                    {//CHECK IF VENDOR EXISTS
                        fa.VND_ACCT_ID = ds.Tables[0].Rows[0][0].ToString();
                        fa.ACCT_TYPE_CODE = "DUNS"; 
                        fa.ACCT_NBR = ds.Tables[0].Rows[0][2].ToString(); ;
                        fa.ACCOUNT_NAME = ds.Tables[0].Rows[0][3].ToString();
                        fa.ENABLED_FLAG = ds.Tables[0].Rows[0][4].ToString();

                        //ADD THE ACCOUNT i.e. Add(AccountID, 'DUNS', QualAccountNbr, RS.Fields['vnd_name'].Value, True, EmptyParam, EmptyParam);
                        AddAccount(fa.ACCT_TYPE_CODE, fa.ACCT_NBR, fa.ACCOUNT_NAME, true);

                        foreach (string c in Companies)
                        {
                            UpdateLists(fa.VND_ACCT_ID, Companies);
                        }

                        foreach (string u in Users)
                        {
                            Setusers(fa.VND_ACCT_ID, Users);
                        }

                    }
                }
                catch(Exception ex)
                {
                    return ex.Message.ToString();
                }                

            }

            return "Success!";
        }

        private void Setusers( string AccountID, string[] Users)
        {
            string[] users = Users[0].Split(',');
            switch (users[0].ToString())
            {
                case "A":
                    SetInsertUser(AccountID, users[1].ToString());
                    break;
                case "D":
                    SetDeleteUser(AccountID, users[1].ToString());
                    break;                
            }
        }

        private void SetInsertUser(string AccountID, string userID)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertUserAccount", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                MyListAccount mla = new MyListAccount();
                command.Parameters.AddWithValue("@PersonNbr", userID);
                command.Parameters.AddWithValue("@AcctID", AccountID);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);
            }
        }

        private void SetDeleteUser(string AccountID, string userID)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_DeleteUserAccount", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                MyListAccount mla = new MyListAccount();
                command.Parameters.AddWithValue("@VndNbr", userID);
                command.Parameters.AddWithValue("@AcctID", AccountID);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);
            }
        }

        private void UpdateLists( string AccountID, string[] Companies)
        {
            string[] companies = Companies[0].Split(',');
            switch (companies[0].ToString())
            {
                case "A":
                    SetInsertCompanies(companies[1].ToString(), AccountID);
                    break;
                case "D":
                    SetDeleteCompanies(companies[1].ToString(), AccountID);
                    break;                
            }
        }

        private void SetInsertCompanies(string VndNbr, string AcctID)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertCompanyAccount", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                MyListAccount mla = new MyListAccount();
                command.Parameters.AddWithValue("@VndNbr", VndNbr);
                command.Parameters.AddWithValue("@AcctID", AcctID);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);                               
            }
        }

        private void SetDeleteCompanies(string VndNbr, string AcctID)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_DeleteCompanyAccount", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                MyListAccount mla = new MyListAccount();
                command.Parameters.AddWithValue("@VndNbr", VndNbr);
                command.Parameters.AddWithValue("@AcctID", AcctID);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);
            }
        }

        private void AddAccount(string AccountTypCd, string AccountNbr, string Accountname, bool EnabledFlag)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertAccount", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                MyListAccount mla = new MyListAccount();
                command.Parameters.AddWithValue("@AcctTypeCode", AccountTypCd);
                command.Parameters.AddWithValue("@AccountNbr", AccountNbr);
                command.Parameters.AddWithValue("@AccountName", Accountname);
                command.Parameters.AddWithValue("@EnabledFlag", EnabledFlag);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    mla.clientid = ds.Tables[0].Rows[0][2].ToString();
                }              
            }            
        }
    }

    public class MyListAccount
    {
        public string clientid;
    }

    public class FindAccounts
    {
        public string VND_ACCT_ID { get; set; }
        public string ACCT_TYPE_CODE { get; set; }
        public string ACCT_NBR { get; set; }
        public string ACCOUNT_NAME { get; set; }
        public string ENABLED_FLAG { get; set; }
        public string VND_NAME { get; set; }
        public string DUNS_NBR { get; set; }
    }
}
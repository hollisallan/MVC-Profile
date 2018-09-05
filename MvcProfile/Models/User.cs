using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MvcProfile.Models
{
    public class MyUser
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;
        public UserType UpdateLists(int UserID, string[] AppRoles, string[] Privileges, string[] Contacts)
        {//"http://localhost:57559/api/UserUpdateLists/?UserID=7&AppRole[]=X,2&Privileges[]=X,1,1&Contacts[]=X,1,1
            var ut = new UserType();
            string[] roles = AppRoles[0].Split(',');
            string[] privileges = Privileges[0].Split(',');
            string[] contacts = Contacts[0].Split(',');

            switch (roles[0].ToString())
            {
                case "A":
                    AddRoles(AppRoles);
                    break;
                case "D":
                    DeleteRoles(AppRoles);
                    break;
            }
            switch (privileges[0].ToString())
            {
                case "A":
                    AddPrivileges(Privileges);
                    break;
                case "D":
                    DeletePrivileges(Privileges);
                    break;
            }
            switch (contacts[0].ToString())
            {
                case "A":
                    // AddRoles(Privileges);
                    break;
                case "D":
                    //DeleteRoles(Privileges);
                    break;
            }


            return ut;
        }

        public UserType AddRoles(string[] roles)
        {//http://localhost:57559/api/UserUpdateLists/?UserID=7&AppRole[]=A,2
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertUserAppRole", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                var ut = new UserType();
                string[] awv = roles[0].Split(',');
                command.Parameters.AddWithValue("@PersonNbr", awv[1]);
                command.Parameters.AddWithValue("@AppRoleNbr", awv[2]);
            
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                }

                return ut;
            }
        }
        public UserType DeleteRoles(string[] roles)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_DeleteUserAppRole", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                var ut = new UserType();
                string[] awv = roles[0].Split(',');
                command.Parameters.AddWithValue("@PersonNbr", awv[1]);
                command.Parameters.AddWithValue("@AppRoleNbr", awv[2]);
          
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                }

                return ut;
            }
        }
        public UserType AddPrivileges(string[] privileges)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertUserPrivilege", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                var ut = new UserType();
                string[] awv = privileges[0].Split(',');
                command.Parameters.AddWithValue("@PersonNbr", awv[1]);
                command.Parameters.AddWithValue("@PrivNbr", awv[2]);
               
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                }

                return ut;
            }
        }
        public UserType DeletePrivileges(string[] privileges)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_DeleteUserPrivilege", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                var ut = new UserType();
                string[] awv = privileges[0].Split(',');
                command.Parameters.AddWithValue("@PersonNbr", awv[1]);
                command.Parameters.AddWithValue("@PrivNbr", awv[2]);
       
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                }

                return ut;
            }
        }
        public UserType AddContacts(string[] contacts)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertUserContact", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                var ut = new UserType();
                string[] awv = contacts[0].Split(',');
                command.Parameters.AddWithValue("@PersonNbr", awv[1]);
                command.Parameters.AddWithValue("@AcctID", awv[2]);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                }

                return ut;
            }
        }
        public UserType DeleteContacts(string[] contacts)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_DeleteUserContact", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                var ut = new UserType();
                string[] awv = contacts[0].Split(',');
                command.Parameters.AddWithValue("@PersonNbr", awv[1]);
                command.Parameters.AddWithValue("@AcctID", awv[2]);              
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                }

                return ut;
            }
        }
        public UserType CopyAccountsWithValidation(string FromUsername, int ToUserID, string AccntNbr)
        {//http://localhost:57559/api/UserCopyAcct/?FromUsername=123&ToUserID=123&AcctNbr
            var ut = new UserType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_CopyUserAccounts", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                                
                command.Parameters.AddWithValue("@FromUsername", FromUsername);
                command.Parameters.AddWithValue("@ToPersonNbr", ToUserID);
                command.Parameters.AddWithValue("@AcctNbr", AccntNbr);
                var returnPwdFlag = command.Parameters.Add("@RetVal", System.Data.SqlDbType.Int);
                returnPwdFlag.Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                }

                return ut;
            }
            return ut;
        }
        public FindUserType FindUser(int PersonNbr, string UserName, string Email, string LastName, 
            string VendorName, string PrimaryPhone, string AccountName, 
            string AccountNbr, int VendorRoleNbr, int PersonContactNbr, 
            int EnabledFlag, int RowLimit, int SortBy)
        {//http://localhost:57559/api/UserFind/?PersonNbr=80566&UserName=b002472&Email=null&LastName=null&VendorName=null&Phone=null&AccountName=null&AccountNbr=null&VendorRoleNbr=0&PersonContactNbr=0&EnabledFlag=1&RowLimit=1&SortBy=1
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_FindUsers", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                var ut = new FindUserType();
                if (!PersonNbr.Equals("null"))
                {
                    command.Parameters.AddWithValue("@PersonNbr", PersonNbr);
                }
                else { command.Parameters.AddWithValue("@PersonNbr", DBNull.Value); }

                if (!UserName.Equals("null"))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                }
                else { command.Parameters.AddWithValue("@UserName", DBNull.Value); }

                if (!Email.Equals("null"))
                {
                    command.Parameters.AddWithValue("@Email", PersonNbr);
                }
                else { command.Parameters.AddWithValue("@Email", DBNull.Value); }

                if (!LastName.Equals("null"))
                {
                    command.Parameters.AddWithValue("@LastName", LastName);
                }
                else { command.Parameters.AddWithValue("@LastName", DBNull.Value); }

                if (!VendorName.Equals("null"))
                {
                    command.Parameters.AddWithValue("@VendorName", VendorName);
                }
                else { command.Parameters.AddWithValue("@VendorName", DBNull.Value); }

                if (!PrimaryPhone.Equals("null"))
                {
                    command.Parameters.AddWithValue("@PrimaryPhone", PrimaryPhone);
                }
                else { command.Parameters.AddWithValue("@PrimaryPhone", DBNull.Value); }

                if (!AccountName.Equals("null"))
                {
                    command.Parameters.AddWithValue("@AccountName", AccountName);
                }
                else { command.Parameters.AddWithValue("@AccountName", DBNull.Value); }

                if (!AccountNbr.Equals("null"))
                {
                    command.Parameters.AddWithValue("@AccountNbr", AccountNbr);
                }
                else { command.Parameters.AddWithValue("@AccountNbr", DBNull.Value); }
                
                if (VendorRoleNbr > 0)
                {
                    command.Parameters.AddWithValue("@VendorRoleNbr", VendorRoleNbr);
                }
                else { command.Parameters.AddWithValue("@VendorRoleNbr", DBNull.Value); }
                
                if (PersonContactNbr > 0)
                {
                    command.Parameters.AddWithValue("@PersonContactNbr", PersonContactNbr);
                }
                else { command.Parameters.AddWithValue("@PersonContactNbr", DBNull.Value); }

                command.Parameters.AddWithValue("@EnabledFlag", EnabledFlag);
                command.Parameters.AddWithValue("@RowLimit", RowLimit);
                command.Parameters.AddWithValue("@SortBy", SortBy);
            
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                    foreach (DataRow theRow in ds.Tables[0].Rows)
                    {
                        ut.USER_ID = theRow[0].ToString();
                        ut.USER_NAME = theRow[1].ToString();
                        ut.USER_PSWD = theRow[2].ToString();
                        ut.ONLINE_FLAG = theRow[3].ToString();
                        ut.ENABLED_FLAG = theRow[4].ToString();
                        ut.PERSON_NAME_FIRST = theRow[5].ToString();
                        ut.PERSON_NAME_MDDL = theRow[6].ToString();
                        ut.PERSON_NAME_LAST = theRow[7].ToString();
                        ut.EMAIL = theRow[8].ToString();
                        ut.PHONE = theRow[9].ToString();                                                
                    }
                }
                catch (Exception ex)                {
                    ut.RetVal = ex.ToString();
                }

                return ut;
            }

        }
        public string AddUser(string UserName, string Password, string FirstName, string MiddleName,
                string LastName, string Title, string Status, string Email, string Phone, string PhoneExt, string AltPhone, string AltPhoneExt,
                string Fax,  int PersonTypeNbr, bool MustChgPswdFlag, bool NoChgPswdFlag,
                bool NoEmailFlag)
        {//http://localhost:57559/api/UserAdd/?UserName=XXX&Password&FirstName=xxx&MiddleName=xxx&LastName=xxx&Title=MR&Status=1&Email=xxx@xxx.com&Phone=123-123-1234&PhoneExt=999&AltPhone=123-123-1234&AltPhoneExt=999&Fax=123-123-1234&PersonTypeNbr=99&MustChgPswdFlag=False&NoChgPswdFlag=False&NoEmailFlag=False
            var aut = new AddUserType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertUser", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                try
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    command.Parameters.AddWithValue("@Password", Password);
                    command.Parameters.AddWithValue("@FirstName", FirstName);
                    command.Parameters.AddWithValue("@MiddleName", MiddleName);
                    command.Parameters.AddWithValue("@LastName", LastName);
                    command.Parameters.AddWithValue("@Title", Title);
                    command.Parameters.AddWithValue("@Status", Status);
                    command.Parameters.AddWithValue("@Email", Email);
                    command.Parameters.AddWithValue("@Phone", Phone);
                    command.Parameters.AddWithValue("@PhoneExt", PhoneExt);
                    command.Parameters.AddWithValue("@AltPhone", AltPhone);
                    command.Parameters.AddWithValue("@AltPhoneExt", AltPhoneExt);
                    command.Parameters.AddWithValue("@Fax", Fax);
                    command.Parameters.AddWithValue("@PersonTypeNbr", PersonTypeNbr);
                    command.Parameters.AddWithValue("@MustChgPswdFlag", MustChgPswdFlag);
                    command.Parameters.AddWithValue("@NoChgPswdFlag", NoChgPswdFlag);
                    command.Parameters.AddWithValue("@NoEmailFlag", NoEmailFlag);
                    
                    var returnParameter = command.Parameters.Add("@RetVal", System.Data.SqlDbType.Int);
                    returnParameter.Direction = System.Data.ParameterDirection.Output;

                    conn.Open();
                    command.ExecuteNonQuery();

                    return "Success!";
                }
                catch (Exception ex)
                {
                    return ex.Message.ToString();
                }
            }            
        }
        public UserType RequiresCompanyEmail(int PersonNbr)
        {
            var ut = new UserType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_CheckUserRequiresCoEmail", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {

                command.Parameters.AddWithValue("@PersonNbr", PersonNbr);

                var returnPwdFlag = command.Parameters.Add("@RetVal", System.Data.SqlDbType.Int);
                returnPwdFlag.Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                    ut.RetVal = returnPwdFlag.Value.ToString();
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                }
                return ut;
            }
        }
        public UserType SetRecommendedAppRoles(int UserID)
        {//http://localhost:57559/api/SRA/?UserID=7
            var ut = new UserType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_UpdateUserRcmdAppRoles", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                try
                {
                    command.Parameters.AddWithValue("@PersonNbr", UserID);                                       
                    conn.Open();
                    command.ExecuteNonQuery();
                    ut.RetVal = "Success!";
                    return ut;
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                    return ut;
                }
            }            
        }
        public UserType UpdateEnabledStatus(int UserID, bool Enabled)
        {//http://localhost:57559/api/UES/?UserID=7&Enabled=True
            var ut = new UserType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_UpdateUserEnabledStatus", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                try
                {
                    command.Parameters.AddWithValue("@PersonNbr", UserID);
                    command.Parameters.AddWithValue("@Enabled", Enabled);
                    conn.Open();
                    command.ExecuteNonQuery();
                    ut.RetVal = "Success!";
                    return ut;
                }
                catch (Exception ex)
                {
                    ut.RetVal = ex.Message.ToString();
                    return ut;
                }
            }
        }
    }

    public class UserType
    {
        public string RetVal { get; set; }
    }
    public class FindUserType
    {
        public string USER_ID { get; set; }
        public string USER_NAME { get; set; }
        public string USER_PSWD { get; set; }
        public string ONLINE_FLAG { get; set; }
        public string ENABLED_FLAG { get; set; }
        public string PERSON_NAME_FIRST { get; set; }
        public string PERSON_NAME_MDDL { get; set; }
        public string PERSON_NAME_LAST { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public string RetVal { get; set; }
    }
    public class AddUserType
    {
        string UserID { get; set; }
        string UserName { get; set; }
        string Password { get; set; }
        string FirstName { get; set; }
        string MiddleName { get; set; }
        string LastName { get; set; }
        string Title { get; set; }
        string Status { get; set; }
        string Email { get; set; }
        string Phone { get; set; }
        string PhoneExt { get; set; }
        string AltPhone { get; set; }
        string AltPhoneExt { get; set; }
        string Fax { get; set; }
        int PersonTypeNbr { get; set; }
        bool MustChgPswdFlag { get; set; }
        bool NoChgPswdFlag { get; set; }
        bool NoEmailFlag { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MvcProfile.Models
{    
    public class MyCompany
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;

        //RETURNS THE VENDOR NUMBER, VENDOR ROLE NUMBER 
        public CompanyValues AddCompany(string name, string duns, bool iFlag)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertCompany", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                command.Parameters.AddWithValue("@VndName", name);
                command.Parameters.AddWithValue("@DunsNbr", duns);
                command.Parameters.AddWithValue("@InternalFlag", iFlag);
                var returnPwdFlag = command.Parameters.Add("@RetVal", System.Data.SqlDbType.Int);
                returnPwdFlag.Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);                    
                    cv.retVal = returnPwdFlag.Value.ToString();
                    
                }
                catch(Exception ex)
                {
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
            
        }

        public CompanyValues DelCompany(string duns)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_DeleteCompany", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                command.Parameters.AddWithValue("@VndNbr", duns);                                
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);                    
                    cv.retVal  = "0";                                       
                }
                catch (Exception ex)
                {
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        public CompanyValues ListRoles(int VndNbr)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetCompanyRoles", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                command.Parameters.AddWithValue("@VndNbr", VndNbr);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                    List<string> lVal = new List<string>();
                    foreach (DataRow theRow in ds.Tables[0].Rows)
                    {
                        lVal.Add(theRow[0] + "," + theRow[1]);
                    }

                    cv.roles = lVal;                    
                }
                catch (Exception ex)
                {
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        public List<CompanyLocations> ListAddressForRole(int VndNbr, int RoleNbr)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetCompanyLocsForRole", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {                
                //CompanyValues cv = new CompanyValues();
                List<CompanyLocations> lcl = new List<CompanyLocations>();
                command.Parameters.AddWithValue("@VndNbr", VndNbr);
                command.Parameters.AddWithValue("@RoleNbr", RoleNbr);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                    
                    foreach (DataRow theRow in ds.Tables[0].Rows)
                    {
                        CompanyLocations cl = new CompanyLocations();       
                        cl.ADDRESS_NBR = theRow[0].ToString();
                        cl.VND_NBR = theRow[1].ToString();
                        cl.VND_ROLE_NBR = theRow[2].ToString();
                        cl.VND_ROLE_DESC = theRow[3].ToString();
                        cl.ADDR_SEQ_NBR = theRow[4].ToString();
                        cl.LOC_NAME = theRow[5].ToString();
                        cl.LOC_ABBR = theRow[6].ToString();
                        cl.HQ_ADDR_FLAG = theRow[7].ToString();
                        cl.DUNS_SUF = theRow[8].ToString();

                        cl.LOC_TYPE_NBR = theRow[9].ToString();
                        cl.LOC_TYPE_CODE = theRow[10].ToString();
                        cl.DIV_NBR = theRow[11].ToString();
                        cl.AP_CODE = theRow[12].ToString();
                        cl.CONV_CODE = theRow[13].ToString();
                        cl.ENABLED_FLAG = theRow[14].ToString();
                        cl.ADDR_LINE_1 = theRow[15].ToString();
                        cl.ADDR_LINE_2 = theRow[16].ToString();

                        cl.ADDR_LINE_3 = theRow[17].ToString();
                        cl.ADDR_LINE_4 = theRow[18].ToString();
                        cl.CITY = theRow[19].ToString();
                        cl.ZIP_CODE = theRow[20].ToString();
                        cl.STATE = theRow[21].ToString();
                        cl.NON_US_STATE = theRow[22].ToString();

                        cl.NTN_NBR = theRow[23].ToString();
                        cl.REGION_NBR = theRow[24].ToString();
                        cl.EMAIL = theRow[25].ToString();
                        cl.PHONE = theRow[26].ToString();
                        cl.ALT_PHONE = theRow[27].ToString();
                        cl.FAX = theRow[28].ToString();

                        lcl.Add(cl);
                        
                    }                    
                }
                catch (Exception ex)
                {
                    
                }                 
                return lcl;
            }
        }

        public CompanyValues AddRoles(string[] roles)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertCompanyRole", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                string[] awv = roles[0].Split(',');
                command.Parameters.AddWithValue("@VndNbr", awv[1]);
                command.Parameters.AddWithValue("@RoleNbr", awv[2]);
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
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        public CompanyValues DeleteRoles(string[] roles)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_DeleteCompanyRole", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                string[] awv = roles[0].Split(',');
                command.Parameters.AddWithValue("@VndNbr", awv[1]);
                command.Parameters.AddWithValue("@RoleNbr", awv[2]);
                
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);                    
                }
                catch (Exception ex)
                {
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        public CompanyValues AddAddresses(string[] addresses)
        {//http://localhost:57559/api/CoUpdateLists/?vndNbr=11&roles[]=X,11,1&addresses[]=A,11,0,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,225,4,0,0,0,0,0,0&accounts[]=A,0,0
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertCompanyLocation", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                string[] awv = addresses[0].Split(',');
                command.Parameters.AddWithValue("@VndNbr", awv[1]);
                command.Parameters.AddWithValue("@RoleNbr", awv[2]);
                command.Parameters.AddWithValue("@LocName", awv[3]);
                command.Parameters.AddWithValue("@LocAbbr", awv[4]);
                command.Parameters.AddWithValue("@DunsSuffix", awv[5]);
                command.Parameters.AddWithValue("@LocTypeNbr", awv[6]);
                command.Parameters.AddWithValue("@DivNbr", awv[7]);
                command.Parameters.AddWithValue("@APCode", awv[8]);
                command.Parameters.AddWithValue("@ConvCode", awv[9]);
                command.Parameters.AddWithValue("@HqFlag", awv[10]);
                command.Parameters.AddWithValue("@EnabledFlag", awv[11]);
                command.Parameters.AddWithValue("@AddrLine1", awv[12]);
                command.Parameters.AddWithValue("@AddrLine2", awv[13]);

                command.Parameters.AddWithValue("@AddrLine3", awv[14]);
                command.Parameters.AddWithValue("@AddrLine4", awv[15]);
                command.Parameters.AddWithValue("@City", awv[16]);

                command.Parameters.AddWithValue("@Zipcode", awv[17]);
                command.Parameters.AddWithValue("@State", awv[18]);
                command.Parameters.AddWithValue("@NonUSState", awv[19]);
                command.Parameters.AddWithValue("@NtnNbr",awv[20]);
                command.Parameters.AddWithValue("@RegionNbr", awv[21]);
                command.Parameters.AddWithValue("@Phone", awv[22]);
                command.Parameters.AddWithValue("@PhoneExt", awv[23]);
                command.Parameters.AddWithValue("@Email", awv[24]);
                command.Parameters.AddWithValue("@AltPhone", awv[25]);
                command.Parameters.AddWithValue("@AltPhoneExt", awv[26]);
                command.Parameters.AddWithValue("@Fax", awv[27]);                
                var returnPwdFlag = command.Parameters.Add("@RetVal", System.Data.SqlDbType.Int);
                returnPwdFlag.Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                    cv.retVal = returnPwdFlag.ToString();
                }
                catch (Exception ex)
                {
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        public CompanyValues DeleteAddresses(string[] addresses)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_DeleteCompanyLocation", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                string[] awv = addresses[0].Split(',');
                command.Parameters.AddWithValue("@VndNbr", awv[1]);
                command.Parameters.AddWithValue("@RoleNbr", awv[2]);
                command.Parameters.AddWithValue("@AddrSeqNbr", awv[3]);                

                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        public CompanyValues UpdateAddresses(string[] addresses)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_UpdateCompanyLocation", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                string[] awv = addresses[0].Split(',');
                command.Parameters.AddWithValue("@VndNbr", awv[1]);
                command.Parameters.AddWithValue("@RoleNbr", awv[2]);
                command.Parameters.AddWithValue("@AddrSeqNbr", awv[3]);
                command.Parameters.AddWithValue("@HqFlag", awv[4]);
                command.Parameters.AddWithValue("@EnabledFlag", awv[5]);
                command.Parameters.AddWithValue("@LocName", awv[6]);

                command.Parameters.AddWithValue("@LocAbbr", awv[7]);
                command.Parameters.AddWithValue("@DunsSuffix", awv[8]);
                command.Parameters.AddWithValue("@LocTypeNbr", awv[9]);
                command.Parameters.AddWithValue("@DivNbr", awv[10]);

                command.Parameters.AddWithValue("@APCode", awv[11]);
                command.Parameters.AddWithValue("@ConvCode", awv[12]);
                command.Parameters.AddWithValue("@AddrLine1", awv[13]);
                command.Parameters.AddWithValue("@AddrLine2", awv[14]);

                command.Parameters.AddWithValue("@AddrLine3", awv[15]);
                command.Parameters.AddWithValue("@AddrLine4", awv[16]);
                command.Parameters.AddWithValue("@City", awv[17]);
                command.Parameters.AddWithValue("@Zipcode", awv[18]);
                command.Parameters.AddWithValue("@State", awv[19]);
                command.Parameters.AddWithValue("@NonUSState", awv[20]);
                command.Parameters.AddWithValue("@NtnNbr", awv[21]);
                command.Parameters.AddWithValue("@RegionNbr", awv[22]);
                command.Parameters.AddWithValue("@Phone", awv[23]);
                command.Parameters.AddWithValue("@PhoneExt", awv[24]);
                command.Parameters.AddWithValue("@Email", awv[25]);

                command.Parameters.AddWithValue("@AltPhone", awv[26]);
                command.Parameters.AddWithValue("@AltPhoneExt", awv[27]);
                command.Parameters.AddWithValue("@Fax", awv[28]);
                command.Parameters.AddWithValue("@PrefMediumTypeNbr", awv[29]);

                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                }
                catch (Exception ex)
                {
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        public CompanyValues InsertAccount(string[] accounts)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_InsertCompanyAccount", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                string[] awv = accounts[0].Split(',');
                command.Parameters.AddWithValue("@VndNbr", awv[1]);
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
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        public CompanyValues DeleteAccount(string[] accounts)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_DeleteCompanyAccount", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                string[] awv = accounts[0].Split(',');
                command.Parameters.AddWithValue("@VndNbr", awv[1]);
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
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        //public List<FindCompany> Find(string VendorName, string DunsNbr, string AcctName, string AccountNbr, int VendorRoleNbr, bool IncludeDisabled)
        public List<FindCompany> Find(string VendorName, string DunNbr, string AccountName, string AccountNbr, string VendorRoleNbr, string IncludeDisabled)
        {//http://localhost:57559/api/CoFind/?VendorName=AMI&DunsNbr=%20&AccountName=%20&AccountNbr=%20&VendorRoleNbr=2&IncludeDisabled=False
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_FindCompanies", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
               
                List<FindCompany> lfc = new List<FindCompany>();
                command.Parameters.AddWithValue("@VendorName", VendorName);
                command.Parameters.AddWithValue("@DunsNbr", DunNbr);
                command.Parameters.AddWithValue("@AccountName", AccountName);
                command.Parameters.AddWithValue("@AccountNbr", AccountNbr);
                command.Parameters.AddWithValue("@VendorRoleNbr", VendorRoleNbr);
                command.Parameters.AddWithValue("@IncludeDisabled", IncludeDisabled);




                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);

                    foreach (DataRow theRow in ds.Tables[0].Rows)
                    {
                        FindCompany fc = new FindCompany();
                        fc.ENABLED_FLAG = theRow[0].ToString();
                        fc.VND_NBR = theRow[1].ToString();
                        fc.VND_NAME = theRow[2].ToString();
                        fc.DUNS_NBR = theRow[3].ToString();
                        fc.INTERNAL_FLAG = theRow[4].ToString();
                        fc.VND_ACCT_ID = theRow[5].ToString();
                        fc.ACCT_TYPE_CODE = theRow[6].ToString();
                        fc.ACCT_NBR = theRow[7].ToString();
                        fc.ACCOUNT_NAME = theRow[8].ToString();
                        fc.ACCT_ENABLED_FLAG = theRow[9].ToString();
                        fc.VND_ROLE_NBR = theRow[10].ToString();
                        fc.VND_ROLE_CODE = theRow[11].ToString();
                        fc.VND_ROLE_DESC = theRow[12].ToString();                        
                        lfc.Add(fc);
                    }
                }
                catch (Exception ex)
                {

                }

                return lfc;
            }
        }

        public CompanyValues UpdateEnabledStatus(int VndNbr, bool bEnabled)
        {
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_UpdateCompanyEnabledStatus", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                CompanyValues cv = new CompanyValues();
                command.Parameters.AddWithValue("@VndNbr", VndNbr);
                command.Parameters.AddWithValue("@Enabled", bEnabled);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();

                try
                {
                    adapter.Fill(ds);
                    cv.retVal = "0";
                }
                catch (Exception ex)
                {
                    cv.retVal = ex.Message.ToString();
                }

                return cv;
            }
        }

        public CompanyValues UpdateLists(int VndNbr, string[] Roles, string[] Addresses, string[] Accounts)
        {
            CompanyValues cv = new CompanyValues();

            string[] roles = Roles[0].Split(',');
            switch ( roles[0].ToString() )
            {
                    case "A":
                    AddRoles(Roles);
                    break;
                    case "D":
                    DeleteRoles(Roles);
                    break;
            }

            string[] addresses = Addresses[0].Split(',');
            switch (addresses[0].ToString())
            {
                case "A":
                    AddAddresses(Addresses);
                    break;
                case "D":
                    DeleteAddresses(Addresses);
                    break;
                case "U":
                    UpdateAddresses(Addresses);
                    break;
            }

            string[] accounts = Accounts[0].Split(',');
            switch (accounts[0].ToString())
            {
                case "A":
                    InsertAccount(Accounts);
                    break;
                case "D":
                    DeleteAccount(Accounts);
                    break;
            }
            
            return cv;
        }
    }

    public class CompanyValues
    {
        public string retVal { get; set; }
        public List<string> roles;
        public List<CompanyLocations> addressRoles;
    }

    public class CompanyLocations
    {
        public string ADDRESS_NBR {get; set;}
        public string VND_NBR {get; set;}
        public string VND_ROLE_NBR {get; set; }
        public string VND_ROLE_DESC {get; set; }
        public string ADDR_SEQ_NBR { get; set; }
        public string LOC_NAME { get; set; }
        public string LOC_ABBR { get; set; }
        public string HQ_ADDR_FLAG { get; set; }
        public string DUNS_SUF { get; set; }
        public string LOC_TYPE_NBR { get; set; }
        public string LOC_TYPE_CODE { get; set; }
        public string DIV_NBR { get; set; }
        public string AP_CODE { get; set; }
        public string CONV_CODE { get; set; }
        public string ENABLED_FLAG { get; set; }
        public string ADDR_LINE_1 { get; set; }
        public string ADDR_LINE_2 { get; set; }
        public string ADDR_LINE_3 { get; set; }
        public string ADDR_LINE_4 { get; set; }
        public string CITY { get; set; }
        public string ZIP_CODE { get; set; }
        public string STATE { get; set; }
        public string NON_US_STATE { get; set; }
        public string NTN_NBR { get; set; }
        public string REGION_NBR { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public string ALT_PHONE { get; set; }
        public string FAX { get; set; }

    }

    public class FindCompany
    {
        public string ENABLED_FLAG { get; set; }
        public string VND_NBR { get; set; }
        public string VND_NAME { get; set; }
        public string DUNS_NBR { get; set; }
        public string INTERNAL_FLAG { get; set; }
        public string VND_ACCT_ID { get; set; }
        public string ACCT_TYPE_CODE { get; set; }
        public string ACCT_NBR { get; set; }
        public string ACCOUNT_NAME { get; set; }
        public string ACCT_ENABLED_FLAG { get; set; }
        public string VND_ROLE_NBR { get; set; }
        public string VND_ROLE_CODE { get; set; }
        public string VND_ROLE_DESC { get; set; }
    }    
}
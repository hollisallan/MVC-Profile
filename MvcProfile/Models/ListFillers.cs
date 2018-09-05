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
    public class ListFillers
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;
        private static readonly string ORA_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["oraConnection"].ConnectionString;
        public PersonType Get(short PersonTypeNbr)
        {//http://localhost:57559/api/ListUserTypes/?PersonTypeNbr=0
            PersonType pt = new PersonType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetUserType", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {                
                command.Parameters.AddWithValue("@PersonTypeNbr", PersonTypeNbr);                
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    pt.person_type_nbr = ds.Tables[0].Rows[0][0].ToString();
                    pt.person_type_cd = ds.Tables[0].Rows[0][1].ToString();
                    pt.person_type_desc = ds.Tables[0].Rows[0][2].ToString();
                }

                return pt;
            }
        }
        public  List<ContactType> GetContacts(string RoleCodes)
        {//http://localhost:57559/api/ContactTypes/?RoleCodes=1,1

            PersonType pt = new PersonType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetContactsByRole", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {

                command.Parameters.AddWithValue("@RoleCodes", RoleCodes);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);
                var ct = new List<ContactType>();

                // foreach (DataRow row in  ds.Tables[0].Rows)
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    ct.Add(new ContactType
                    {
                        PERSON_CONTACT_NBR = dr[0].ToString(),
                        PERSON_CONTACT_CODE = dr[1].ToString(),
                        PERSON_CONTACT_DESC = dr[2].ToString(),
                        CONTACT_DISPLAY_TEXT = dr[3].ToString(),
                        USER_OPTION_FLAG = dr[4].ToString()
                    });
                }
                    
                

                return ct;
            }
        }
        public RegionType GetRegions(int RegionNbr)
        {//http://localhost:57559/api/RegionTypes/?RegionNbr=1
            RegionType rt = new RegionType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetRegion", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@RegionNbr", RegionNbr);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rt.REGION_NBR = ds.Tables[0].Rows[0][0].ToString();
                    rt.REGION_DESC = ds.Tables[0].Rows[0][1].ToString();                    
                }

                return rt;
            }
        }
        public NationType GetNations(int NtnNbr, bool ShowHidden)
        {//http://localhost:57559/api/NationTypes/?NtnNbr=1&ShowHidden=true
            NationType nt = new NationType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetNation", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@NtnNbr", NtnNbr);
                command.Parameters.AddWithValue("@ShowHidden", ShowHidden);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    nt.NTN_NBR = ds.Tables[0].Rows[0][0].ToString();
                    nt.NTN_CODE = ds.Tables[0].Rows[0][1].ToString();
                    nt.NTN_DESC = ds.Tables[0].Rows[0][2].ToString();
                    nt.HIDDEN_FLAG = ds.Tables[0].Rows[0][3].ToString();
                }

                return nt;
            }
        }
        public RoleType GetRoles(int VndRoleNbr, int PersonContactNbr)
        {//http://localhost:57559/api/RoleTypes/?VndRoleNbr=0&PersonContactNbr=0
            var rt = new RoleType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetRoleContact", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@VndRoleNbr", VndRoleNbr);
                command.Parameters.AddWithValue("@PersonContactNbr", PersonContactNbr);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rt.VND_ROLE_NBR = ds.Tables[0].Rows[0][0].ToString();
                    rt.VND_ROLE_CODE = ds.Tables[0].Rows[0][1].ToString();
                    rt.VND_ROLE_DESC = ds.Tables[0].Rows[0][2].ToString();
                    rt.PERSON_CONTACT_NBR = ds.Tables[0].Rows[0][3].ToString();
                    rt.PERSON_CONTACT_CODE = ds.Tables[0].Rows[0][4].ToString();
                    rt.PERSON_CONTACT_DESC = ds.Tables[0].Rows[0][5].ToString();
                }

                return rt;
            }
        }
        public LocationType GetLocations(int LocTypeNbr, string LocTypeCode)
        {//http://localhost:57559/api/LocationTypes/?LocTypeNbr=2&LocTypeCode=BLK
            var rt = new LocationType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetLocationType", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@LocTypeNbr", LocTypeNbr);
                command.Parameters.AddWithValue("@LocTypeCode", LocTypeCode);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rt.LOC_TYPE_NBR = ds.Tables[0].Rows[0][0].ToString();
                    rt.LOC_TYPE_CODE = ds.Tables[0].Rows[0][1].ToString();
                    rt.LOC_TYPE_DESC = ds.Tables[0].Rows[0][2].ToString();                
                }

                return rt;
            }
        }
        public AccountType GetAccounts(string AcctTypeCode)
        {//http://localhost:57559/api/AccountTypes/?AcctTypeCode=DUNS
            var rt = new AccountType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetAccountType", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@AcctTypeCode", AcctTypeCode);
                
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rt.ACCT_TYPE_CODE = ds.Tables[0].Rows[0][0].ToString();
                    rt.ACCT_TYPE_DESC = ds.Tables[0].Rows[0][1].ToString();                    
                }

                return rt;
            }
        }
        public AppType GetApps(int AppNbr, string AppCode)
        {//http://localhost:57559/api/AppTypes/?AppNbr=1&AppCode=APQUERY
            var rt = new AppType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetApp", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@AppNbr", AppNbr);
                command.Parameters.AddWithValue("@AppCode", AppCode);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rt.APP_NBR = ds.Tables[0].Rows[0][0].ToString();
                    rt.APP_CODE= ds.Tables[0].Rows[0][1].ToString();
                    rt.APP_DESC = ds.Tables[0].Rows[0][2].ToString();
                }

                return rt;
            }
        }
        public PrivilegeType GetPrivileges(int PrivTypeNbr, string PrivTypeCode)
        {//http://localhost:57559/api/PrivilegeTypes/?PrivTypeNbr=2&PrivTypeCode=VIEW
            var rt = new PrivilegeType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetPrivilegeType", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@PrivTypeNbr", PrivTypeNbr);
                command.Parameters.AddWithValue("@PrivTypeCode", PrivTypeCode);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rt.PRIV_TYPE_NBR = ds.Tables[0].Rows[0][0].ToString();
                    rt.PRIV_TYPE_CODE = ds.Tables[0].Rows[0][1].ToString();
                    rt.PRIV_TYPE_DESC = ds.Tables[0].Rows[0][2].ToString();
                    rt.WGT_FACTOR = ds.Tables[0].Rows[0][3].ToString();
                }

                return rt;
            }
        }
        public ResourceType GetResources(int ResTypeNbr, string ResTypeCode)
        {//http://localhost:57559/api/ResourceTypes/?ResTypeNbr=2&ResTypeCode=PAGE
            var rt = new ResourceType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetResourceType", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@ResTypeNbr", ResTypeNbr);
                command.Parameters.AddWithValue("@ResTypeCode", ResTypeCode);
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rt.RES_TYPE_NBR = ds.Tables[0].Rows[0][0].ToString();
                    rt.RES_TYPE_CODE = ds.Tables[0].Rows[0][1].ToString();
                    rt.RES_TYPE_DESC = ds.Tables[0].Rows[0][2].ToString();                    
                }

                return rt;
            }
        }
        public List<DivisionType> GetDivisions()
        {//http://localhost:57559/api/DivisionTypes
            DivisionType pt = new DivisionType();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_GetDivisions", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {                
                conn.Open();
                var adapter = new SqlDataAdapter(command);
                var ds = new DataSet();
                adapter.Fill(ds);
                var ct = new List<DivisionType>();

                // foreach (DataRow row in  ds.Tables[0].Rows)
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    DataRow dr = ds.Tables[0].Rows[i];
                    ct.Add(new DivisionType
                    {
                        ADDRESS_NBR = dr[0].ToString(),
                        DIV_NAME = dr[1].ToString(),
                        DIV_ABBR = dr[2].ToString(),
                        DIV_NBR = dr[3].ToString(),
                        AP_CODE = dr[4].ToString(),
                        LOCN_LOC_NBR = dr[5].ToString(),
                        LOC_NAME = dr[6].ToString(),
                    });
                }

                return ct;
            }

        }
    }

    public class PersonType
    {        
       public string person_type_nbr { get; set; }
       public string person_type_cd { get; set; }
       public string person_type_desc { get; set; }
    }

    public class ContactType
    {
        public string PERSON_CONTACT_NBR { get; set; }
        public string PERSON_CONTACT_CODE { get; set; }
        public string PERSON_CONTACT_DESC { get; set; }
        public string CONTACT_DISPLAY_TEXT { get; set; }
        public string USER_OPTION_FLAG { get; set; }
    }

    public class RegionType
    {
        public string REGION_NBR { get; set; }        
        public string REGION_DESC { get; set; }
    }

    public class NationType
    {
        public string NTN_NBR { get; set; }
        public string NTN_CODE { get; set; }
        public string NTN_DESC { get; set; }
        public string HIDDEN_FLAG { get; set; }
    }
    public class RoleType
    {
        public string VND_ROLE_NBR { get; set; }
        public string VND_ROLE_CODE { get; set; }
        public string VND_ROLE_DESC { get; set; }
        public string PERSON_CONTACT_NBR { get; set; }
        public string PERSON_CONTACT_CODE { get; set; }
        public string PERSON_CONTACT_DESC { get; set; }
    }
    public class LocationType
    {
        public string LOC_TYPE_NBR { get; set; }
        public string LOC_TYPE_CODE { get; set; }
        public string LOC_TYPE_DESC { get; set; }
    }
    public class AccountType
    {
        public string ACCT_TYPE_CODE { get; set; }
        public string ACCT_TYPE_DESC { get; set; }
    }
    public class AppType
    {
        public string APP_NBR { get; set; }
        public string APP_CODE { get; set; }
        public string APP_DESC { get; set; }
    }
    public class PrivilegeType
    {
        public string PRIV_TYPE_NBR { get; set; }
        public string PRIV_TYPE_CODE { get; set; }
        public string PRIV_TYPE_DESC { get; set; }
        public string WGT_FACTOR { get; set; }
    }
    public class ResourceType
    {
        public string RES_TYPE_NBR { get; set;  }
        public string RES_TYPE_CODE { get; set; }
        public string RES_TYPE_DESC { get; set; }
    }
    public class DivisionType
    {
        public string ADDRESS_NBR { get; set; }
        public string DIV_NAME { get; set; }
        public string DIV_ABBR { get; set; }
        public string DIV_NBR { get; set; }
        public string AP_CODE { get; set; }
        public string LOCN_LOC_NBR { get; set; }
        public string LOC_NAME { get; set; }
    }
}
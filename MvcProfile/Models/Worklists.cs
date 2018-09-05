using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Net.Mail;

namespace MvcProfile.Models
{
    public class Worklists
    {
        private static readonly string SQL_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["sqlConnection"].ConnectionString;
        private static readonly string ORA_CONNECTION_STRING = ConfigurationManager.ConnectionStrings["oraConnection"].ConnectionString;
        public WorkList WorkListIns(int OrderNbr, string Instructions, string Comments, int AdminID, bool CompletedFlag)
        {//http://localhost:57559/api/WLInsert/?OrderNbr=1&Instructions=Testing&Comments=NULL&AdminID=1&CompleteFlag=false
            var wl = new WorkList();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_wInsertItem", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                command.Parameters.AddWithValue("@OrderNbr", OrderNbr);
                command.Parameters.AddWithValue("@Instructions", Instructions);
                command.Parameters.AddWithValue("@Comments", Comments);
                command.Parameters.AddWithValue("@AdminId", AdminID);
                command.Parameters.AddWithValue("@CompletedFlag", CompletedFlag);
                var returnParameter = command.Parameters.Add("@RetVal", System.Data.SqlDbType.Int);
                returnParameter.Direction = System.Data.ParameterDirection.Output;
                conn.Open();
                command.ExecuteNonQuery();
                wl.RetVal = returnParameter.Value.ToString();

                return wl;
            }            
        }
        public WorkList UpdateVndRole(int OrderNbr, int VndRoleNbr, string StatusCode)
        {                        
            var wl = new WorkList();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_wUpdateVndRole", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {
                try
                {
                    command.Parameters.AddWithValue("@OrderNbr", OrderNbr);
                    command.Parameters.AddWithValue("@VndRoleNbr", VndRoleNbr);
                    command.Parameters.AddWithValue("@StatusCode", StatusCode);
                    conn.Open();
                    command.ExecuteNonQuery();
                    wl.RetVal = "Sucess!";
                }
                catch (Exception ex)
                {
                    wl.RetVal = ex.Message.ToString();
                }

                return wl;
            }
        }

        public WorkList Notify(int OrderNbr, int NotifyType)
        {//http://localhost:57559/api/Notify/?OrderNbr=1&NotifyType=1
            var wl = new WorkList();
            var oh = new OrderHeader();
            oh = GetOrderHeader(OrderNbr);
            MailAddress from = new MailAddress("helpdesk@macysnet.com");
            MailAddress to = new MailAddress(oh.EMAIL);           
            MailMessage message = new MailMessage(from, to);
            
            var PPPNote= "In addition, please access "
               + "http://www.macysnet.com/Routing/ShippingUpdateForm.html "
               + "to submit the Shipment Address Update Form "
               + "to activate your company-specific e-mail address "
               + "for communication of shipment specific information. ";
            switch (NotifyType)
            {
                case 1:
                    message.Subject = "MacysNet Registration";
                    message.Body = @"You will be notified when your account is activated. " + PPPNote;
                    break;
                case 2:
                    message.Subject = "Your MacysNet Account Is Ready";
                    message.Body = @"Your account is activated. You may access MacysNet at http://www.macysnet.com " + PPPNote;
                    break;
                case 3:
                    message.Subject = "Your MacysNet Profile";
                    message.Body = @"Your MacysNet profile has been updated. " + PPPNote;
                    break;
                case 4:
                    message.Subject = "Your MacysNet account Is Ready";
                    message.Body = @"Your account is activated. You may access MacysNet at http://www.macysnet.com " + PPPNote;
                    break;
                default:
                    message.Subject = "MacysNet Registration Error";
                    message.Body = @"Registration OrderNbr ";
                    break;
            }
            
            SmtpClient client = new SmtpClient(   "appmail.fds.com" );            

            try
            {
                client.Send(message);                
                wl.RetVal = "Success";
            }
            catch (Exception ex)
            {
                wl.RetVal = ex.ToString();
            }

            return wl;
        }
        private OrderHeader GetOrderHeader(int OrderNbr)
        {
            var oh = new OrderHeader();
            var ds = new DataSet();
            using (var conn = new SqlConnection(SQL_CONNECTION_STRING))
            using (var command = new SqlCommand("s_wGetOrderHeader", conn)
            {
                CommandType = System.Data.CommandType.StoredProcedure
            })
            {                
                command.Parameters.AddWithValue("@OrderNbr", OrderNbr);                
                conn.Open();
                var adapter = new SqlDataAdapter(command);                
                adapter.Fill(ds);
                
                if (ds.Tables[0].Rows.Count > 0)
                {
                    oh.ORDER_NBR = ds.Tables[0].Rows[0][0].ToString();
                    oh.PERSON_NBR = ds.Tables[0].Rows[0][1].ToString();
                    oh.PERSON_NAME_FIRST = ds.Tables[0].Rows[0][2].ToString();

                    oh.PERSON_NAME_MDDL = ds.Tables[0].Rows[0][3].ToString();
                    oh.PERSON_NAME_LAST = ds.Tables[0].Rows[0][4].ToString();
                    oh.USER_NAME = ds.Tables[0].Rows[0][5].ToString();
                    oh.USER_PSWD = ds.Tables[0].Rows[0][6].ToString();
                    oh.EMAIL = ds.Tables[0].Rows[0][7].ToString();
                    oh.PHONE = ds.Tables[0].Rows[0][8].ToString();

                    oh.HAS_DETAILS = ds.Tables[0].Rows[0][9].ToString();
                    oh.STATUS_DESC = ds.Tables[0].Rows[0][10].ToString();
                }
            }

            return oh;
        }
    }

    public class WorkList
    {
        public string RetVal { get; set; }
    }
    public class OrderHeader
    {
        public string ORDER_NBR { get; set; }
        public string PERSON_NBR { get; set; }
        public string PERSON_NAME_FIRST { get; set; }
        public string PERSON_NAME_MDDL { get; set; }
        public string PERSON_NAME_LAST { get; set; }
        public string USER_NAME { get; set; }
        public string USER_PSWD { get; set; }
        public string EMAIL { get; set; }
        public string PHONE { get; set; }
        public string HAS_DETAILS { get; set; }
        public string STATUS_DESC { get; set; }
    }
}
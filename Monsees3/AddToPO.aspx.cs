using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Management;
using BasicFrame.WebControls;
using System.Net.Mail;
using System.Text;


// Logout
// D.S.Harmor
// Description - Allows a user to Logout to a specific Job
// 08/19/2011 - Initial Version
// 
// 

namespace Monsees
{
    public partial class AddToPO : System.Web.UI.Page
    {
        private string ContactID;
        private Int32 CompanyID;
        
        private string POID;       
        private string MonseesConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
            Int32 CustomerRecord = 0;
            ResultMsg.Text = "";
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            if ((Session["Authenticate"] != null) && (Convert.ToBoolean(Session["Authenticate"]) == true))
            {
                if (Request.QueryString["CID"] != null)
                    ContactID = Request.QueryString["CID"];

                if (Request.QueryString["POID"] != null)
                    POID = Request.QueryString["POID"];


				try
				{
					CompanyID = Convert.ToInt32(GetCompanyID(Int32.Parse(ContactID)));
				}
				catch(Exception)
				{
					//No contact found.
				}
                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                string sqlstring = "Select [CompanyID] FROM [PurchaseOrders] WHERE [POID] = " + POID + ";";
                // create a connection with sqldatabase 
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                // create a sql command which will user connection string and your select statement string
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
                System.Data.SqlClient.SqlDataReader reader;
                // open a connection with sqldatabase
                con.Open();

                // execute sql command and store a return values in reade
                reader = comm.ExecuteReader();

                while (reader.Read())
                {

                    CustomerRecord = Convert.ToInt32(reader["CompanyID"].ToString());
                   
                }
                con.Close();

                if (CustomerRecord != CompanyID)
                {
                    Response.Redirect("Default.aspx");
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
        }




        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            ServiceDataSource.ConnectionString = MonseesConnectionString;
            int[] nos = new int[Convert.ToInt32(txtNo.Text)];
            for (int i = 0; i < nos.Length; i++)
                nos[i] = i + 1;
            GridView1.DataSource = nos;
            GridView1.DataBind();
            
            
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            int result;
            System.Data.SqlClient.SqlParameter ret;
            string sessionval;
            StringBuilder emailbody = new StringBuilder("");
            MailMessage message = new MailMessage();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("CreateSession", con); 
            cmd.Parameters.Clear();            
            cmd.CommandType = CommandType.StoredProcedure; 
            cmd.Parameters.AddWithValue("@CID", Convert.ToInt32(ContactID));
            cmd.Parameters.AddWithValue("@Company", CompanyID);
            cmd.Parameters.AddWithValue("@POID", Convert.ToInt32(POID));
            cmd.Parameters.AddWithValue("@Body", EmailText.Text);
            con.Open();
            ret = cmd.CreateParameter();
            ret.ParameterName = "returnvalue";
            ret.DbType = DbType.Int32;
            ret.Direction = ParameterDirection.ReturnValue;
            cmd.Parameters.Add(ret);

            emailbody.Append("<html><body><table width='80%'><tr><td colspan='6'>" + EmailText.Text + "<br><br></td></tr><tr>"); 

            result = cmd.ExecuteNonQuery();

            sessionval = cmd.Parameters["returnvalue"].Value.ToString();
            con.Dispose();
            con.Close();
            string sqlstring = "SELECT CompanyName FROM CustomerDB WHERE CustomerID = " + CompanyID;

            System.Data.SqlClient.SqlConnection con2 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            System.Data.SqlClient.SqlCommand cmd2 = new System.Data.SqlClient.SqlCommand(sqlstring, con2);
            con2.Open();
            System.Data.SqlClient.SqlDataReader reader = cmd2.ExecuteReader();
            while (reader.Read())
            {
                emailbody.Append("<td><br>Company Name:</td><td>" + reader["CompanyName"].ToString() + "<br></td></tr><tr>");
            }
            con2.Close();
            emailbody.Append("<td>Part #</td><td>Rev #</td><td>Description</td><td>Qty</td><td>Req. Dlvry</td><td>Service</td></tr><tr>");
            foreach (GridViewRow gvRow in GridView1.Rows)
            {
                TextBox PartNumber1 = (TextBox)gvRow.FindControl("PartNumber1");
                TextBox Description1 = (TextBox)gvRow.FindControl("Description1");
                TextBox RevNumber1 = (TextBox)gvRow.FindControl("RevNumber1");
                TextBox Quantity1 = (TextBox)gvRow.FindControl("Quantity1");
                FileUpload myFileTest = (FileUpload)gvRow.FindControl("filMyFileTest");
                BDPLite Delivery1 = (BDPLite)gvRow.FindControl("Delivery1");
                DropDownList ServiceDropdownList1 = (DropDownList)gvRow.FindControl("ServiceDropdownList1");
                if (myFileTest.HasFile)
                {

                    myFileTest.SaveAs(System.IO.Path.Combine(Server.MapPath("Files"), myFileTest.FileName));              
                    message.Attachments.Add(new Attachment(System.IO.Path.Combine(Server.MapPath("Files"), myFileTest.FileName)));
                    //update db using the name of the file corresponding to RowID
                }
                
                System.Data.SqlClient.SqlConnection con1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("AddtoPO", con1); 
                cmd1.Parameters.Clear();
                cmd1.CommandType = CommandType.StoredProcedure;
                
                cmd1.Parameters.AddWithValue("@CID", Convert.ToInt32(ContactID));
                cmd1.Parameters.AddWithValue("@Company", CompanyID);
                cmd1.Parameters.AddWithValue("@POID", Convert.ToInt32(POID));
                cmd1.Parameters.AddWithValue("@Part", PartNumber1.Text);
                emailbody.Append("<td>" + PartNumber1.Text + "</td>");
                cmd1.Parameters.AddWithValue("@Rev", RevNumber1.Text);
                emailbody.Append("<td>" + RevNumber1.Text + "</td>");
                cmd1.Parameters.AddWithValue("@Desc", Description1.Text);
                emailbody.Append("<td>" + Description1.Text + "</td>");
                cmd1.Parameters.AddWithValue("@Qty", Convert.ToInt32(Quantity1.Text));
                emailbody.Append("<td>" + Quantity1.Text + "</td>");
                cmd1.Parameters.AddWithValue("@Delivery", Delivery1.SelectedDate);
                emailbody.Append("<td DataFormatString='{0:d}'>" + Delivery1.SelectedDate + "</td>");
                cmd1.Parameters.AddWithValue("@Notes", ServiceDropdownList1.SelectedValue);
                emailbody.Append("<td>" + ServiceDropdownList1.SelectedItem + "</td>");
                cmd1.Parameters.AddWithValue("@Session", sessionval);
                con1.Open();
                cmd1.ExecuteNonQuery();
                con1.Close();
                con1.Dispose();
                emailbody.Append("</tr>");
            }
            emailbody.Append("</table></body></html>");
            
            
                
                message.From = new MailAddress("neworders@monseestool.com");
                message.To.Add(new MailAddress("jason.spurling@monseestool.com"));
                message.Body = emailbody.ToString();
                message.IsBodyHtml = true;
                message.Subject = "New Order Received";
                if (filMyFile.PostedFile != null)
                {
                    HttpPostedFile myFile = filMyFile.PostedFile;
                    int nFileLen = myFile.ContentLength;
                    byte[] myData = new byte[nFileLen];
                    string FileName = Path.GetFileName(filMyFile.PostedFile.FileName);
                    myFile.InputStream.Read(myData, 0, nFileLen);
                    filMyFile.PostedFile.SaveAs(Server.MapPath(FileName));
                    message.Attachments.Add(new Attachment(Server.MapPath(FileName)));
                }

                SmtpClient emailWithAttach = new SmtpClient();
                emailWithAttach.Host = "localhost";
                emailWithAttach.DeliveryMethod = SmtpDeliveryMethod.Network;
                emailWithAttach.UseDefaultCredentials = true;
                emailWithAttach.Send(message);


                Response.Redirect("Receipt.aspx?Session=" + sessionval);

            
        }

        private void LoadServices()
        {
            string sqlstring = "Select [ServiceID],Service from [Services] where Active = 1 ORDER BY Service ASC";

            // create a connection with sqldatabase 
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            // create a sql command which will user connection string and your select statement string
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
            System.Data.SqlClient.SqlDataReader reader;
            // open a connection with sqldatabase
            con.Open();

            // execute sql command and store a return values in reade
            reader = comm.ExecuteReader();
            while (reader.Read())
            {
                //ServiceDropDownList.Items.Add(reader["Service"].ToString());
                
            }
            con.Close();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string sqlstring = "Select [ServiceID],Service from [Services] where Active = 1 ORDER BY Service ASC";
            foreach (GridViewRow gvRow in GridView1.Rows)
            {
                DropDownList ServiceDropdownList1 = (DropDownList)gvRow.FindControl("ServiceDropdownList1");

                // create a connection with sqldatabase 
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                // create a sql command which will user connection string and your select statement string
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
                System.Data.SqlClient.SqlDataReader reader;
                // open a connection with sqldatabase
                con.Open();

                // execute sql command and store a return values in reade
                reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    //ServiceDropDownList1.Items.Add(reader["Service"].ToString());

                }
                con.Close();
            }
        }

        public string GetCompanyID(Int32 ContactID)
        {
            string sqlstring = "SELECT CustomerID FROM Contact WHERE ContactID = " + ContactID;
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            // create a connection with sqldatabase 
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            // create a sql command which will user connection string and your select statement string
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
            System.Data.SqlClient.SqlDataReader reader;
            // open a connection with sqldatabase
            con.Open();


            // execute sql command and store a return values in reade
            reader = comm.ExecuteReader();
            string result = null;

            // check if reader hase any value then return true otherwise return false
            if (reader.Read())
            {
                result = reader["CustomerID"].ToString();

            }
            else
            {

                result = null;
            }
            con.Close();
            return result;

        }
    }
}

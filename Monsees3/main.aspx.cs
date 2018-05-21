using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

// ActiveJobs
// D.S.Harmor
// Description - View of all Active Production Jobs allowing users to log in and out of Jobs
// 08/19/2011 - Initial Version
// 
// Known Issues
//-------------------
// 08/19/2011 - LoggedInViewGrid ProcessID field should be invisible but when it is the value is unable to be read.

namespace Monsees
{
    public partial class _main : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        private string MatPriceID;
        private Int32 ContactID;
        private Int32 CompanyID;
        public string CompanyName;
       

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Session["Authenticate"] != null) && (Convert.ToBoolean(Session["Authenticate"]) == true))
            {
                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                MonseesSqlDataSourcePermissions.ConnectionString = MonseesConnectionString;
                ContactID = Int32.Parse(Session["CustomerID"].ToString());
                CompanyID = Convert.ToInt32(GetCompanyID(ContactID));
                CompanyName = Session["Customer"].ToString();
                MonseesSqlDataSourcePermissions.SelectCommand = @"--Use monsees2 
																declare @true bit declare @false bit SET @true = 1 SET @false = 0 Select Link, Description From PermissionList WHERE ContactID = " + ContactID;
                
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

                           
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
                      
            LinkButton btn = (LinkButton)(sender);
            string Link = btn.CommandArgument;
            
            Response.Write("<script type='text/javascript'>window.open('" + Link + ".aspx');</script>");
                   
        }


        protected void LogoutButton_Click(object sender, EventArgs e)
        {
            Session["Authenticate"] = false;
            Session["Customer"] = "";
            Session["CustomerID"] = "";

            Response.Redirect("Default.aspx");
        }

               
        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
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

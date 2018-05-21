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
    public partial class _Default_Receipt : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        private Int32 CustomerID;
        private Int32 CustomerRecord;
        
        private string SessionID;
        


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
            SessionID = Request.QueryString["Session"];

                    
            if ((Session["Authenticate"] != null) && (Convert.ToBoolean(Session["Authenticate"]) == true))
            {
                string sqlstring;               
                CustomerID = Int32.Parse(Session["CustomerID"].ToString());
                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                sqlstring = "SELECT CustomerID FROM Session WHERE SessionID = @Session";
                // create a connection with sqldatabase 
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                // create a sql command which will user connection string and your select statement string
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
                System.Data.SqlClient.SqlDataReader reader;
                // open a connection with sqldatabase
                con.Open();

                // execute sql command and store a return values in reade
                comm.Parameters.AddWithValue("@Session", SessionID);
                reader = comm.ExecuteReader();

                while (reader.Read())
                {

                    CustomerRecord = Convert.ToInt32(reader["CustomerID"].ToString());

                }
                con.Close();

                if (CustomerRecord == CustomerID)
                {
                
                    MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
                    MonseesSqlDataSource.SelectCommand = @"--Use monsees2 
															declare @true bit declare @false bit SET @true = 1 SET @false = 0 Select LineItem, POItemID, PartNumber, [Revision Number] AS ActiveVersion, DrawingNumber, UnitPriced, Quantity, NextQuantity, NextDelivery From POItems WHERE SessionID = " + SessionID;

                    sqlstring = "SELECT dbo.[PO Item].SessionID, dbo.[Purchase Order].PONumber, dbo.[Purchase Order].PODate, dbo.CustomerDB.CompanyName FROM dbo.CustomerDB RIGHT OUTER JOIN dbo.[Purchase Order] ON dbo.CustomerDB.CustomerID = dbo.[Purchase Order].CompanyID RIGHT OUTER JOIN dbo.[PO Item] ON dbo.[Purchase Order].POID = dbo.[PO Item].POID WHERE SessionID = @Session GROUP BY dbo.[PO Item].SessionID, dbo.[Purchase Order].PONumber, dbo.[Purchase Order].PODate, dbo.CustomerDB.CompanyName";
                    
                    // create a sql command which will user connection string and your select statement string
                    comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                    // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
                
                    // open a connection with sqldatabase
                    con.Open();

                    // execute sql command and store a return values in reade
                    comm.Parameters.AddWithValue("@Session", SessionID);
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                    
                        PONumber.Text = "PO Number : " + reader["PONumber"].ToString();
                        ContactName.Text = "Company : " + reader["CompanyName"].ToString();
                        PODate.Text = "PO Date : " + reader["PODate"].ToString();
                       

                    }
                    con.Close();
                }
                    
                else
                {
                    Response.Redirect("Forbidden.htm");
                }
            }
            else
            {
                Response.Redirect("Default.aspx");
            }
            
            
        }



       

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }
    }
}
 

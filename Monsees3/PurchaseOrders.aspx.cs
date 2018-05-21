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
    public partial class _PurchaseOrders : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        private string POID;
        private Int32 ContactID;
        private Int32 CompanyID;
        public string CompanyName;
       

        protected void Page_Load(object sender, EventArgs e)
        {

            if ((Session["Authenticate"] != null) && (Convert.ToBoolean(Session["Authenticate"]) == true))
            {
                ContactID = Int32.Parse(Session["CustomerID"].ToString());
                CompanyID = Convert.ToInt32(GetCompanyID(ContactID));
                CompanyName = Session["Customer"].ToString();

                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                MonseesSqlDataSourcePurchaseOrders.ConnectionString = MonseesConnectionString;
                MonseesSqlDataSourcePurchaseOrders.SelectCommand = @"--Use monsees2 
																	declare @true bit declare @false bit SET @true = 1 SET @false = 0 Select * From PurchaseOrders WHERE IsOpen = 1 And CompanyID = " + CompanyID;
                
            }
            else
            {
                Response.Redirect("Default.aspx");
            }

                Last_Refreshed.Text = "Last Refreshed : " + DateTime.Now;
           
        }

        protected void PurchaseOrderGrid_SelectedIndexChanged(object sender, EventArgs e)
        {
           
 
        }

        

        protected void PurchaseOrderGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
          
            
            

            string command_name = e.CommandName;

            if ((command_name == "Details"))
            {
                int index = Convert.ToInt32(e.CommandArgument);

                
                        gvRow = PurchaseOrderGrid.Rows[index];
                        POID = gvRow.Cells[0].Text;

                        Response.Write("<script type='text/javascript'>window.open('OrderDetails.aspx?POID=" + POID + "');</script>");
                        
                   
                      
                
            }
        }

      

       
        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }
      
     

       
        protected void LoggedInViewGrid_SelectedIndexChanged(object sender, EventArgs e)
        {

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

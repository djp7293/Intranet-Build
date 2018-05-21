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
    public partial class _Default_CustomerPO : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        private Int32 ContactID;
        private Int32 CompanyID;
        private Int32 CustomerRecord;
        private string POID;


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
                POID = Request.QueryString["POID"];

                    
           
                string sqlstring = "Select * FROM PurchaseOrders WHERE POID = " + POID;
               
                
                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                
                // create a connection with sqldatabase 
                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                // create a sql command which will user connection string and your select statement string
         
                
                    MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
                    MonseesSqlDataSource.SelectCommand = @"--Use monsees2 
														declare @true bit declare @false bit SET @true = 1 SET @false = 0 Select LineItem, PartNumber, [Revision Number], DrawingNumber, UnitPriced, Quantity, NextQuantity, CAST(dbo.POItems.NextDelivery AS Date) As NextDelivery, CAST(dbo.POItems.MaxOfCurrDelivery AS Date) As MaxOfCurrDelivery, Total, ShippedTotal, InvoicedTotal From POItems WHERE POID = " + POID;

                    sqlstring = "Select [POID], [PONumber], [CompanyName], [ContactName], [PODate], [MinOfCurrDelivery], [Total], [ShippedTotal], [InvoicedTotal], [CompanyID], OpenLinePO FROM [PurchaseOrders] WHERE [POID] = " + POID + ";";
                    
                    // create a sql command which will user connection string and your select statement string
                    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                    System.Data.SqlClient.SqlDataReader reader;
                    // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
                
                    // open a connection with sqldatabase
                    con.Open();

                    // execute sql command and store a return values in reade
                    reader = comm.ExecuteReader();

                    while (reader.Read())
                    {
                        CustomerName.Text = "Customer: " + reader["CompanyName"].ToString();
                        PONumber.Text = "PO Number : " + reader["PONumber"].ToString();
                        ContactName.Text = "Contact : " + reader["ContactName"].ToString();
                        PODate.Text = "PO Date : " + reader["PODate"].ToString();
                        NextDelivery.Text = "Next Delivery : " + reader["MinOfCurrDelivery"].ToString();

                        Total.Text = "Total : " + reader["Total"].ToString();
                        ShippedTotal.Text = "Shipped : " + reader["ShippedTotal"].ToString();
                        InvoiceTotal.Text = "Invoiced : " + reader["InvoicedTotal"].ToString();
                        if (reader["OpenLinePO"].ToString() == "*")
                        {

                            
                        }
                        else
                        {
                            AddLines.Visible = false;
                        }
                    }
                    con.Close();
                
                    
       
            
            Last_Refreshed.Text = "Last Refreshed : " + DateTime.Now;
        }


        protected void AddLines_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddtoPO.aspx?CID=" + ContactID + "&POID=" + POID);
        }
       

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }

        
    }
}
 

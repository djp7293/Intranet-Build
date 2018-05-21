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
using Monsees.Security;

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
    public partial class _Default_Employee : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        private string EmployeeID;
        private Int32 index;
        
       

        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
            
                string sqlstring = "Select [Name] from [Employees] where [EmployeeID] = " + Request.QueryString["Employee"];
                string Employee = "";
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
                while (reader.Read())
                {
                    Employee = reader["Name"].ToString();
                }
                con.Close();

                UserNameLabel.Text = Employee;

                MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
                EmployeeID = Request.QueryString["Employee"];
                MonseesSqlDataSource.SelectCommand = "Select * from EmployeeHistory where (EmployeeID =" + EmployeeID + ")";
                
               
                
          
           
            Last_Refreshed.Text = "Last Refreshed : " + DateTime.Now;
           
        }

        
       

        protected void ProductionViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            GridView gv;
            string LotID;
            

            string command_name = e.CommandName;

            if ((command_name == "GetFile") || (command_name == "Deliveries")||(command_name == "PartHistory")||(command_name == "Inspection")||(command_name == "Fixturing"))
            {
                Int32 totrows = ProductionViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;
                    //TO DO: Check to see if the user is already logged into the given job
               
                switch (e.CommandName)
                {
                    
                    case "PartHistory":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        string DetailID = "1";

                        string sqlstring = "Select [DetailID] from [Job Item] where [JobItemID] = " + LotID;

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
                            DetailID = reader["DetailID"].ToString();
                        }
                        con.Close();

                        Response.Write("<script type='text/javascript'>window.open('PartHistory.aspx?DetailID=" + DetailID + "','_blank');</script>");

                        break;


                    case "Inspection":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in
                        string pageName = (Page.IsInMappedRole("Inspection")) ? "InspectionReport.aspx" : "InspectionReportPrint.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobItemID=" + LotID + "');</script>");
                        break;

                    case "Fixturing":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in                        
                        Response.Write("<script type='text/javascript'>window.open('Fixturing.aspx?JobItemID=" + LotID + "');</script>");
                        break;


                    case "Deliveries":
                        gvRow = ProductionViewGrid.Rows[index];
                        gv = (GridView)gvRow.FindControl("DeliveryViewGrid");
                        try
                        {
                            gv = (GridView)gvRow.FindControl("DeliveryViewGrid");
                        }
                        catch
                        {
                            MessageBox("Failed");
                        }
                        LotID = gvRow.Cells[2].Text;
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                        MonseesSqlDataSourceDeliveries.ConnectionString = MonseesConnectionString;
                        MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped] FROM [FormDeliveries] WHERE JobItemID=" + LotID;
                        gv.DataSource = MonseesSqlDataSourceDeliveries;
                        gv.DataBind();
                       
                        break;
                    
                    case "GetFile":                        
                        String PartNumber;
                        String RevNumber;
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;                       
                        PartNumber = clickedRow.Cells[4].Text;
                        RevNumber = clickedRow.Cells[5].Text;
                        Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);
                        break;
                    default:
                        
                        break;

                }
            }
        }

        

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }
      
        

        
        

        

       
    }
}

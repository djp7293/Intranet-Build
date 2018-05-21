using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Services;
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
    public partial class _Default_FixtureTag : System.Web.UI.Page
    {
        private string MonseesConnectionString;


        private string JobItemID;


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
            JobItemID = Request.QueryString["id"];
            



            if (!IsPostBack)
            {

                string sqlstring;
                

                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
               //MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
                //MonseesSqlDataSource.SelectCommand = "--Use monsees2 declare @true bit declare @false bit SET @true = 1 SET @false = 0 Select * From InspectionReport WHERE JobItemID=" + JobItemID + " ORDER BY DimensionNumber";

                sqlstring = "Select JobItemID, CompanyName, SourceLot, PartNumber, Description, SourceLot, OperationName, Loc, FixtureMapID FROM FixtureInvSummary WHERE JobItemID=" + JobItemID + ";";
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
                        CompanyName.Text = reader["CompanyName"].ToString();                         
                        JobItem.Text = reader["JobItemID"].ToString();                       
                        PartNumber.Text = reader["PartNumber"].ToString();                       
                                               
                        DrawingNumber.Text = reader["Description"].ToString();                        
                        SourceLot.Text = reader["SourceLot"].ToString();                        
                        OperationName.Text = reader["OperationName"].ToString();                                               
                        Location1.Text = reader["Loc"].ToString();
                        InventoryID.Text = reader["FixtureMapID"].ToString();
                        
                    }
                    con.Close();
                
            }
            
        }



        

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }






    }

        
    }




 

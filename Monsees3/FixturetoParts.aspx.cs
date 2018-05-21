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
	public partial class FixturetoParts : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        
        private Int32 index;
        private string Revision;



        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MonseesSqlDataSource.ConnectionString = MonseesConnectionString;

            if (Request.QueryString["Id"] != null)
                Revision = Request.QueryString["Id"];

        }


       

        protected void RefreshOpsButton_Click(object sender, EventArgs e)
        {
            MonseesDB objMonseesDB;
            int result = 0;
            bool atLeastOneRowUpdated = false;
            // Iterate through the Products.Rows property
            AllocateFixture.Text = "INSERT INTO [FixtureMapping] (FixtureRevID, DetailUsingID) SELECT " + Revision + ", DetailID FROM Version WHERE";

           try 
           { 
            foreach (GridViewRow row in ProductionViewGrid.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("Allocate");
                
                if (cb != null && cb.Checked)
                {
                    int RevisionID;
                    // Delete row! (Well, not really...)
                    if (atLeastOneRowUpdated == true)
                    {
                        AllocateFixture.Text += " OR";
                    }
                    atLeastOneRowUpdated = true;
                    // First, get the ProductID for the selected row
                    RevisionID =
                        Convert.ToInt32(ProductionViewGrid.DataKeys[row.RowIndex].Value);
                    // "Delete" the row

                    AllocateFixture.Text += string.Format(
                        " RevisionID = {0}", RevisionID);
                }
            }
            // Show the Label if at least one row was deleted...
            objMonseesDB = new MonseesDB();
            result = objMonseesDB.ExecuteNonQuery(AllocateFixture.Text);
            MessageBox("The fixture was successfully allocated to the parts selected");
           }
           catch
           {
               MessageBox("There was an error when attempting to allocate this fixture to parts selected.");
           }
            
        }



        
        

        protected bool RefreshSetups(String SQLWrite)
        {
            MonseesDB objMonseesDB;
            bool return_result = false;
            int result = 0;

            MessageBox(SQLWrite);
            objMonseesDB = new MonseesDB();
            try
            {
                
                result = objMonseesDB.ExecuteNonQuery(SQLWrite);

            }
            catch (System.Exception ex)
            {
                return_result = false;
            }
            finally
            {
                return_result = true;
            }

            return return_result;
        }
        
        

        protected void ProductionViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;
            
            string command_name = e.CommandName;

            if ((command_name == "ViewOps") || (command_name == "GetFile")||(command_name == "PartHistory"))
            {
                Int32 totrows = ProductionViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job
                
                switch (e.CommandName)
                {
                    case "ViewOps":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('ViewOps.aspx?JobItemID=" + LotID + "','_blank');</script>");

                        break;
                  
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
 

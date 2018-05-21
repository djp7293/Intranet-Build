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
    public partial class InspectionRpt : System.Web.UI.Page
    {
        private string MonseesConnectionString;


        public string JobItemID;


        protected void Page_Load(object sender, EventArgs e)
        {
	
            // Check if the user is already logged in or not
            JobItemID = Request.QueryString["JobItemID"];
			//Page.RequireRole("Inspection", "/InspectionReportPrint.aspx?JobItemID=" + JobItemID);

            String Dim;
            TextBox msmnt1txt;
            TextBox msmnt2txt;
            TextBox msmnt3txt;
            TextBox finaltxt;
            TextBox remarktxt;



            if (!IsPostBack)
            {

                string sqlstring = "Select * FROM ProductionView WHERE JobItemID = " + JobItemID;
                //UpdateTableButton.Attributes.Add("onclick", "CallMe()");

                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
                MonseesSqlDataSource.SelectCommand = @"--Use monsees2 
														declare @true bit declare @false bit SET @true = 1 SET @false = 0 Select * From InspectionReport WHERE JobItemID=" + JobItemID + " ORDER BY DimensionNumber";

                sqlstring = "Select [JobItemID], [PartNumber], [Revision Number], [DrawingNumber], [CompanyName], [Quantity], [JobNumber], [Block Address] FROM InspectionReport WHERE JobItemID=" + JobItemID + " GROUP BY [JobItemID], [PartNumber], [Revision Number], [DrawingNumber], [CompanyName], [Quantity], [JobNumber], [Block Address]";
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
                if (!reader.HasRows)
                {
                    reader.Close();
                    System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("CreateInspection", con);
                    comm2.CommandType = CommandType.StoredProcedure;
                    comm2.Parameters.AddWithValue("@JobItemID", JobItemID);
                    comm2.ExecuteNonQuery();

                    reader = comm.ExecuteReader();
                    while (reader.Read())
                    {
                        JobItem.Text = "Lot # : " + reader["JobItemID"].ToString();
                        PartNumber.Text = "Part # : " + reader["PartNumber"].ToString();
                        RevisionNumber.Text = "Rev # : " + reader["Revision Number"].ToString();
                        DrawingNumber.Text = "Description : " + reader["DrawingNumber"].ToString();
                        CompanyName.Text = "Company Name : " + reader["CompanyName"].ToString();

                        qty.Text = "Quantity : " + reader["Quantity"].ToString();
                        JobNumber.Text = "Job # : " + reader["JobNumber"].ToString();
                    }
                    con.Close();
                }
                else
                {
                    while (reader.Read())
                    {
                        JobItem.Text = "Lot # : " + reader["JobItemID"].ToString();
                        PartNumber.Text = "Part # : " + reader["PartNumber"].ToString();
                        RevisionNumber.Text = "Rev # : " + reader["Revision Number"].ToString();
                        DrawingNumber.Text = "Description : " + reader["DrawingNumber"].ToString();
                        CompanyName.Text = "Company Name : " + reader["CompanyName"].ToString();

                        qty.Text = "Quantity : " + reader["Quantity"].ToString();
                        JobNumber.Text = "Job # : " + reader["JobNumber"].ToString();
                    }
                    con.Close();
                }
            }
        
        }



        protected void UpdateTableButton_Click(object sender, EventArgs e)
        {

            String Dim;
            TextBox msmnt1txt;
            TextBox msmnt2txt;
            TextBox msmnt3txt;
            TextBox finaltxt;
            TextBox remarktxt;




            foreach (GridViewRow row in ProductionViewGrid.Rows)
            {

                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
               

                msmnt1txt = (TextBox)ProductionViewGrid.Rows[row.RowIndex].FindControl("lblId1");
                msmnt2txt = (TextBox)ProductionViewGrid.Rows[row.RowIndex].FindControl("lblId2");
                msmnt3txt = (TextBox)ProductionViewGrid.Rows[row.RowIndex].FindControl("lblId3");
                finaltxt = (TextBox)ProductionViewGrid.Rows[row.RowIndex].FindControl("lblIdf");
                remarktxt = (TextBox)ProductionViewGrid.Rows[row.RowIndex].FindControl("lblIdr");

                Dim = row.Cells[0].Text;



                MonseesSqlDataSource.UpdateParameters["Measure1"].DefaultValue = msmnt1txt.Text;
                MonseesSqlDataSource.UpdateParameters["Measure2"].DefaultValue = msmnt2txt.Text;
                MonseesSqlDataSource.UpdateParameters["Measure3"].DefaultValue = msmnt3txt.Text;
                MonseesSqlDataSource.UpdateParameters["Final"].DefaultValue = finaltxt.Text;
                MonseesSqlDataSource.UpdateParameters["Remark"].DefaultValue = remarktxt.Text;
                MonseesSqlDataSource.UpdateParameters["ItemID"].DefaultValue = JobItemID;
                MonseesSqlDataSource.UpdateParameters["Dim"].DefaultValue = Dim;


                MonseesSqlDataSource.Update();



            }

            Response.Redirect("InspectionRpt.aspx?JobItemID=" + JobItemID);






        }

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }






    }

        
    }




 

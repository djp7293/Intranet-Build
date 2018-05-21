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
using Monsees.Database;
using Monsees.DataModel;
using Monsees.Pages;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;

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
    public partial class _Default_FixtureList : DataPage
    {
        private string MonseesConnectionString;
        
        private Int32 index;
        public string JobItemID;
       
        public JobDetailModel JobDetailModel { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
            

            JobItemID = Request.QueryString["SourceLot"];
            GetData();

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSource.SelectCommand = "SELECT * FROM [AllocateFixture]";
            


                
        }

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            JobDetailModel = inspectionRepository.GetJobDetailModelByJobItemId(Convert.ToInt32(JobItemID));
            

            this.UnitOfWork.End();
        }

        protected void AllocateViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string FixtureID;
            SqlBoolean Flag;
            string command_name = e.CommandName;

            if ((command_name == "Allocate") || (command_name == "GetFile"))
            {
                Int32 totrows = AllocateViewGrid.Rows.Count;
                int index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job
                
                switch (e.CommandName)
                {
                    
                    case "Allocate":
                        
                        gvRow = AllocateViewGrid.Rows[index];
                        GridView gv = (GridView)gvRow.FindControl("SetupViewGrid");
                        string FixtureInvID = gvRow.Cells[0].Text;
                        SqlConnection connection = new SqlConnection(MonseesConnectionString);
                        connection.Open();
                        if ((connection.State & ConnectionState.Open) > 0)
                        {
                            connection.Close();
                            try
                            {
                                if (FixtureInvID == null) 
                                { 
                                    FixtureID = gvRow.Cells[1].Text;
                                    Flag = false;
                                } 
                                else 
                                { 
                                    FixtureID = FixtureInvID;
                                    Flag = true;
                                }
                                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("AllocateFixturetoPart2", con);
                                cmd.Parameters.Clear();
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.AddWithValue("@DetailID", JobDetailModel.DetailID);
                                cmd.Parameters.AddWithValue("@FixtureID", Convert.ToInt32(FixtureInvID));
                                cmd.Parameters.AddWithValue("@Flag", Flag);
                                
                                con.Open();
                                cmd.ExecuteNonQuery();
                                Response.Redirect("Fixturing.aspx?JobItemID="+JobItemID);
                                con.Close();
                                
                            }
                            catch
                            {
                                Response.Write("Didn't Work." + JobDetailModel.DetailID + " 1 " + FixtureInvID + " 2 " + gvRow.Cells[0].Text + " 3 " + index);
                            }
                        }
                        else
                        {
                            Response.Write("No network connection!");
                        }
                        break;

                    case "GetFile":
                        String PartNumber;
                        String RevNumber;
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                        PartNumber = clickedRow.Cells[2].Text;
                        RevNumber = "NA";
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
 

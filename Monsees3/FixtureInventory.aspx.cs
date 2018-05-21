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
    public partial class _Default_FixtureInventory : DataPage
    {
        private string MonseesConnectionString;
        
        private Int32 index;
        public string JobItemID;
        public Int32 DetailID { get; set; }
        public JobDetailModel JobDetailModel { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
            

            
         

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

        protected void FixtureInventoryGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            
            string command_name = e.CommandName;

            if (command_name == "GetFile")
            {
                Int32 totrows = FixtureInventoryGrid.Rows.Count;
                int index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job
                
                switch (e.CommandName)
                {
                    
                    

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
 

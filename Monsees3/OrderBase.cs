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
    public partial class _Default_OurOrders : DataPage
    {
        private string MonseesConnectionString;
        
        private Int32 index;

        public string POID;

        private string potype;


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not

            POID = Request.QueryString["POID"];
            potype = Request.QueryString["type"];
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            HeaderData.ConnectionString = MonseesConnectionString;
            HeaderData.SelectCommand = "SELECT VendorName, dbo.SupplyOrders.SuppliesPONum AS PONumber, ContactName, dbo.SupplyOrders.Date AS IssueDate,DueDate, dbo.SupplyOrders.Cost AS Total, Description, dbo.SupplyOrders.Notes AS Note FROM dbo.SupplyOrders LEFT OUTER JOIN dbo.Contact ON dbo.SupplyOrders.Contact = dbo.Contact.ContactID LEFT OUTER JOIN dbo.Subcontractors ON dbo.SupplyOrders.VendorID = dbo.Subcontractors.SubcontractID WHERE dbo.SupplyOrders.SuppliesPONum = 3630";
            HeaderList.DataSource = HeaderData;
            HeaderList.DataBind();
            GetData();
            
            // create a sql command which will user connection string and your select statement string
            
            

        }


        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            
        }

		protected virtual void HandleViewPO()
		{

		}

        protected void ItemViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            
            string command_name = e.CommandName;

            if ((command_name == "ViewPO") || (command_name == "Attach")||(command_name == "Alloc"))
            {
				HandleViewPO();
            }

        }

        

        
        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }


        

      

       
    }
}
 

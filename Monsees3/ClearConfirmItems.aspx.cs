using System;
using System.IO;
using System.Web.Services;
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
using System.Collections.Generic;
using Monsees.Security;
using Monsees.DataModel;
using Monsees.Database;
using Dapper;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using Monsees.Data;
using Monsees.Pages;

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
    public partial class _Default_ClearConfirm : DataPage
    {

        protected Int32 index;
        protected string MonseesConnectionString;
        protected string[] EmployeeLoginName;
        protected string EmployeeName;
        protected Int32 EmployeeID;
        private List<OpenOperationLine> EditTracking = new List<OpenOperationLine>();
        public List<MaterialModel> MaterialList { get; set; }
        public List<DimensionModel> DimensionList { get; set; }
        public List<MaterialSizeModel> SizeList { get; set; }
        public List<WorkcodeModel> WCList { get; set; }
        public List<VendorListModel> VendorList { get; set; }
        public List<ViewJobItem> JobItemData { get; set; }
        public List<OperationListModel> OperationList { get; set; }
        ViewJobItem SpecificData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

           
            Last_Refreshed.Text = "Last Refreshed : " + DateTime.Now;


            EmployeeLoginName = User.Identity.Name.Split('\\');

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string sqlstring = "Select [EmployeeID], [Name] FROM [Employees] WHERE [WindowsAuthLogin] = 'jspurling';";
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

                EmployeeID = Convert.ToInt32(reader["EmployeeID"].ToString());
                EmployeeName = reader["Name"].ToString();


            }
            con.Close();

            GetData();
        }

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            MaterialList = inspectionRepository.GetMaterials();
            DimensionList = inspectionRepository.GetDimensions();
            SizeList = inspectionRepository.GetMaterialSizes();
            WCList = inspectionRepository.GetWorkcodes();
            VendorList = inspectionRepository.GetVendors();
            OperationList = inspectionRepository.GetOperations();

            this.UnitOfWork.End();
        }

        [WebMethod]
        protected void ExpandCollapse(object sender, EventArgs e)
        {
            GridViewRow ProdGrid = (GridViewRow)((Button)sender).Parent.Parent;
            GridView Prod = (GridView)((Button)sender).Parent.Parent.Parent.Parent;
            if (ProdGrid.RowType == DataControlRowType.DataRow)
            {
                index = ProdGrid.RowIndex;
                string JobItemID = ProductionViewGrid.DataKeys[index].Values[0].ToString();
               
                
                string DetailID = "0";
                string RevisionID = "0";

                ListView ListView2 = ProdGrid.FindControl("ListView2") as ListView;
                GridView DeliveryViewGrid = ProdGrid.FindControl("DeliveryViewGrid") as GridView;
                GridView GridView2 = ProdGrid.FindControl("GridView2") as GridView;
                GridView GridView3 = ProdGrid.FindControl("GridView3") as GridView;
                GridView GridView4 = ProdGrid.FindControl("GridView4") as GridView;
                GridView GridView5 = ProdGrid.FindControl("GridView5") as GridView;
                GridView GridView6 = ProdGrid.FindControl("GridView6") as GridView;
                GridView GridView7 = ProdGrid.FindControl("GridView7") as GridView;
                GridView GridView8 = ProdGrid.FindControl("GridView8") as GridView;
                GridView GridView9 = ProdGrid.FindControl("GridView9") as GridView;
                GridView CARView = ProdGrid.FindControl("CARView") as GridView;

                MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

                DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
                DeliveryViewGrid.DataBind();

                SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [Length], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID, HeatTreatID, PlatingID, SubcontractID, SubcontractID2, MaterialID, [Material Dimension], [Material Size], [Active Version], [ScrapRate], [MaterialSource], ProjectManager, Abbr FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

                ListView2.DataSource = SqlDataSource3;
                ListView2.DataBind();

                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [WorkcodeID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

                GridView2.DataSource = SqlDataSource4;
                GridView2.DataBind();

                SqlDataSource5.SelectCommand = "SELECT [SubcontractID], [WorkCode], [Quantity], [DueDate], CAST(CASE WHEN [HasDetail]=1 THEN 0 ELSE 1 END As Bit) As [Received] FROM [SubcontractItems] WHERE [JobItemID] = " + JobItemID;

                GridView3.DataSource = SqlDataSource5;
                GridView3.DataBind();

                SqlDataSource6.SelectCommand = "SELECT [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

                GridView4.DataSource = SqlDataSource6;
                GridView4.DataBind();

                SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], pct, [MatlPriceID], [MaterialPOID] FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

                GridView5.DataSource = SqlDataSource7;
                GridView5.DataBind();

                SqlDataSource8.SelectCommand = "SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName] FROM [FixtureOrders] WHERE [SourceLot] =" + JobItemID;

                GridView6.DataSource = SqlDataSource8;
                GridView6.DataBind();

                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                string sqlstring = "Select DetailID, [Active Version] FROM [Job Item] WHERE [JobItemID] = " + JobItemID + ";";
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
                    RevisionID = reader["Active Version"].ToString();

                }

                con.Close();

                SqlDataSource9.SelectCommand = "SELECT [PartNumber], [Description], [Loc], [Material] FROM [FixtureInvSummary] WHERE [DetailUsingID] = " + DetailID;

                GridView7.DataSource = SqlDataSource9;
                GridView7.DataBind();

                SqlDataSource10.SelectCommand = "SELECT [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [PerAssembly], [NextOp] FROM [AssemblyItemsSummary] WHERE [AssemblyLot] = " + JobItemID;

                GridView8.DataSource = SqlDataSource10;
                GridView8.DataBind();

                SqlDataSource11.SelectCommand = "SELECT [DrawingNumber], [PerAssy], [ItemNumber], [VendorName] FROM [BOMItemSummary] WHERE [AssyRevisionID] = " + RevisionID;

                GridView9.DataSource = SqlDataSource11;
                GridView9.DataBind();

                SqlDataSource12.SelectCommand = "SELECT * FROM CorrectiveActionView WHERE [DetailID] = " + DetailID;

                CARView.DataSource = SqlDataSource12;
                CARView.DataBind();

                HtmlGenericControl div = (HtmlGenericControl)ProdGrid.FindControl("div1");
                Button ExpCol = (Button)ProdGrid.FindControl("ExpColMain");


                if (div.Visible == false)
                {
                    div.Visible = true;
                    ExpCol.Text = "-";

                }
                else {
                    div.Visible = false;
                    ExpCol.Text = "+";
                }


            }

        }

        protected void ExpandCollapseIndependent(object sender)
        {
            GridViewRow ProdGrid = (GridViewRow)((Button)sender).Parent.Parent;
            GridView Prod = (GridView)((Button)sender).Parent.Parent.Parent.Parent;
            if (ProdGrid.RowType == DataControlRowType.DataRow)
            {
                index = ProdGrid.RowIndex;
                string JobItemID = ProductionViewGrid.DataKeys[index].Values[0].ToString();


                string DetailID = "0";
                string RevisionID = "0";

                ListView ListView2 = ProdGrid.FindControl("ListView2") as ListView;
                GridView DeliveryViewGrid = ProdGrid.FindControl("DeliveryViewGrid") as GridView;
                GridView GridView2 = ProdGrid.FindControl("GridView2") as GridView;
                GridView GridView3 = ProdGrid.FindControl("GridView3") as GridView;
                GridView GridView4 = ProdGrid.FindControl("GridView4") as GridView;
                GridView GridView5 = ProdGrid.FindControl("GridView5") as GridView;
                GridView GridView6 = ProdGrid.FindControl("GridView6") as GridView;
                GridView GridView7 = ProdGrid.FindControl("GridView7") as GridView;
                GridView GridView8 = ProdGrid.FindControl("GridView8") as GridView;
                GridView GridView9 = ProdGrid.FindControl("GridView9") as GridView;
                GridView CARView = ProdGrid.FindControl("CARView") as GridView;

                MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

                DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
                DeliveryViewGrid.DataBind();

                SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [Length], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID, HeatTreatID, PlatingID, SubcontractID, SubcontractID2, MaterialID, [Material Dimension], [Material Size], [Active Version], [ScrapRate], [MaterialSource], ProjectManager, Abbr FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

                ListView2.DataSource = SqlDataSource3;
                ListView2.DataBind();

                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [WorkcodeID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

                GridView2.DataSource = SqlDataSource4;
                GridView2.DataBind();

                SqlDataSource5.SelectCommand = "SELECT [SubcontractID], [WorkCode], [Quantity], [DueDate], CAST(CASE WHEN [HasDetail]=1 THEN 0 ELSE 1 END As Bit) As [Received] FROM [SubcontractItems] WHERE [JobItemID] = " + JobItemID;

                GridView3.DataSource = SqlDataSource5;
                GridView3.DataBind();

                SqlDataSource6.SelectCommand = "SELECT [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

                GridView4.DataSource = SqlDataSource6;
                GridView4.DataBind();

                SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], pct, [MatlPriceID], [MaterialPOID] FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

                GridView5.DataSource = SqlDataSource7;
                GridView5.DataBind();

                SqlDataSource8.SelectCommand = "SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName] FROM [FixtureOrders] WHERE [SourceLot] =" + JobItemID;

                GridView6.DataSource = SqlDataSource8;
                GridView6.DataBind();

                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                string sqlstring = "Select DetailID, [Active Version] FROM [Job Item] WHERE [JobItemID] = " + JobItemID + ";";
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
                    RevisionID = reader["Active Version"].ToString();

                }

                con.Close();

                SqlDataSource9.SelectCommand = "SELECT [PartNumber], [Description], [Loc], [Material] FROM [FixtureInvSummary] WHERE [DetailUsingID] = " + DetailID;

                GridView7.DataSource = SqlDataSource9;
                GridView7.DataBind();

                SqlDataSource10.SelectCommand = "SELECT [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [PerAssembly], [NextOp] FROM [AssemblyItemsSummary] WHERE [AssemblyLot] = " + JobItemID;

                GridView8.DataSource = SqlDataSource10;
                GridView8.DataBind();

                SqlDataSource11.SelectCommand = "SELECT [DrawingNumber], [PerAssy], [ItemNumber], [VendorName] FROM [BOMItemSummary] WHERE [AssyRevisionID] = " + RevisionID;

                GridView9.DataSource = SqlDataSource11;
                GridView9.DataBind();

                SqlDataSource12.SelectCommand = "SELECT * FROM CorrectiveActionView WHERE [DetailID] = " + DetailID;

                CARView.DataSource = SqlDataSource12;
                CARView.DataBind();

                HtmlGenericControl div = (HtmlGenericControl)ProdGrid.FindControl("div1");
                Button ExpCol = (Button)ProdGrid.FindControl("ExpColMain");


                if (div.Visible == false)
                {
                    div.Visible = true;
                    ExpCol.Text = "-";

                }
                else {
                    div.Visible = false;
                    ExpCol.Text = "+";
                }


            }

        }

        protected void ProductionViewGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#ffad5c");

                }


                if (((HiddenField)e.Row.FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }


                if (((HiddenField)e.Row.FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)e.Row.FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)e.Row.FindControl("Hot")).Value))
                    {
                        e.Row.Font.Bold = true;

                    }
                }
            }
        }

        protected void ProductionViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;

            string command_name = e.CommandName;

            if ((command_name == "Clear") || (command_name == "Lock") || (command_name == "PartHistory") || (command_name == "Unlock") || (command_name == "GetFile") || (command_name == "InitCAR"))
            {
                Int32 totrows = ProductionViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "Clear":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        
                        string sqlstring = "UPDATE [Job Item] SET [CompleteClear] = 1 WHERE [JobItemID] = " + LotID;

                        // create a connection with sqldatabase 
                        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        // create a sql command which will user connection string and your select statement string
                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);

                        // open a connection with sqldatabase
                        con.Open();

                        comm.ExecuteNonQuery();
                        con.Close();
                        ProductionViewGrid.DataBind();

                        break;

                    case "InitCAR":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in                        
                        Response.Write("<script type='text/javascript'>window.open('CARInitiate.aspx?id=" + LotID + "');</script>");
                        break;

                    case "Lock":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        
                        
                        // create a connection with sqldatabase 
                        System.Data.SqlClient.SqlConnection con2 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        // create a sql command which will user connection string and your select statement string
                        System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("LockPart", con2);
                        comm2.CommandType = CommandType.StoredProcedure;
                        comm2.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(LotID));
                        comm2.Parameters.AddWithValue("@flag", 1);
                        // open a connection with sqldatabase
                        con2.Open();

                        comm2.ExecuteNonQuery();
                        con2.Close();
                        ProductionViewGrid.DataBind();
                        break;

                    case "Unlock":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;


                        // create a connection with sqldatabase 
                        System.Data.SqlClient.SqlConnection con4 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        // create a sql command which will user connection string and your select statement string
                        System.Data.SqlClient.SqlCommand comm4 = new System.Data.SqlClient.SqlCommand("LockPart", con4);
                        comm4.CommandType = CommandType.StoredProcedure;
                        comm4.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(LotID));
                        comm4.Parameters.AddWithValue("@flag", 0);
                        // open a connection with sqldatabase
                        con4.Open();

                        comm4.ExecuteNonQuery();
                        con4.Close();
                        ProductionViewGrid.DataBind();
                        break;

                    case "PartHistory":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        string DetailID = "1";

                        string sqlstring2 = "Select [DetailID] from [Job Item] where [JobItemID] = " + LotID;

                        // create a connection with sqldatabase 
                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        // create a sql command which will user connection string and your select statement string
                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand(sqlstring2, con3);
                        // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
                        System.Data.SqlClient.SqlDataReader reader;
                        // open a connection with sqldatabase
                        con3.Open();

                        // execute sql command and store a return values in reade
                        reader = comm3.ExecuteReader();
                        while (reader.Read())
                        {
                            DetailID = reader["DetailID"].ToString();
                        }
                        con3.Close();

                        Response.Write("<script type='text/javascript'>window.open('PartHistory.aspx?DetailID=" + DetailID + "','_blank');</script>");

                        break;

                    case "GetFile":
                        String PartNumber;
                        String RevNumber;
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                        PartNumber = ((LinkButton)clickedRow.FindControl("lbpart")).Text;
                        RevNumber = clickedRow.Cells[5].Text;
                        Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);

                        break;

                    default:

                        break;

                }
            }
        }

        protected void UpdatedOperations_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID;
            string ProcessID;
            string sqlstring;


            Int32 totrows = UpdatedOperations.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument);
            //Get the value of column from the DataKeys using the RowIndex.
            LinkID = UpdatedOperations.DataKeys[index].Values[0].ToString();
            ProcessID = UpdatedOperations.DataKeys[index].Values[1].ToString();

            string command_name = e.CommandName;

            if ((command_name == "Clear") || (command_name == "Reverse") || (command_name == "ViewLot"))
            {
                
                switch (e.CommandName)
                {
                    case "Clear":
                        sqlstring = "UPDATE [JobSetup] SET [MovedOn]=0 WHERE [JobSetupID] = " + LinkID;
                         
                        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                        
                          
                        con.Open();

                        // execute sql command and store a return values in reade
                        comm.ExecuteNonQuery();
                        con.Close();
                        UpdatedOperations.DataBind();
                        break;
                        
                    case "Reverse":
                        sqlstring = "UPDATE [JobSetup] SET [Completed]=0, [MovedOn]=0 WHERE [JobSetupID] = " + LinkID;
                         
                        System.Data.SqlClient.SqlConnection con2 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(sqlstring, con2);
                        
                          
                        con2.Open();

                        // execute sql command and store a return values in reade
                        comm2.ExecuteNonQuery();
                        con2.Close();
                        UpdatedOperations.DataBind();
                        break;

                    case "Delete":
                        sqlstring = "UPDATE [JobSetup] SET [Completed]=0, [MovedOn]=0 WHERE [JobSetupID] = " + LinkID;

                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand(sqlstring, con3);


                        con3.Open();

                        // execute sql command and store a return values in reade
                        sqlstring = "UPDATE [JobSetup] SET [Completed]=0, [MovedOn]=0 WHERE [JobSetupID] = " + LinkID;
                        comm3.ExecuteNonQuery();
                        comm3.CommandText = "DELETE FROM [Process] WHERE [ProcessID] = " + ProcessID;
                        comm3.ExecuteNonQuery();
                        con3.Close();
                        UpdatedOperations.DataBind();
                        break;

                    case "ViewLot":
                        string pageName = "Lot.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?id=" + LinkID + "');</script>");
                        break;
                 
                    default:

                        break;

                }
            }
        }

        protected void NewJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
           
            string LinkID;

            Int32 totrows = NewJobs.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument) % totrows;
            //Get the value of column from the DataKeys using the RowIndex.
            LinkID = NewJobs.DataKeys[index].Values[0].ToString();

            string command_name = e.CommandName;

            if ((command_name == "ViewJob"))
            {
                
                switch (e.CommandName)
                {
                    case "ViewJob":
                        string pageName = (Page.IsInMappedRole("Office")) ? "JobSummaryEdit.aspx" : "JobSummary.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobID=" + LinkID + "');</script>");
                        break;

                    default:

                        break;

                }
            }
        }

        protected void WIPInventory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID;
            string sqlstring;


            Int32 totrows = WIPInventory.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument);
            //Get the value of column from the DataKeys using the RowIndex.
            LinkID = WIPInventory.DataKeys[index].Values[0].ToString();

            string command_name = e.CommandName;

            if ((command_name == "Accept") || (command_name == "Reject") || (command_name == "Modify"))
            {
                

                switch (e.CommandName)
                {
                    case "Accept":
                        GridViewRow gvRow = WIPInventory.Rows[index];
                        Int32 NewJobQty = Convert.ToInt32(gvRow.Cells[5].Text);
                        Int32 LotID = Convert.ToInt32(gvRow.Cells[0].Text);
                        Int32 AddedQty = Convert.ToInt32(gvRow.Cells[6].Text);
                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("ConfirmWIPInventory", con3);
                        comm3.CommandType = System.Data.CommandType.StoredProcedure;
                        comm3.Parameters.AddWithValue("@NewJobQty", NewJobQty);
                        comm3.Parameters.AddWithValue("@JobItemID", LotID);
                        comm3.Parameters.AddWithValue("@DeliveryItemID", Convert.ToInt32(LinkID));
                        comm3.Parameters.AddWithValue("@Quantity", AddedQty);


                        con3.Open();

                        // execute sql command and store a return values in reade
                        comm3.ExecuteNonQuery();
                        con3.Close();
                        WIPInventory.DataBind();
                        break;

                    case "Reject":
                        sqlstring = "DELETE FROM DeliveryItem WHERE [DeliveryItemID] = " + LinkID;
                         
                        System.Data.SqlClient.SqlConnection con4 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm4 = new System.Data.SqlClient.SqlCommand(sqlstring, con4);
                        
                          
                        con4.Open();

                        // execute sql command and store a return values in reade
                        comm4.ExecuteNonQuery();
                        con4.Close();
                        WIPInventory.DataBind();
                        break;

                    case "Modify":

                        break;

                    default:

                        break;

                }
            }
        }

        protected void Inventory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID;
            string sqlstring;


            Int32 totrows = Inventory.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument);
            //Get the value of column from the DataKeys using the RowIndex.
            LinkID = Inventory.DataKeys[index].Values[0].ToString();

            string command_name = e.CommandName;

            if ((command_name == "Accept") || (command_name == "Reject") || (command_name == "Modify"))
            {
               
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "Accept":
                        GridViewRow gvRow = Inventory.Rows[index];
                        Int32 InventoryQty = Convert.ToInt32(gvRow.Cells[5].Text);
                        Int32 LotID = Convert.ToInt32(gvRow.Cells[0].Text);
                        Int32 Qty = Convert.ToInt32(gvRow.Cells[1].Text);
                        string status = gvRow.Cells[6].Text;
                        
                        if (status == "Incomplete" || status == "Discrepant")
                        {
                            MessageBox("This inventory is either marked incomplete or discrepant.  Its lot will be reactivated and returned to the production schedule for completion or rework.");
                        }
                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("ConfirmInventoryPart1", con3);
                        comm3.CommandType = System.Data.CommandType.StoredProcedure;
                        comm3.Parameters.AddWithValue("@JobItemID", LotID);
                        comm3.Parameters.AddWithValue("@InventoryQty", InventoryQty);                        
                        comm3.Parameters.AddWithValue("@DeliveryItemID", LinkID);
                        comm3.Parameters.AddWithValue("@Quantity", Qty);

                        
                        System.Data.SqlClient.SqlCommand comm4 = new System.Data.SqlClient.SqlCommand("ConfirmInventoryPart2", con3);
                        comm4.CommandType = System.Data.CommandType.StoredProcedure;
                        comm4.Parameters.AddWithValue("@Question", 6);
                                                
                        comm4.Parameters.AddWithValue("@DeliveryItemID", LinkID);
                        comm4.Parameters.AddWithValue("@JobItemID", LotID);
                        comm4.Parameters.AddWithValue("@Status", status);


                        con3.Open();

                        // execute sql command and store a return values in reade
                        comm3.ExecuteNonQuery();
                        comm4.ExecuteNonQuery();
                        con3.Close();
                        
                        Response.Write("<script type='text/javascript'>window.open('/Reports/Label.aspx?id=" + LotID + "');</script>");
                        Inventory.DataBind();
                        break;

                    case "Reject":
                        sqlstring = "DELETE FROM DeliveryItem WHERE [DeliveryItemID] = " + LinkID;
                         
                        System.Data.SqlClient.SqlConnection con5 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm5 = new System.Data.SqlClient.SqlCommand(sqlstring, con5);
                        
                          
                        con5.Open();

                        // execute sql command and store a return values in reade
                        comm5.ExecuteNonQuery();
                        con5.Close();
                        Inventory.DataBind();
                        break;

                    case "Modify":

                        break;

                    default:

                        break;

                }
            }
        }

        protected void MaterialOrders_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID;
            string sqlstring;


            Int32 totrows = MaterialOrders.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument);
            //Get the value of column from the DataKeys using the RowIndex.
            LinkID = MaterialOrders.DataKeys[index].Values[0].ToString();

            string command_name = e.CommandName;


            if ((command_name == "Accept") || (command_name == "Reject") || (command_name == "Modify"))
            {
                
                
                switch (e.CommandName)
                {
                    case "Accept":
                        sqlstring = "UPDATE MaterialPO SET [ApprovalRecd]=1 WHERE [MaterialPOID] = " + LinkID;
                         
                        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                        
                          
                        con.Open();

                        // execute sql command and store a return values in reade
                        comm.ExecuteNonQuery();
                        con.Close();
                        UpdatedOperations.DataBind();
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

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID;
            string sqlstring;
            string SetupID = "0";

            Int32 totrows = Inventory.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument);
            //Get the value of column from the DataKeys using the RowIndex.
            LinkID = GridView1.DataKeys[index].Values[0].ToString();

            string command_name = e.CommandName;


            if ((command_name == "Authorize") || (command_name == "Reject") || (command_name == "Permanent"))
            {


                switch (e.CommandName)
                {
                    case "Authorize":
                        sqlstring = "UPDATE JobSetup SET [Authorized]=1, EmployeeAuthorized=" + EmployeeID + " WHERE [JobSetupID] = " + LinkID;

                        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);


                        con.Open();

                        // execute sql command and store a return values in reade
                        comm.ExecuteNonQuery();
                        con.Close();
                        GridView1.DataBind();
                        break;

                    case "Reject":
                        sqlstring = "DELETE FROM JobSetup WHERE [JobSetupID] = " + LinkID;
                         
                        System.Data.SqlClient.SqlConnection con2 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(sqlstring, con2);
                        
                          
                        con2.Open();

                        // execute sql command and store a return values in reade
                        comm2.ExecuteNonQuery();
                        con2.Close();
                        GridView1.DataBind();
                        break;

                    case "Permanent":
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                        sqlstring = "Select [SetupID], [WorkcodeID] FROM [JobSetup] WHERE [JobSetupID] = " + LinkID + ";";
                        // create a connection with sqldatabase 
                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        // create a sql command which will user connection string and your select statement string
                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand(sqlstring, con3);
                        // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
                        System.Data.SqlClient.SqlDataReader reader;
                        // open a connection with sqldatabase
                        con3.Open();

                        // execute sql command and store a return values in reade
                        reader = comm3.ExecuteReader();

                        while (reader.Read())
                        {

                            
                           SetupID = reader["SetupID"].ToString();


                        }
                        con3.Close();
                        con3.Open();
                        sqlstring = "UPDATE Setup SET Active=1 WHERE SetupID = " + SetupID;
                        System.Data.SqlClient.SqlCommand comm4 = new System.Data.SqlClient.SqlCommand(sqlstring, con3);
                        
                        comm4.ExecuteNonQuery();
                        sqlstring = "UPDATE JobSetup SET [Authorized]=1, EmployeeAuthorized=" + EmployeeID + " WHERE [JobSetupID] = " + LinkID;
                        comm4 = new System.Data.SqlClient.SqlCommand(sqlstring, con3);
                        comm4.ExecuteNonQuery();
                        con3.Close();
                        GridView1.DataBind();
                        break;

                    default:

                        break;

                }
            }
        }

        protected void MaterialOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView view = (GridView)sender;
            GridViewRow Row = e.Row;           
            GridView MaterialItems = (GridView)Row.FindControl("MaterialItems");

            if (Row.RowIndex > -1)
            {
                string MatPOID = Row.Cells[1].Text;
                MatPOLineItems.SelectCommand = "SELECT [MatPriceID], [cost], [MaterialName], [Dimension], [Size], [Length], [quantity], [VendorName], [MaterialPOID], [MaterialDimID], [MaterialSizeID], [MaterialID], [DueDate], [ItemNum], [Shipping], [ShippingCharge], [ConfirmationNum], [ContactName], [received], [JobNumber], [MinOfMatlCertReqd] FROM [MaterialOrders2] WHERE MaterialPOID = " + MatPOID;
                MaterialItems.DataSource = MatPOLineItems;
                MaterialItems.DataBind();
            }
        }



        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
            string JobSetupID = "0";
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            GridView LogHoursGrid = (GridView)Row.FindControl("LogHoursGridGV2");
            TextBox SetupHrs = (TextBox)Row.FindControl("SetupBox");
            TextBox RuntimeMins = (TextBox)Row.FindControl("OperationBox");

            if (Row.RowIndex > -1)
            {
                dpl = (DropDownList)Row.FindControl("ProcDescList");
                if (dpl != null)
                {
                    dpl.DataSource = OperationList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdProcIDList")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                    if (val == "")
                    {
                        dpl.Visible = false;
                        SetupHrs.Visible = false;
                        RuntimeMins.Visible = false;
                    }
                }
                dpl = (DropDownList)Row.FindControl("WCDescList");
                if (dpl != null)
                {
                    dpl.DataSource = WorkcodeList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdWCIDList")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                    if (val == "")
                    {
                        dpl.Visible = false;
                        SetupHrs.Visible = true;
                        RuntimeMins.Visible = true;
                    }
                }

                JobSetupID = Grid.DataKeys[Row.RowIndex].Values[0].ToString();
                LogHoursGridSource.SelectCommand = "SELECT ProcessID, JobSetupID, Name, Hours, QuantityIn, QuantityOut, EmployeeID FROM LoggedHoursSummary WHERE JobSetupID = " + JobSetupID;
                LogHoursGrid.DataSource = LogHoursGridSource;
                LogHoursGrid.DataBind();
            }

        }

        protected void LogHoursGridGV2_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpandedLogGV2(gvwChild, sender);

        }

        protected void LogHoursGridGV2_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;

            double HoursValue;

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            TextBox Hours = (TextBox)gvrow.FindControl("HoursGV2");

            TextBox QtyIn = (TextBox)gvrow.FindControl("QtyInGV2");
            TextBox QtyOut = (TextBox)gvrow.FindControl("QtyOutGV2");
            DropDownList EmplID = (DropDownList)gvrow.FindControl("EmplGV2");
            CheckBox Checked = (CheckBox)gvrow.FindControl("MoveOnGV2");


            gvwChild.EditIndex = -1;

            KeepExpandedLogGV2(gvwChild, sender);


            if (Hours.Text.Trim() != "")
            {
                HoursValue = Convert.ToDouble(Hours.Text);
            }
            else
            {
                HoursValue = 0;
            };

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("EditProcess", con);
            comm2.CommandType = CommandType.StoredProcedure;
            comm2.Parameters.AddWithValue("@QuantityIn", Convert.ToInt32(QtyIn.Text));
            comm2.Parameters.AddWithValue("@QuantityOut", Convert.ToInt32(QtyOut.Text));
            comm2.Parameters.AddWithValue("@Hours", Convert.ToInt32(Hours.Text));
            comm2.Parameters.AddWithValue("@Logout", DateTime.Now);
            comm2.Parameters.AddWithValue("@JobItemID", 0);
            comm2.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(EmplID.SelectedValue));
            comm2.Parameters.AddWithValue("@JobSetupID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Values[0].ToString()));
            comm2.Parameters.AddWithValue("@ProgramNum", "");
            comm2.Parameters.AddWithValue("@CheckMoveOn", Convert.ToBoolean(Checked.Checked));
            comm2.Parameters.AddWithValue("@ProcessID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Values[1].ToString()));

            try
            {

                result = comm2.ExecuteNonQuery();


            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                con.Close();
                ProductionViewGrid.DataBind();
                gvwChild.DataBind();
            }



        }

        protected void LogHoursGridGV2_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = e.NewEditIndex;

            KeepExpandedLogGV2(gvwChild, sender);



        }

        protected void LogHoursGridGV2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;

            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;

            if (i > -1)
            {

                dpl = (DropDownList)Row.FindControl("EmplGV2");
                if (dpl != null)
                {
                    dpl.DataSource = EmployeeList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdEmplGV2")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }
            }

        }

        protected void AddOpGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            DropDownList dpl = (DropDownList)gvRowParent.FindControl("DropDownList2GV2");
            if (dpl != null)
            {
                dpl.DataSource = OperationList;
                dpl.DataBind();

            }
            dpl = (DropDownList)gvRowParent.FindControl("EmployeeAddListGV2");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();

            }

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }


        protected void CancelAddOpGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void AddNowOpGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            string JobItemID = gvRowParent.Cells[2].Text;
            DropDownList Operation = (DropDownList)gvRowParent.FindControl("DropDownList2GV2");
            TextBox SetupCostBox = (TextBox)gvRowParent.FindControl("TextBox3GV2");
            TextBox OperationCostBox = (TextBox)gvRowParent.FindControl("TextBox4GV2");
            TextBox OpCommentBox = (TextBox)gvRowParent.FindControl("OpCommentBoxGV2");
            DropDownList EmployeeAddList = (DropDownList)gvRowParent.FindControl("EmployeeAddListGV2");
            TextBox OrderBox = (TextBox)gvRowParent.FindControl("RequestedOrderBox");
            string OperationID = Operation.SelectedValue.ToString();
            string SetupCost = SetupCostBox.Text;
            string OperationCost = OperationCostBox.Text;
            string description = OpCommentBox.Text;
            string createemployee = EmployeeAddList.SelectedValue.ToString();
            string order = OrderBox.Text;


            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand("AddOperationtoLot", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@jobitemID", Convert.ToInt32(JobItemID));
            com.Parameters.AddWithValue("@operationID", Convert.ToInt32(OperationID));
            com.Parameters.AddWithValue("@setupcost", SetupCost);
            com.Parameters.AddWithValue("@operationcost", OperationCost);
            com.Parameters.AddWithValue("@description", description);
            com.Parameters.AddWithValue("employee", createemployee);
            com.Parameters.AddWithValue("@ProcessOrder", Convert.ToInt32(order));
            con.Open();
            com.ExecuteNonQuery();


            con.Close();

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void AddSubGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            DropDownList dpl = (DropDownList)gvRowParent.FindControl("DropDownList3GV2");
            if (dpl != null)
            {
                dpl.DataSource = WorkcodeList;
                dpl.DataBind();

            }
            dpl = (DropDownList)gvRowParent.FindControl("EmployeeAddList2GV2");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();

            }

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }


        protected void CancelAddSubGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void AddNowSubGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            string JobItemID = gvRowParent.Cells[2].Text;
            DropDownList Workcode = (DropDownList)gvRowParent.FindControl("DropDownList3GV2");

            TextBox OpCommentBox = (TextBox)gvRowParent.FindControl("SubCommentBoxGV2");
            DropDownList EmployeeAddList = (DropDownList)gvRowParent.FindControl("EmployeeAddList2GV2");
            TextBox OrderBox = (TextBox)gvRowParent.FindControl("ReqOrderBoxSub");
            string OperationID = Workcode.SelectedValue.ToString();

            string description = OpCommentBox.Text;
            string createemployee = EmployeeAddList.SelectedValue.ToString();
            string order = OrderBox.Text;

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand("AddSubcontracttoLot", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@jobitemID", Convert.ToInt32(JobItemID));
            com.Parameters.AddWithValue("@workcodeID", Convert.ToInt32(OperationID));

            com.Parameters.AddWithValue("@description", description);
            com.Parameters.AddWithValue("employee", createemployee);
            com.Parameters.AddWithValue("@ProcessOrder", Convert.ToInt32(order));
            con.Open();
            com.ExecuteNonQuery();
            con.Close();

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void AddSub_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView10") as GridView;
            DropDownList dpl = (DropDownList)gvRowParent.FindControl("DropDownList3");
            if (dpl != null)
            {
                dpl.DataSource = WorkcodeList;
                dpl.DataBind();

            }
            dpl = (DropDownList)gvRowParent.FindControl("EmployeeAddList2");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();

            }

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }


        protected void CancelAddSub_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView10") as GridView;

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void AddNowSub_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView10") as GridView;
            string JobItemID = gvRowParent.Cells[2].Text;
            DropDownList Workcode = (DropDownList)gvRowParent.FindControl("DropDownList3");

            TextBox OpCommentBox = (TextBox)gvRowParent.FindControl("SubCommentBox");
            DropDownList EmployeeAddList = (DropDownList)gvRowParent.FindControl("EmployeeAddList2");
            TextBox OrderBox = (TextBox)gvRowParent.FindControl("ReqOrderBoxSub");
            string OperationID = Workcode.SelectedValue.ToString();

            string description = OpCommentBox.Text;
            string createemployee = EmployeeAddList.SelectedValue.ToString();
            string order = OrderBox.Text;

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand("AddSubcontracttoLot", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@jobitemID", Convert.ToInt32(JobItemID));
            com.Parameters.AddWithValue("@workcodeID", Convert.ToInt32(OperationID));

            com.Parameters.AddWithValue("@description", description);
            com.Parameters.AddWithValue("employee", createemployee);
            com.Parameters.AddWithValue("@ProcessOrder", Convert.ToInt32(order));
            con.Open();
            com.ExecuteNonQuery();
            con.Close();

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void LogNewNowGV2_Command(object sender, CommandEventArgs e)
        {
            string MonseesConnectionString;

            double HoursValue;
            Int32 QtyInValue;
            Int32 QtyOutValue;

            System.Web.UI.WebControls.GridViewRow gvwChild = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView gvChild = (GridView)gvwChild.Parent.Parent;
            GridView gvChildChild = (GridView)gvwChild.FindControl("LogHoursGridGV2");
            TextBox Hours = (TextBox)gvwChild.FindControl("HoursAddGV2");
            TextBox QtyIn = (TextBox)gvwChild.FindControl("QtyInAddGV2");
            TextBox QtyOut = (TextBox)gvwChild.FindControl("QtyOutAddGV2");
            DropDownList EmplID = (DropDownList)gvwChild.FindControl("EmployeeListGV2");
            CheckBox Checked = (CheckBox)gvwChild.FindControl("MoveOnGV2");


            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;

            if (QtyIn.Text.Trim() != "")
            {
                QtyInValue = Convert.ToInt32(QtyIn.Text);
            }
            else
            {
                QtyInValue = 0;
            };

            if (QtyOut.Text.Trim() != "")
            {
                QtyOutValue = Convert.ToInt32(QtyOut.Text);
            }
            else
            {
                QtyOutValue = 0;
            };

            if (Hours.Text.Trim() != "")
            {
                HoursValue = Convert.ToDouble(Hours.Text);
            }
            else
            {
                HoursValue = 0;
            };

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("MoveProcess", con);
            comm2.CommandType = CommandType.StoredProcedure;
            comm2.Parameters.AddWithValue("@QuantityIn", QtyInValue);
            comm2.Parameters.AddWithValue("@QuantityOut", QtyOutValue);
            comm2.Parameters.AddWithValue("@Hours", HoursValue);
            comm2.Parameters.AddWithValue("@Logout", DateTime.Now);
            comm2.Parameters.AddWithValue("@JobItemID", 0);
            comm2.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(EmplID.SelectedValue));
            comm2.Parameters.AddWithValue("@JobSetupID", Convert.ToInt32(gvChild.DataKeys[gvwChild.RowIndex].Value.ToString()));
            comm2.Parameters.AddWithValue("@ProgramNum", "");
            comm2.Parameters.AddWithValue("@CheckMoveOn", Convert.ToBoolean(Checked.Checked));
            try
            {

                result = comm2.ExecuteNonQuery();


            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                con.Close();
                string jobsetupid = gvChild.DataKeys[gvwChild.RowIndex].Values[0].ToString();
                string JobItemID = ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Values[0].ToString();
                LogHoursGridSource.SelectCommand = "SELECT ProcessID, JobSetupID, Name, Hours, QuantityIn, QuantityOut, EmployeeID FROM LoggedHoursSummary WHERE JobSetupID = " + jobsetupid;
                gvChildChild.DataSource = LogHoursGridSource;
                gvChildChild.DataBind();
                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [WorkcodeID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";
                gvChild.DataSource = SqlDataSource4;
                gvChild.DataBind();
            }
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvChild).Parent.Parent.Parent.Parent as GridView;
            //BindChildgvwChildLogGV2(gvChild.DataKeys[gvwChild.RowIndex].Values[0].ToString(), gvChildChild);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;

        }



        protected void KeepExpandedLogGV2(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvwChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridViewRow gvRowParentParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            BindChildgvwChildLogGV2(gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), gvwChild);
            index = gvRowParentParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            //ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        private void BindChildgvwChildLogGV2(string jobsetupId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobSetupID = jobsetupId;
            LogHoursGridSource.SelectCommand = "SELECT ProcessID, JobSetupID, Name, Hours, QuantityIn, QuantityOut, EmployeeID FROM LoggedHoursSummary WHERE JobSetupID = " + JobSetupID;

            gvChild.DataSource = LogHoursGridSource;
            gvChild.DataBind();
        }

        protected void LogHoursGridGV2_RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            string MonseesConnectionString;

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];

            gvwChild.EditIndex = -1;




            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            string UpdateQuery = "DELETE FROM Process WHERE ProcessID=@ProcessID";

            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(UpdateQuery, con);


            comm2.Parameters.AddWithValue("@ProcessID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Values[1].ToString()));

            try
            {

                result = comm2.ExecuteNonQuery();


            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                con.Close();

                KeepExpandedLogGV2(gvwChild, sender);
            }

        }


        protected void LogNewGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            GridViewRow LogHoursGrid = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            DropDownList dpl = (DropDownList)LogHoursGrid.FindControl("EmployeeListGV2");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();

            }
            //BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            //ExpandCollapseIndependent(button);
            div.Visible = true;

        }

        protected void CancelLogGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;

        }

        //This is where next set has been pasted
        protected void GridView2_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpanded(gvwChild, sender);

        }

        protected void GridView2_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;


            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            TextBox Setup = (TextBox)gvrow.FindControl("SetupBox");
            TextBox Operation = (TextBox)gvrow.FindControl("OperationBox");
            TextBox Comment = (TextBox)gvrow.FindControl("CommentBoxGV2");
            DropDownList OperationList = (DropDownList)gvrow.FindControl("ProcDescList");
            DropDownList WorkcodeList = (DropDownList)gvrow.FindControl("WCDescList");
            CheckBox Completed = (CheckBox)gvrow.FindControl("Completed");
            string JobSetupID = gvwChild.DataKeys[e.RowIndex].Values[0].ToString();
            gvwChild.EditIndex = -1;

            KeepExpanded(gvwChild, sender);

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();


            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();

            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("EditOperation", con);
            if (string.IsNullOrEmpty(((HiddenField)gvrow.FindControl("hdProcIDList")).Value.Trim()))
            {
                string updatequery = "UPDATE JobSetup SET WorkcodeID = @WorkcodeID, Comments=@Comments, Completed=@completed WHERE JobSetupID = @JobSetupID";
                comm2 = new System.Data.SqlClient.SqlCommand(updatequery, con);
                comm2.Parameters.AddWithValue("@WorkcodeID", Convert.ToInt32(WorkcodeList.SelectedValue.ToString()));
                comm2.Parameters.AddWithValue("@Comments", Comment.Text.ToString());
                comm2.Parameters.AddWithValue("@JobSetupID", JobSetupID);
                comm2.Parameters.AddWithValue("@completed", Completed.Checked);

            }
            else
            {
                comm2 = new System.Data.SqlClient.SqlCommand("EditOperation", con);
                comm2.CommandType = CommandType.StoredProcedure;
                comm2.Parameters.AddWithValue("@OperationID", Convert.ToInt32(OperationList.SelectedValue.ToString()));
                comm2.Parameters.AddWithValue("@Setup", Convert.ToInt32(Setup.Text));
                comm2.Parameters.AddWithValue("@Operation", Convert.ToInt32(Operation.Text));
                comm2.Parameters.AddWithValue("@JobSetupID", JobSetupID);
                comm2.Parameters.AddWithValue("@Comment", Comment.Text.ToString());
                comm2.Parameters.AddWithValue("@completed", Completed.Checked);
            }

            try
            {

                comm2.ExecuteNonQuery();


            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                con.Close();
                gvwChild.DataBind();
            }

        }


        protected void GridView2_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = e.NewEditIndex;

            KeepExpanded(gvwChild, sender);
        }

        protected void GridView2_RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            string MonseesConnectionString;

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];

            gvwChild.EditIndex = -1;

            KeepExpanded(gvwChild, sender);


            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            string UpdateQuery = "DELETE FROM JobSetup WHERE JobSetupID=@JobSetupID";

            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(UpdateQuery, con);


            comm2.Parameters.AddWithValue("@JobSetupID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Values[0].ToString()));

            try
            {

                result = comm2.ExecuteNonQuery();


            }
            catch (System.Exception ex)
            {

            }
            finally
            {
                con.Close();
                gvwChild.DataBind();
            }

        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView GridView2 = (GridView)sender;
            GridViewRow gvRow;
            string JobSetupID;
            string JobItemID;
            Int32 NewProcessOrder;
            Int32 totrows = GridView2.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument) % totrows;
            gvRow = GridView2.Rows[index];
            JobSetupID = GridView2.DataKeys[index].Values[0].ToString();
            JobItemID = GridView2.DataKeys[index].Values[1].ToString();
            string CurrProcessOrder = GridView2.DataKeys[index].Values[2].ToString();

            switch (e.CommandName)
            {
                case "Up":

                    NewProcessOrder = Convert.ToInt32(CurrProcessOrder) - 1;
                    if (NewProcessOrder == 0)
                    {
                        NewProcessOrder = 1;
                        string UpdateQuery = "UPDATE [JobSetup] SET ProcessOrder=@NewProcessOrder WHERE [JobSetupID] = @JobSetupID";
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                        con.Open();
                        SqlCommand com = new SqlCommand(UpdateQuery, con);
                        com.Parameters.AddWithValue("@NewProcessOrder", NewProcessOrder);
                        com.Parameters.AddWithValue("@JobSetupID", JobSetupID);
                        com.ExecuteNonQuery();
                        con.Close();
                        KeepExpanded(GridView2, sender);
                    }
                    else
                    {
                        string UpdateQuery = "UPDATE [JobSetup] SET ProcessOrder=@OldProcessOrder WHERE JobItemID=@JobItemID AND ProcessOrder=@NewProcessOrder";
                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                        SqlCommand com = new SqlCommand(UpdateQuery, con);
                        com.Parameters.AddWithValue("@NewProcessOrder", NewProcessOrder);
                        com.Parameters.AddWithValue("@OldProcessOrder", NewProcessOrder + 1);
                        com.Parameters.AddWithValue("@JobItemID", JobItemID);
                        con.Open();
                        com.ExecuteNonQuery();
                        UpdateQuery = "UPDATE [JobSetup] SET ProcessOrder=@NewProcessOrder WHERE [JobSetupID] = @JobSetupID";

                        com = new SqlCommand(UpdateQuery, con);
                        com.Parameters.AddWithValue("@NewProcessOrder", NewProcessOrder);
                        com.Parameters.AddWithValue("@JobSetupID", JobSetupID);
                        com.ExecuteNonQuery();


                        con.Close();
                        KeepExpanded(GridView2, sender);
                    }
                    break;
                case "Down":
                    NewProcessOrder = Convert.ToInt32(CurrProcessOrder) + 1;
                    SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    con2.Open();
                    string UpdateQueryDwn = "UPDATE [JobSetup] SET ProcessOrder=@OldProcessOrder WHERE JobItemID=@JobItemID AND ProcessOrder=@NewProcessOrder";
                    SqlCommand com2 = new SqlCommand(UpdateQueryDwn, con2);
                    com2.Parameters.AddWithValue("@NewProcessOrder", NewProcessOrder);
                    com2.Parameters.AddWithValue("@OldProcessOrder", NewProcessOrder - 1);
                    com2.Parameters.AddWithValue("@JobItemID", JobItemID);
                    com2.ExecuteNonQuery();
                    UpdateQueryDwn = "UPDATE [JobSetup] SET ProcessOrder=@NewProcessOrder WHERE [JobSetupID] = @JobSetupID";

                    com2 = new SqlCommand(UpdateQueryDwn, con2);
                    com2.Parameters.AddWithValue("@NewProcessOrder", NewProcessOrder);
                    com2.Parameters.AddWithValue("@JobSetupID", JobSetupID);
                    com2.ExecuteNonQuery();


                    con2.Close();
                    KeepExpanded(GridView2, sender);
                    break;
                default:
                    break;
            }
        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            GridView gv = (GridView)sender;
            string POID;

            string command_name = e.CommandName;

            if ((command_name == "ViewOrder"))
            {

                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "ViewOrder":
                        POID = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
                        //Check to see if user is already logged in                        
                        Response.Write("<script type='text/javascript'>window.open('MaterialPO.aspx?POID=" + POID + "');</script>");
                        break;
                    default:
                        break;

                }
            }
        }

        protected void GridView3_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            GridView gv = (GridView)sender;
            string POID;

            string command_name = e.CommandName;

            if ((command_name == "ViewOrder"))
            {

                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "ViewOrder":
                        POID = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
                        //Check to see if user is already logged in                        
                        Response.Write("<script type='text/javascript'>window.open('SubcontractPO.aspx?POID=" + POID + "');</script>");
                        break;
                    default:
                        break;

                }
            }
        }


        protected string PreventUnlistedValueError(DropDownList li, string val)
        {
            if (li.Items.FindByValue(val) == null)
            {
                //option 1: add the value to the list and display
                if (val == "") val = "0";
                ListItem lit = new ListItem();
                lit.Text = val;
                lit.Value = val;
                li.Items.Insert(li.Items.Count, lit);
                //option 2: set a default e.g.
                //val="";
            }
            return val;
        }

        protected void KeepExpanded(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent as GridViewRow;
            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        private void BindChildgvwChildView(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [WorkcodeID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";


            gvChild.DataSource = SqlDataSource4;
            gvChild.DataBind();
        }

        protected void ProductionViewGrid_PreRender(object sender, EventArgs e)
        {
            GridViewRow gvrow;
            int locked;
            int index2 = ProductionViewGrid.Rows.Count - 1;
            for (int i=1; i<=index2; i++)
            {
                gvrow = (GridViewRow)ProductionViewGrid.Rows[i];
                locked = Convert.ToInt32(((CheckBox)(gvrow.FindControl("lockflag"))).Checked);
                if (locked == 1)
                {
                    ((Button)gvrow.FindControl("lockbutton")).Visible = false;
                    ((Button)gvrow.FindControl("unlockbutton")).Visible = true;
                }
                else
                {
                    ((Button)gvrow.FindControl("unlockbutton")).Visible = false;
                    ((Button)gvrow.FindControl("lockbutton")).Visible = true;
                }
            
            }

        }


        protected void ListView3_ItemEditing(object sender, ListViewEditEventArgs e)
        {

            System.Web.UI.WebControls.ListView lvwChild = ((System.Web.UI.WebControls.ListView)sender);
            lvwChild.EditIndex = e.NewEditIndex;
            //GridViewRow gvRowParent = ((System.Web.UI.WebControls.ListView)sender).Parent.Parent as GridViewRow;
            //string JobItemID = NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString();
            //SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID, HeatTreatID, PlatingID, SubcontractID, SubcontractID2, MaterialID, [Material Dimension], [Material Size] FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

            //lvwChild.DataBind();
            //ListViewItem item = lvwChild.Items[e.NewEditIndex];
            //if (item != null)
            //{

            //}
            KeepExpandedList(lvwChild, sender);
            // Label HeatTreatLabel = (lvwChild.Items[e.NewEditIndex].FindControl("Heat_TreatLabel")) as Label;
            //   DropDownList HeatTreatDrop = (lvwChild.Items[e.NewEditIndex].FindControl("HeatTreatList")) as DropDownList;
            //  HeatTreatDrop.SelectedValue = HeatTreatLabel.Text;

            //  Label PlatingLabel = (lvwChild.Items[e.NewEditIndex].FindControl("PlatingLabel")) as Label;
            //  DropDownList PlatingDrop = (lvwChild.Items[e.NewEditIndex].FindControl("PlatingList")) as DropDownList;
            //  PlatingDrop.SelectedValue = PlatingLabel.Text;

            //  Label SubcontractLabel = (lvwChild.Items[e.NewEditIndex].FindControl("SubcontractLabel")) as Label;
            //  DropDownList SubcontractDrop = (lvwChild.Items[e.NewEditIndex].FindControl("SubcontractList")) as DropDownList;
            //  SubcontractDrop.SelectedValue = SubcontractLabel.Text;

            //  Label Subcontract2Label = (lvwChild.Items[e.NewEditIndex].FindControl("Subcontract2Label")) as Label;
            // DropDownList Subcontract2Drop = (lvwChild.Items[e.NewEditIndex].FindControl("Subcontract2List")) as DropDownList;
            //  Subcontract2Drop.SelectedValue = Subcontract2Label.Text;

            //lvwChild.DataSource = GetEmployee("Select * from Employee");
            //lvwChild.DataBind();
        }
        protected void ListView3_ItemUpdating(object sender, ListViewUpdateEventArgs e)
        {
            Int32 heattreat = 0;
            Int32 plating = 0;
            Int32 subcontract = 0;
            Int32 subcontract2 = 0;
            Int32 projectmanager = 0;
            Int32 material = 0;
            Int32 dimension = 0;
            Int32 size = 0;
            string revision = "";
            double length = 0;
            double stockcut = 0;
            Int32 partspercut = 0;
            Boolean purchasecut = false;
            Boolean drill = false;
            double drillsize = 0;

            Int32 matlsource = 0;
            string jobitemid;



            System.Web.UI.WebControls.ListView lvwChild = ((System.Web.UI.WebControls.ListView)sender);
            revision = lvwChild.DataKeys[e.ItemIndex].Values[1].ToString();
            jobitemid = lvwChild.DataKeys[e.ItemIndex].Values[0].ToString();
            DropDownList HeatTreatDrop = (lvwChild.Items[e.ItemIndex].FindControl("HeatTreatList")) as DropDownList;
            if (HeatTreatDrop != null)
                heattreat = string.IsNullOrEmpty(HeatTreatDrop.SelectedValue) ? 0 : Int32.Parse(HeatTreatDrop.SelectedValue);
            DropDownList PlatingDrop = (lvwChild.Items[e.ItemIndex].FindControl("PlatingList")) as DropDownList;
            if (PlatingDrop != null)
                plating = string.IsNullOrEmpty(PlatingDrop.SelectedValue.ToString()) ? 0 : Int32.Parse(PlatingDrop.SelectedValue.ToString());
            DropDownList SubcontractDrop = (lvwChild.Items[e.ItemIndex].FindControl("SubcontractList")) as DropDownList;
            if (SubcontractDrop != null)
                subcontract = string.IsNullOrEmpty(SubcontractDrop.SelectedValue) ? 0 : Int32.Parse(SubcontractDrop.SelectedValue);
            DropDownList Subcontract2Drop = (lvwChild.Items[e.ItemIndex].FindControl("Subcontract2List")) as DropDownList;
            if (Subcontract2Drop != null)
                subcontract2 = string.IsNullOrEmpty(Subcontract2Drop.SelectedValue) ? 0 : Int32.Parse(Subcontract2Drop.SelectedValue);
            DropDownList MaterialDrop = (lvwChild.Items[e.ItemIndex].FindControl("MaterialList")) as DropDownList;
            if (MaterialDrop != null)
                material = string.IsNullOrEmpty(MaterialDrop.SelectedValue) ? 0 : Int32.Parse(MaterialDrop.SelectedValue);
            DropDownList DimensionDrop = (lvwChild.Items[e.ItemIndex].FindControl("MaterialDimList")) as DropDownList;
            if (DimensionDrop != null)
                dimension = string.IsNullOrEmpty(DimensionDrop.SelectedValue) ? 0 : Int32.Parse(DimensionDrop.SelectedValue);
            DropDownList SizeDrop = (lvwChild.Items[e.ItemIndex].FindControl("MaterialSizeList")) as DropDownList;
            if (SizeDrop != null)
                size = Convert.ToInt32(SizeDrop.SelectedValue);
            DropDownList PMDrop = (lvwChild.Items[e.ItemIndex].FindControl("PMList")) as DropDownList;
            if (PMDrop != null)
                projectmanager = Convert.ToInt32(PMDrop.SelectedValue);
            TextBox LengthBox = (lvwChild.Items[e.ItemIndex].FindControl("LengthBox")) as TextBox;
            if (LengthBox != null)
                length = string.IsNullOrEmpty(LengthBox.Text) ? 0 : double.Parse(LengthBox.Text);

            TextBox StockCutBox = (lvwChild.Items[e.ItemIndex].FindControl("StockCutBox")) as TextBox;
            if (StockCutBox != null)
                stockcut = string.IsNullOrEmpty(StockCutBox.Text) ? 0 : double.Parse(StockCutBox.Text);
            TextBox DrillSizeBox = (lvwChild.Items[e.ItemIndex].FindControl("DrillSizeBox")) as TextBox;
            if (DrillSizeBox != null)
                drillsize = string.IsNullOrEmpty(DrillSizeBox.Text) ? 0 : double.Parse(DrillSizeBox.Text);
            TextBox PartsPerCutBox = (lvwChild.Items[e.ItemIndex].FindControl("PartsPerCutBox")) as TextBox;
            if (PartsPerCutBox != null)
                partspercut = string.IsNullOrEmpty(PartsPerCutBox.Text) ? 0 : int.Parse(PartsPerCutBox.Text);
            CheckBox PurchaseCutBox = (lvwChild.Items[e.ItemIndex].FindControl("PurchaseCutBox")) as CheckBox;
            purchasecut = PurchaseCutBox.Checked;
            CheckBox DrillBox = (lvwChild.Items[e.ItemIndex].FindControl("DrillBox")) as CheckBox;
            drill = DrillBox.Checked;
            DropDownList MatlSourceDrop = (lvwChild.Items[e.ItemIndex].FindControl("MatlSourceList")) as DropDownList;
            if (MatlSourceDrop != null)
                matlsource = string.IsNullOrEmpty(MatlSourceDrop.SelectedValue) ? 0 : Int32.Parse(MatlSourceDrop.SelectedValue);

            string UpdateQuery = "UPDATE [Version] SET [HeatTreatID] = @heattreat, [PlatingID] = @plating, [SubcontractID] = @subcontract, [SubcontractID2] = @subcontract2, [MaterialID] = @material, [Material Dimension] = @dimension, [Material Size] = @size, [Length per Part]=@length, StockCut=@stockcut, PartsPerCut=@partspercut, PurchaseCut=@purchasecut, Drill=@drill, DrillSize=@drillsize WHERE [RevisionID] = @revision";
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con.Open();
            SqlCommand com = new SqlCommand(UpdateQuery, con);
            com.Parameters.AddWithValue("@heattreat", heattreat);
            com.Parameters.AddWithValue("@plating", plating);
            com.Parameters.AddWithValue("@subcontract", subcontract);
            com.Parameters.AddWithValue("@subcontract2", subcontract2);

            com.Parameters.AddWithValue("@material", material);
            com.Parameters.AddWithValue("@dimension", dimension);
            com.Parameters.AddWithValue("@size", size);
            com.Parameters.AddWithValue("@length", length);
            com.Parameters.AddWithValue("@stockcut", stockcut);
            com.Parameters.AddWithValue("@partspercut", partspercut);
            com.Parameters.AddWithValue("@purchasecut", purchasecut);
            com.Parameters.AddWithValue("@drill", drill);
            com.Parameters.AddWithValue("@drillsize", drillsize);
            com.Parameters.AddWithValue("@revision", revision);
            com.ExecuteNonQuery();

            UpdateQuery = "UPDATE [Job Item] SET [MaterialSource]=@matlsource, [ProjectManager]=@projectmanager WHERE JobItemID=@jobitemid";
            com = new SqlCommand(UpdateQuery, con);
            com.Parameters.AddWithValue("@matlsource", matlsource);
            com.Parameters.AddWithValue("@jobitemid", jobitemid);
            com.Parameters.AddWithValue("@projectmanager", projectmanager);
            com.ExecuteNonQuery();

            con.Close();
            lvwChild.EditIndex = -1;
            KeepExpandedList(lvwChild, sender);
        }

        protected void ListView3_ItemCanceling(object sender, ListViewCancelEventArgs e)
        {
            System.Web.UI.WebControls.ListView lvwChild = ((System.Web.UI.WebControls.ListView)sender);
            lvwChild.EditIndex = -1;
            KeepExpandedList(lvwChild, sender);
        }

        protected void KeepExpandedList(System.Web.UI.WebControls.ListView lvwChild, object sender)
        {
            GridViewRow lvitemParent = ((System.Web.UI.WebControls.ListView)sender).Parent.Parent.Parent as GridViewRow;
            BindChildgvwChildViewList(ProductionViewGrid.DataKeys[lvitemParent.RowIndex].Value.ToString(), lvwChild);
            index = lvitemParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        private void BindChildgvwChildViewList(string jobitemId, System.Web.UI.WebControls.ListView lvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [Length], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID, HeatTreatID, PlatingID, SubcontractID, SubcontractID2, MaterialID, [Material Dimension], [Material Size], [Active Version], [ScrapRate], [MaterialSource], ProjectManager, Abbr FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

            lvChild.DataSource = SqlDataSource3;
            lvChild.DataBind();
        }

        protected void ListView3_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
            ListView List = (ListView)sender;
            DropDownList dpl;
            Int32 i = List.EditIndex;
            ListViewItem item = e.Item;
            if (i > -1)
            {

                dpl = (DropDownList)item.FindControl("HeatTreatList");

                if (dpl != null)
                {
                    dpl.DataSource = WCList;
                    dpl.DataBind();
                    string val = ((HiddenField)item.FindControl("hdHeatTreat")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)item.FindControl("PlatingList");
                if (dpl != null)
                {
                    dpl.DataSource = WCList;
                    dpl.DataBind();
                    string val = ((HiddenField)item.FindControl("hdPlating")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)item.FindControl("SubcontractList");
                if (dpl != null)
                {
                    dpl.DataSource = WCList;
                    dpl.DataBind();
                    string val = ((HiddenField)item.FindControl("hdSubcontract")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)item.FindControl("Subcontract2List");
                if (dpl != null)
                {
                    dpl.DataSource = WCList;
                    dpl.DataBind();
                    string val = ((HiddenField)item.FindControl("hdSubcontract2")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)item.FindControl("PMList");
                if (dpl != null)
                {
                    dpl.DataSource = EmployeeList;
                    dpl.DataBind();
                    string val = ((HiddenField)item.FindControl("hdPM")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)item.FindControl("MaterialList");
                if (dpl != null)
                {
                    dpl.DataSource = MaterialList;
                    dpl.DataBind();
                    string val = ((HiddenField)item.FindControl("hdMaterial")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)item.FindControl("MaterialDimList");
                if (dpl != null)
                {
                    dpl.DataSource = DimensionList;
                    dpl.DataBind();
                    string val = ((HiddenField)item.FindControl("hdDimension")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)item.FindControl("MaterialSizeList");
                if (dpl != null)
                {
                    dpl.DataSource = SizeList.FindAll(Size => Size.MaterialDimID.Equals(Convert.ToInt32(string.IsNullOrEmpty(((HiddenField)item.FindControl("hdDimension")).Value.Trim()) ? "0" : ((HiddenField)item.FindControl("hdDimension")).Value.Trim())));
                    dpl.DataBind();
                    string val = ((HiddenField)item.FindControl("hdSize")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }





            }
            else
            {
                dpl = (DropDownList)item.FindControl("VendorList");
                if (dpl != null)
                {
                    dpl.DataSource = VendorList;
                    dpl.DataBind();

                }
                string divlabel = "matlpurchase";

                Control divmatl = item.FindControl(divlabel);
                if (((Label)item.FindControl("MatlSourceLabel")).Text == "Purchased")
                {
                    divmatl.Visible = true;
                }
                else
                {
                    divmatl.Visible = false;
                }
            }

        }

        protected void ListView3_ItemCommand(object sender, ListViewCommandEventArgs e)
        {

            Int32 material = 0;
            Int32 dimension = 0;
            Int32 size = 0;
            string JobItemID;
            double stockcut = 0;
            Int32 partspercut = 0;
            Boolean purchasecut = false;
            double length = 0;
            double totlength = 0;
            Int32 quantity = 0;
            Int32 suggested = 0;
            Boolean approval = false;



            if (e.CommandName == "MaterialQuote")
            {

                ListView lvwChild = (ListView)sender;
                GridViewRow ParentRow = (GridViewRow)e.Item.Parent.Parent.Parent.Parent.Parent.Parent.Parent;

                UnitOfWork uw = new UnitOfWork();
                uw.Context.Open();
                Int32 totalrows = ProductionViewGrid.Rows.Count;
                Int32 indexval = 0;

                JobItemID = lvwChild.DataKeys[0].Values[0].ToString();
                for (int i = 0; i <= totalrows - 1; i++)
                {
                    if (ProductionViewGrid.DataKeys[i].Value.ToString() == JobItemID)
                    {
                        indexval = i;
                        break;
                    }
                }


                GridView lvwMatQuote = (GridView)ProductionViewGrid.Rows[indexval].FindControl("GridView12");

                JobItemData = uw.Context.Query<ViewJobItem>(@"SELECT MaterialID, [Material Dimension] As MaterialDimID, [Material Size] As MaterialSizeID, [Length] As LengthperPart, StockCut, PartsPerCut, PurchaseCut FROM ViewJobItem WHERE JobItemID = @jobitemid", new { jobitemid = JobItemID }).ToList();
                SpecificData = JobItemData.First();
                Int32 partquantitylbl = Convert.ToInt32(ProductionViewGrid.Rows[ParentRow.RowIndex].Cells[8].Text.Trim());
                Label scrapratelbl = e.Item.FindControl("ScrapLabel") as Label;



                material = SpecificData.MaterialID;
                dimension = SpecificData.MaterialDimID;
                size = SpecificData.MaterialSizeID;
                length = SpecificData.LengthperPart;
                stockcut = SpecificData.StockCut;
                partspercut = SpecificData.PartsPerCut;
                purchasecut = SpecificData.PurchaseCut;


                CheckBox ApprovalLabel = (e.Item.FindControl("Approval")) as CheckBox;
                approval = ApprovalLabel.Checked;

                if (purchasecut == false)
                {
                    quantity = 1;
                    totlength = Math.Ceiling(Math.Ceiling(Convert.ToInt32(partquantitylbl) * (1 + Convert.ToDouble(scrapratelbl.Text))) * length);
                }
                else
                {
                    if (stockcut == 0)
                    {
                        quantity = Convert.ToInt32(Math.Ceiling(Convert.ToInt32(partquantitylbl) * (1 + Convert.ToDouble(scrapratelbl.Text))));
                        totlength = length;
                    }
                    else
                    {
                        quantity = Convert.ToInt32(Math.Ceiling(Math.Ceiling(Convert.ToInt32(partquantitylbl) * (1 + Convert.ToDouble(scrapratelbl.Text))) / partspercut));
                        totlength = stockcut;
                    }
                }



                string UpdateQuery = "INSERT INTO MatQuoteItem (JobItemID, MaterialID, MatDimID, MatSizeID, Length, Quantity, Cut, SuggVendor, ReqdApproval) VALUES (@JobItemID, @material, @dimension, @size, @totlength, @quantity, @purchasecut, @suggested, @approvalreqd);";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                con.Open();
                SqlCommand com = new SqlCommand(UpdateQuery, con);

                com.Parameters.AddWithValue("@material", material);
                com.Parameters.AddWithValue("@dimension", dimension);
                com.Parameters.AddWithValue("@size", size);
                com.Parameters.AddWithValue("@stockcut", stockcut);

                com.Parameters.AddWithValue("@purchasecut", purchasecut);
                com.Parameters.AddWithValue("@JobItemID", JobItemID);
                com.Parameters.AddWithValue("@totlength", totlength);
                com.Parameters.AddWithValue("@quantity", quantity);
                com.Parameters.AddWithValue("@suggested", suggested);
                com.Parameters.AddWithValue("@approvalreqd", approval);

                com.ExecuteNonQuery();
                con.Close();

                SqlDataSource6.SelectCommand = "SELECT [MatQueueID], [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending], [SuggVendor], [ReqdApproval], [Size], [MaterialID], [MatDimID], [MatSizeID], [VendorName] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

                lvwMatQuote.DataSource = SqlDataSource6;
                lvwMatQuote.DataBind();

                KeepExpandedList(lvwChild, sender);

            }
        }

        protected void MaterialDimList_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListViewItem item = (ListViewItem)(((System.Web.UI.WebControls.DropDownList)sender).Parent.Parent);
            DropDownList SizeListDL = (DropDownList)item.FindControl("MaterialSizeList");

            if (SizeListDL != null)
            {
                string matdim = string.IsNullOrEmpty(((DropDownList)item.FindControl("MaterialDimList")).SelectedValue.Trim()) ? "0" : ((DropDownList)item.FindControl("MaterialDimList")).SelectedValue.Trim();
                SizeListDL.DataSource = SizeList.FindAll(Size => Size.MaterialDimID.Equals(Convert.ToInt32(matdim)));
                SizeListDL.DataBind();
                string val = ((HiddenField)item.FindControl("hdSize")).Value.Trim();
                SizeListDL.SelectedValue = PreventUnlistedValueError(SizeListDL, val);
                GridViewRow lvitemParent = (GridViewRow)item.Parent.Parent.Parent.Parent.Parent.Parent.Parent;

                if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + lvitemParent.Cells[2].Text + "').style.display = 'inline';", true);
                }

            }

        }

        protected void CARView_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            GridViewRow gvRow;
            GridView gv;
            string CARID;


            switch (e.CommandName)
            {
                case "ViewCAR":
                    gv = (GridView)sender;
                    gvRow = gv.Rows[Convert.ToInt32(e.CommandArgument)];
                    CARID = gvRow.Cells[0].Text;
                    //Check to see if user is already logged in
                    string pageName = (Page.IsInMappedRole("Inspection")) ? "CARComplete.aspx" : "CARComplete.aspx";
                    Response.Write("<script type='text/javascript'>window.open('" + pageName + "?id=" + CARID + "');</script>");
                    break;
                default:
                    break;
            }

            for (int i = 0; i < ProductionViewGrid.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)ProductionViewGrid.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    ProductionViewGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)ProductionViewGrid.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    ProductionViewGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)ProductionViewGrid.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    ProductionViewGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)ProductionViewGrid.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)ProductionViewGrid.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)ProductionViewGrid.Rows[i].FindControl("Hot")).Value))
                    {
                        ProductionViewGrid.Rows[i].Font.Bold = true;

                    }


                }


            }
        }

    }
}

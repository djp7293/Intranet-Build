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
	public partial class MonitorOps : DataPage
    {
        private string MonseesConnectionString;
        
        private Int32 index;
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
            // Check if the user is already logged in or not

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSourceSusp.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSourceNC.ConnectionString = MonseesConnectionString;

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

        protected void RefreshOpsButton_Click(object sender, EventArgs e)
        {
            MonseesDB objMonseesDB;
            int result = 0;
            bool atLeastOneRowUpdated = false;
            // Iterate through the Products.Rows property
            UpdateResults.Text = "UPDATE [JobSetup] SET [Completed] = 1 WHERE ";
            foreach (GridViewRow row in ProductionViewGrid.Rows)
            {
                // Access the CheckBox
                CheckBox cb = (CheckBox)row.FindControl("Update");
                
                if (cb != null && cb.Checked)
                {
                    int JobSetupID;
                    // Delete row! (Well, not really...)
                    if (atLeastOneRowUpdated == true)
                    {
                        UpdateResults.Text += " OR";
                    }
                    atLeastOneRowUpdated = true;
                    // First, get the ProductID for the selected row
                    JobSetupID =
                        Convert.ToInt32(ProductionViewGrid.DataKeys[row.RowIndex].Value);
                    // "Delete" the row

                    UpdateResults.Text += string.Format(
                        " JobSetupID = {0}", JobSetupID);
                }
            }
            // Show the Label if at least one row was deleted...
            objMonseesDB = new MonseesDB();
            result = objMonseesDB.ExecuteNonQuery(UpdateResults.Text);
            MessageBox(UpdateResults.Text);
            Response.Redirect("MonitorOps.aspx");
            
        }

        protected void ProductionViewGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                index = e.Row.RowIndex;
                string JobItemID = ProductionViewGrid.DataKeys[index].Values[0].ToString();
                string DetailID = "0";
                string RevisionID = "0";

                ListView ListView2 = e.Row.FindControl("ListView2") as ListView;
                GridView DeliveryViewGrid = e.Row.FindControl("DeliveryViewGrid") as GridView;
                GridView GridView2 = e.Row.FindControl("GridView2") as GridView;
                GridView GridView3 = e.Row.FindControl("GridView3") as GridView;
                GridView GridView4 = e.Row.FindControl("GridView4") as GridView;
                GridView GridView5 = e.Row.FindControl("GridView5") as GridView;
                GridView GridView6 = e.Row.FindControl("GridView6") as GridView;
                GridView GridView7 = e.Row.FindControl("GridView7") as GridView;
                GridView GridView8 = e.Row.FindControl("GridView8") as GridView;
                GridView GridView9 = e.Row.FindControl("GridView9") as GridView;

                MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

                DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
                DeliveryViewGrid.DataBind();

                SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

                ListView2.DataSource = SqlDataSource3;
                ListView2.DataBind();

                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

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

            }

        }

        protected void ClearLotsButton_Click(object sender, EventArgs e)
        {
            MonseesDB objMonseesDB;
            int result = 0;
            bool atLeastOneRowUpdated = false;
            // Iterate through the Products.Rows property
            UpdateResults.Text = "UPDATE [Job Item] SET [Clear] = 1 WHERE ";
            foreach (GridViewRow row in NotClearViewGrid.Rows)
            {
                // Access the CheckBox
                CheckBox cbc = (CheckBox)row.FindControl("Clear");

                if (cbc != null && cbc.Checked)
                {
                    int JobItemID;
                    // Delete row! (Well, not really...)
                    if (atLeastOneRowUpdated == true)
                    {
                        UpdateResults.Text += " OR";
                    }
                    atLeastOneRowUpdated = true;
                    // First, get the ProductID for the selected row
                    JobItemID =                        Convert.ToInt32(NotClearViewGrid.DataKeys[row.RowIndex].Value);
                    // "Delete" the row

                    UpdateResults.Text += string.Format(
                        " JobItemID = {0}", JobItemID);
                }
            }
            // Show the Label if at least one row was deleted...
            objMonseesDB = new MonseesDB();
            result = objMonseesDB.ExecuteNonQuery(UpdateResults.Text);
            MessageBox(UpdateResults.Text);
            Response.Redirect("MonitorOps.aspx");

        }

        protected void ActivateLotsButton_Click(object sender, EventArgs e)
        {
            MonseesDB objMonseesDB;
            int result = 0;
            bool atLeastOneRowUpdated = false;
            // Iterate through the Products.Rows property
            UpdateResults.Text = "UPDATE [DeliveryItem] SET [Suspend] = 0 WHERE RTS = 0 AND (";
            foreach (GridViewRow row in SuspendedViewGrid.Rows)
            {
                // Access the CheckBox
                CheckBox cbc = (CheckBox)row.FindControl("Activate");

                if (cbc != null && cbc.Checked)
                {
                    int JobItemID;
                    // Delete row! (Well, not really...)
                    if (atLeastOneRowUpdated == true)
                    {
                        UpdateResults.Text += " OR";
                    }
                    atLeastOneRowUpdated = true;
                    // First, get the ProductID for the selected row
                    JobItemID = Convert.ToInt32(SuspendedViewGrid.DataKeys[row.RowIndex].Value);
                    // "Delete" the row

                    UpdateResults.Text += string.Format(
                        " LotNumber = {0}", JobItemID);
                }
            }
            // Show the Label if at least one row was deleted...
            UpdateResults.Text += ")";
            objMonseesDB = new MonseesDB();
            result = objMonseesDB.ExecuteNonQuery(UpdateResults.Text);
            MessageBox(UpdateResults.Text);
            Response.Redirect("MonitorOps.aspx");

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

            if ((command_name == "ViewOps") || (command_name == "GetFile")||(command_name == "Deliveries")||(command_name == "PartHistory"))
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

        protected void SuspendedViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;

            string command_name = e.CommandName;

            if ((command_name == "ViewOps") || (command_name == "GetFile") || (command_name == "Deliveries") || (command_name == "PartHistory") || (command_name == "Activate"))
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
                    case "Deliveries":

                        gvRow = ProductionViewGrid.Rows[index];
                        GridView gv = (GridView)gvRow.FindControl("DeliveryViewGrid");
                        LotID = gvRow.Cells[2].Text;
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                        MonseesSqlDataSourceDeliveries.ConnectionString = MonseesConnectionString;
                        MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped] FROM [FormDeliveries] WHERE JobItemID=" + LotID;
                        gv.DataSource = MonseesSqlDataSourceDeliveries;
                        gv.DataBind();
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
                    case "Activate":
                        break;

                    default:

                        break;

                }
            }
        }

        protected void NotClearViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;
            
            System.Data.SqlClient.SqlConnection con;
            System.Data.SqlClient.SqlCommand comm2;
            string command_name = e.CommandName;

            if ((command_name == "ViewOps") || (command_name == "GetFile") || (command_name == "Deliveries") || (command_name == "PartHistory") || (command_name == "Clear") || (command_name == "Edit") || (command_name == "AddFixt") || (command_name == "AllocFixt"))
            {
                Int32 totrows = NotClearViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;
                string SqlStr;
                int result;
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "ViewOps":
                        gvRow = NotClearViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;

                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('ViewOps.aspx?JobItemID=" + LotID + "','_blank');</script>");

                        break;
                    case "Deliveries":

                        gvRow = NotClearViewGrid.Rows[index];
                        GridView gv = (GridView)gvRow.FindControl("DeliveryViewGrid");
                        LotID = gvRow.Cells[2].Text;
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                        MonseesSqlDataSourceDeliveries.ConnectionString = MonseesConnectionString;
                        MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped] FROM [FormDeliveries] WHERE JobItemID=" + LotID;
                        gv.DataSource = MonseesSqlDataSourceDeliveries;
                        gv.DataBind();
                        break;
                    case "PartHistory":
                        gvRow = NotClearViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        string DetailID = "1";

                        string sqlstring = "Select [DetailID] from [Job Item] where [JobItemID] = " + LotID;

                        // create a connection with sqldatabase 
                        con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
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

                    case "Edit":
                        NotClearViewGrid.EditIndex = index;
                        NotClearViewGrid.DataBind();
                        break;


                    case "AddFixt":
                        gvRow = NotClearViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        Response.Write("<script type='text/javascript'>window.open('AddFixture.aspx?SourceLot=" + LotID + "','_blank');</script>");
                        break;
                    case "AllocFixt":
                                                gvRow = NotClearViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        Response.Write("<script type='text/javascript'>window.open('AllocateFixture.aspx?SourceLot=" + LotID + "','_blank');</script>");
                        break;


                    case "Clear":
                        gvRow = NotClearViewGrid.Rows[index];
                        LotID = gvRow.Cells[3].Text;
                        
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            
                        con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con.Open();

                        DropDownList MaterialSrc = (DropDownList)ProductionViewGrid.Rows[index].FindControl("lblIdf");
                        
                        SqlStr = "UPDATE [Job Item] SET [Clear] = 1 WHERE JobItemID = @LotID;";
                        comm2 = new System.Data.SqlClient.SqlCommand(SqlStr, con);
                        comm2.CommandType = CommandType.Text;

                        
                        comm2.Parameters.AddWithValue("@LotID", LotID);
                        
                        result = comm2.ExecuteNonQuery();
                        NotClearViewGrid.DataBind();

                        
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

        protected void ProdViewButton_Click(object sender, EventArgs e)
        {

            int CurrentView = ProductionMultiView.ActiveViewIndex;

            if (CurrentView != 0)
            {
                ProductionViewGrid.DataBind();
                
                ProductionMultiView.SetActiveView(Active);

            }
            
        }

        protected void SuspViewButton_Click(object sender, EventArgs e)
        {

            int CurrentView = ProductionMultiView.ActiveViewIndex;

            if (CurrentView != 1)
            {
                SuspendedViewGrid.DataBind();

                ProductionMultiView.SetActiveView(Suspended);

            }

        }

        protected void NCViewButton_Click(object sender, EventArgs e)
        {

            int CurrentView = ProductionMultiView.ActiveViewIndex;

            if (CurrentView != 2)
            {
                NotClearViewGrid.DataBind();

                ProductionMultiView.SetActiveView(NotCleared);

            }

        }

        protected void NotClearViewGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                index = e.Row.RowIndex;
                string JobItemID = NotClearViewGrid.DataKeys[index].Values[0].ToString();
                string DetailID = "0";
                string RevisionID = "0";

                ListView ListView3 = e.Row.FindControl("ListView3") as ListView;
                GridView DeliveryViewGrid = e.Row.FindControl("DeliveryViewGridUC") as GridView;

                GridView GridView10 = e.Row.FindControl("GridView10") as GridView;
                GridView GridView11 = e.Row.FindControl("GridView11") as GridView;
                GridView GridView12 = e.Row.FindControl("GridView12") as GridView;
                GridView GridView13 = e.Row.FindControl("GridView13") as GridView;
                GridView GridView14 = e.Row.FindControl("GridView14") as GridView;
                GridView GridView15 = e.Row.FindControl("GridView15") as GridView;
                GridView GridView16 = e.Row.FindControl("GridView16") as GridView;
                GridView GridView17 = e.Row.FindControl("GridView17") as GridView;
                GridView StockMatlGrid = e.Row.FindControl("StockMatlGrid") as GridView;

                string MaterialID = "";
                string MatDimID = "";


                MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

                DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
                DeliveryViewGrid.DataBind();

                SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [Length], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID, HeatTreatID, PlatingID, SubcontractID, SubcontractID2, MaterialID, [Material Dimension], [Material Size], [Active Version], [ScrapRate], [MaterialSource], ProjectManager, Abbr FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

                ListView3.DataSource = SqlDataSource3;
                ListView3.DataBind();

                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, Hours, QtyIn, QtyOut, [JobItemID], [ID], [Comments], JobItemID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";


                GridView10.DataSource = SqlDataSource4;
                GridView10.DataBind();




                SqlDataSource5.SelectCommand = "SELECT [SubcontractID], [WorkCode], [Quantity], [DueDate], CAST(CASE WHEN [HasDetail]=1 THEN 0 ELSE 1 END As Bit) As [Received] FROM [SubcontractItems] WHERE [JobItemID] = " + JobItemID;

                GridView11.DataSource = SqlDataSource5;
                GridView11.DataBind();

                SqlDataSource6.SelectCommand = "SELECT [MatQueueID], [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending], [SuggVendor], [ReqdApproval], [Size], [VendorName], [MaterialID], [MatDimID], [MatSizeID] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

                GridView12.DataSource = SqlDataSource6;
                GridView12.DataBind();

                SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], pct, [MatlPriceID], [MaterialPOID] FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

                GridView13.DataSource = SqlDataSource7;
                GridView13.DataBind();

                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                string sqlstring = "SELECT MaterialID, [Material Dimension] FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;
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

                    MaterialID = reader["MaterialID"].ToString();
                    MatDimID = reader["Material Dimension"].ToString();


                }

                MaterialID = string.IsNullOrEmpty(MaterialID) ? "0" : MaterialID;
                MatDimID = string.IsNullOrEmpty(MatDimID) ? "0" : MatDimID;

                StockInventorySource.SelectCommand = "SELECT [MaterialName], [Dimension], [Diameter] As D, [Height] As H, [Width] As W, [Length] As L, [quantity] As Qty, MatPriceID, PurchasedCut, pct FROM [StockMaterialSummary] WHERE [MaterialID] =" + MaterialID + " And MatDimID =" + MatDimID;

                StockMatlGrid.DataSource = StockInventorySource;
                StockMatlGrid.DataBind();


                SqlDataSource8.SelectCommand = "SELECT [JobItemID], [PartNumber], [DrawingNumber], [Quantity], [ContactName] FROM [FixtureOrders] WHERE [SourceLot] =" + JobItemID;

                GridView14.DataSource = SqlDataSource8;
                GridView14.DataBind();

                sqlstring = "Select DetailID, [Active Version] FROM [Job Item] WHERE [JobItemID] = " + JobItemID + ";";
                // create a connection with sqldatabase 
                System.Data.SqlClient.SqlConnection con2 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                // create a sql command which will user connection string and your select statement string
                System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(sqlstring, con2);
                // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
                System.Data.SqlClient.SqlDataReader reader2;
                // open a connection with sqldatabase
                con2.Open();

                // execute sql command and store a return values in reade
                reader2 = comm2.ExecuteReader();

                while (reader2.Read())
                {

                    DetailID = reader2["DetailID"].ToString();
                    RevisionID = reader2["Active Version"].ToString();

                }

                con2.Close();

                SqlDataSource9.SelectCommand = "SELECT [PartNumber], [Description], [Loc], [Material] FROM [FixtureInvSummary] WHERE [DetailUsingID] = " + DetailID;

                GridView15.DataSource = SqlDataSource9;
                GridView15.DataBind();

                SqlDataSource10.SelectCommand = "SELECT [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [PerAssembly], [NextOp] FROM [AssemblyItemsSummary] WHERE [AssemblyLot] = " + JobItemID;

                GridView16.DataSource = SqlDataSource10;
                GridView16.DataBind();

                SqlDataSource11.SelectCommand = "SELECT [DrawingNumber], [PerAssy], [ItemNumber], [VendorName] FROM [BOMItemSummary] WHERE [AssyRevisionID] = " + RevisionID;

                GridView17.DataSource = SqlDataSource11;
                GridView17.DataBind();
            }
        }

       
        protected void KeepExpanded(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent as GridViewRow;
            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        private void BindChildgvwChildView(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, JobItemID, [ID], [Comments], JobItemID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

            gvChild.DataSource = SqlDataSource4;
            gvChild.DataBind();
        }

        protected void LogHoursGrid_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpandedLog(gvwChild, sender);

        }

        protected void GridView10_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpandedUC(gvwChild, sender);

        }

        protected void GridView10_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;

           
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            TextBox Setup = (TextBox)gvrow.FindControl("SetupBox");
            TextBox Operation = (TextBox)gvrow.FindControl("OperationBox");
            TextBox Comment = (TextBox)gvrow.FindControl("CommentBox");
            DropDownList OperationList = (DropDownList)gvrow.FindControl("ProcDescList");
            CheckBox Completed = (CheckBox)gvrow.FindControl("Done");
            string JobSetupID = gvwChild.DataKeys[e.RowIndex].Values[0].ToString();
            gvwChild.EditIndex = -1;

            KeepExpandedUC(gvwChild, sender);

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();


            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();

            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("EditOperation", con);
            if (string.IsNullOrEmpty(((HiddenField)gvrow.FindControl("hdProcIDList")).Value.Trim()))
            {
                string updatequery = "UPDATE JobSetup SET WorkcodeID = @WorkcodeID, Comments=@Comments, Completed=@completed WHERE JobSetupID = @JobSetupID";
                comm2 = new System.Data.SqlClient.SqlCommand(updatequery, con);
                comm2.Parameters.AddWithValue("@WorkcodeID", Convert.ToInt32(OperationList.SelectedValue.ToString()));
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

        
        protected void GridView10_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = e.NewEditIndex;

            KeepExpandedUC(gvwChild, sender);
        }

        protected void GridView10_RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            string MonseesConnectionString;

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];

            gvwChild.EditIndex = -1;

            KeepExpandedUC(gvwChild, sender);


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

        protected void LogHoursGrid_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;

            double HoursValue;
             
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            TextBox Hours = (TextBox)gvrow.FindControl("Hours");

            TextBox QtyIn = (TextBox)gvrow.FindControl("QtyIn");
            TextBox QtyOut = (TextBox)gvrow.FindControl("QtyOut");
            DropDownList EmplID = (DropDownList)gvrow.FindControl("Empl");
            CheckBox Checked = (CheckBox)gvrow.FindControl("MoveOn");
            

            gvwChild.EditIndex = -1;
           
            KeepExpandedLog(gvwChild, sender);      


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
                gvwChild.DataBind();
            }



        }
            
        protected void LogHoursGrid_RowEditing(Object sender, GridViewEditEventArgs e) 
        {
           
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);            
           
            gvwChild.EditIndex = e.NewEditIndex;
            
            KeepExpandedLog(gvwChild, sender);

            
            
        }




        

        protected void LogHoursGrid_RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            string MonseesConnectionString;
                       
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
           
            gvwChild.EditIndex = -1;

            KeepExpandedLog(gvwChild, sender);

         
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
                gvwChild.DataBind();
            }

        }

        

        protected void GridView12_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpandedGrid4(gvwChild, sender);

        }

        protected void GridView12_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;

           
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            
            TextBox LengthBox = (TextBox)gvrow.FindControl("QuoteLengthBox");
            TextBox QtyBox = (TextBox)gvrow.FindControl("QuoteQtyBox");
            
            
            DropDownList MaterialList = (DropDownList)gvrow.FindControl("QuoteMatList");
            DropDownList DimensionList = (DropDownList)gvrow.FindControl("QuoteDimList");
            DropDownList SizeList = (DropDownList)gvrow.FindControl("QuoteSizeList");
            DropDownList SuggVendorList = (DropDownList)gvrow.FindControl("QuoteVendorList");
            
            CheckBox Cut = (CheckBox)gvrow.FindControl("QuoteCutCheck");
            CheckBox Approve = (CheckBox)gvrow.FindControl("QuoteApprCheck");

            gvwChild.EditIndex = -1;

            KeepExpandedGrid4(gvwChild, sender);

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            string UpdateQuery = "UPDATE MatQuoteItem SET MaterialID=@Material, MatDimID=@Dimension, MatSizeID=@Size, Length=@L, Quantity=@Qty, Cut=@Cut, SuggVendor=@Vendor, ReqdApproval=@Approval WHERE MatQueueID = @MatQueueID";
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(UpdateQuery, con);
            
            comm2.Parameters.AddWithValue("@Material", Convert.ToInt32(MaterialList.SelectedValue));
            comm2.Parameters.AddWithValue("@Dimension", Convert.ToInt32(DimensionList.SelectedValue));
            comm2.Parameters.AddWithValue("@Size", Convert.ToInt32(SizeList.SelectedValue));
            comm2.Parameters.AddWithValue("@L", Convert.ToDouble(LengthBox.Text));
            comm2.Parameters.AddWithValue("@Qty", Convert.ToInt32(QtyBox.Text));
            comm2.Parameters.AddWithValue("@Vendor", Convert.ToInt32(SuggVendorList.SelectedValue));
            comm2.Parameters.AddWithValue("@MatQueueID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Value.ToString()));            
            comm2.Parameters.AddWithValue("@Cut", Convert.ToBoolean(Cut.Checked));
            comm2.Parameters.AddWithValue("@Approval", Convert.ToBoolean(Approve.Checked));

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

        protected void GridView12_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = e.NewEditIndex;

            KeepExpandedGrid4(gvwChild, sender);



        }

        protected void GridView12_RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            string MonseesConnectionString;


            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];

           
            gvwChild.EditIndex = -1;

            KeepExpandedGrid4(gvwChild, sender);

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            string UpdateQuery = "DELETE FROM MatQuoteItem WHERE MatQueueID = @MatQueueID";
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(UpdateQuery, con);

           
            comm2.Parameters.AddWithValue("@MatQueueID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Value.ToString()));
           

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
                Int32 heattreat=0;
                Int32 plating=0;
                Int32 subcontract=0;
                Int32 subcontract2=0;
                Int32 projectmanager=0;
                Int32 material=0;
                Int32 dimension=0;
                Int32 size=0;
                string revision="";
                double length = 0;
                double stockcut=0;
                Int32 partspercut=0;
                Boolean purchasecut=false;
                Boolean drill=false;
                double drillsize=0;
                
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
                    length= string.IsNullOrEmpty(LengthBox.Text) ? 0 : double.Parse(LengthBox.Text);

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

        protected void KeepExpandedUC(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent as GridViewRow;
            BindChildgvwChildViewUC(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        private void BindChildgvwChildViewUC(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, JobItemID, [ID], [Comments] FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

            gvChild.DataSource = SqlDataSource4;
            gvChild.DataBind();
        }

        
        protected void KeepExpandedGrid4(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent as GridViewRow;
            BindChildgvwChildViewGrid4(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        protected void KeepExpandedList(System.Web.UI.WebControls.ListView lvwChild, object sender)
        {
            GridViewRow lvitemParent = ((System.Web.UI.WebControls.ListView)sender).Parent.Parent as GridViewRow;
            BindChildgvwChildViewList(NotClearViewGrid.DataKeys[lvitemParent.RowIndex].Value.ToString(), lvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + lvitemParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        protected void KeepExpandedLog(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvwChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridViewRow gvRowParentParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            BindChildgvwChildLog(gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParentParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        
        private void BindChildgvwChildLog(string jobsetupId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobSetupID = jobsetupId;
            LogHoursGridSource.SelectCommand = "SELECT ProcessID, JobSetupID, Name, Hours, QuantityIn, QuantityOut, EmployeeID FROM LoggedHoursSummary WHERE JobSetupID = " + JobSetupID;
               
            gvChild.DataSource = LogHoursGridSource;
            gvChild.DataBind();
        }

        private void BindChildgvwChildViewGrid4(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource6.SelectCommand = "SELECT [MatQueueID], [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending], [SuggVendor], [ReqdApproval], [Size], [VendorName], [MaterialID], [MatDimID], [MatSizeID] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

            gvChild.DataSource = SqlDataSource6;
            gvChild.DataBind();
        }

        private void BindChildgvwChildViewGrid5(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], pct, [MatlPriceID], [MaterialPOID] FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

            gvChild.DataSource = SqlDataSource7;
            gvChild.DataBind();
        }

         private void BindChildgvwChildViewList(string jobitemId, System.Web.UI.WebControls.ListView lvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [Length], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID, HeatTreatID, PlatingID, SubcontractID, SubcontractID2, MaterialID, [Material Dimension], [Material Size], [Active Version], [ScrapRate], [MaterialSource], ProjectManager, Abbr FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

            lvChild.DataSource = SqlDataSource3;
            lvChild.DataBind();
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

        protected void Empl_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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
                dpl.DataSource = SizeList;
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

        protected void GridView12_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            if (i > -1)
            {

                
                dpl = (DropDownList)Row.FindControl("QuoteMatList");
                if (dpl != null)
                {
                    dpl.DataSource = MaterialList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdQuoteMatl")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)Row.FindControl("QuoteDimList");
                if (dpl != null)
                {
                    dpl.DataSource = DimensionList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdQuoteDim")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }

                dpl = (DropDownList)Row.FindControl("QuoteSizeList");
                if (dpl != null)
                {
                    dpl.DataSource = SizeList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdQuoteSize")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }
            
            
                dpl = (DropDownList)Row.FindControl("QuoteVendorList");
                if (dpl != null)
                {
                    dpl.DataSource = VendorList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdQuoteVendor")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);

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
            
            
           
            if (e.CommandName=="MaterialQuote")
            {

                ListView lvwChild = (ListView)sender;
                UnitOfWork uw = new UnitOfWork();
                uw.Context.Open();
                Int32 totalrows = NotClearViewGrid.Rows.Count;
                Int32 indexval=0;
                
                JobItemID = lvwChild.DataKeys[0].Values[0].ToString();
                for (int i  = 0; i <= totalrows-1; i++)
                {
                    if (NotClearViewGrid.DataKeys[i].Value.ToString() == JobItemID)
                    {
                        indexval = i;
                        break;
                    }
                }
        

                GridView lvwMatQuote = (GridView)NotClearViewGrid.Rows[indexval].FindControl("GridView12");
                
                JobItemData = uw.Context.Query<ViewJobItem>(@"SELECT MaterialID, [Material Dimension] As MaterialDimID, [Material Size] As MaterialSizeID, [Length] As LengthperPart, StockCut, PartsPerCut, PurchaseCut FROM ViewJobItem WHERE JobItemID = @jobitemid", new { jobitemid = JobItemID }).ToList();
                SpecificData = JobItemData.First();
                Label partquantitylbl = e.Item.FindControl("QuantityLabel") as Label;
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

                if (purchasecut==false)
                {
                    quantity = 1;
                    totlength = Math.Ceiling(Math.Ceiling(Convert.ToInt32(partquantitylbl.Text) * (1 + Convert.ToDouble(scrapratelbl.Text))) * length);
                }
                else
                {
                    if (stockcut == 0)
                    {
                        quantity = Convert.ToInt32(Math.Ceiling(Convert.ToInt32(partquantitylbl.Text) * (1 + Convert.ToDouble(scrapratelbl.Text))));
                        totlength = length;
                    }
                    else
                    {
                        quantity = Convert.ToInt32(Math.Ceiling(Math.Ceiling(Convert.ToInt32(partquantitylbl.Text) * (1 + Convert.ToDouble(scrapratelbl.Text))) / partspercut));
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

        protected void LogHoursGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        { 

        }

        protected void GridView10_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridView GridView10 = (GridView)sender;
            GridViewRow gvRow;
            string JobSetupID;
            string JobItemID;
            Int32 NewProcessOrder;
            Int32 totrows = GridView10.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument) % totrows;
            gvRow = GridView10.Rows[index];
            JobSetupID = GridView10.DataKeys[index].Values[0].ToString();
            JobItemID = GridView10.DataKeys[index].Values[1].ToString();
            string CurrProcessOrder = GridView10.DataKeys[index].Values[2].ToString();

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
                            KeepExpandedUC(GridView10, sender);
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
                            KeepExpandedUC(GridView10, sender);
                        }
                        break;
                    case "Down":
                            NewProcessOrder = Convert.ToInt32(CurrProcessOrder) + 1;
                            SqlConnection con2 = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                            con2.Open();
                            string UpdateQueryDwn = "UPDATE [JobSetup] SET ProcessOrder=@OldProcessOrder WHERE JobItemID=@JobItemID AND ProcessOrder=@NewProcessOrder";
                            SqlCommand com2 = new SqlCommand(UpdateQueryDwn, con2);
                            com2.Parameters.AddWithValue("@NewProcessOrder", NewProcessOrder);
                            com2.Parameters.AddWithValue("@OldProcessOrder", NewProcessOrder-1);
                            com2.Parameters.AddWithValue("@JobItemID", JobItemID);
                            com2.ExecuteNonQuery();
                            UpdateQueryDwn = "UPDATE [JobSetup] SET ProcessOrder=@NewProcessOrder WHERE [JobSetupID] = @JobSetupID";
                           
                            com2 = new SqlCommand(UpdateQueryDwn, con2);
                            com2.Parameters.AddWithValue("@NewProcessOrder", NewProcessOrder);
                            com2.Parameters.AddWithValue("@JobSetupID", JobSetupID);
                            com2.ExecuteNonQuery();
                           
                            
                            con2.Close();
                            KeepExpandedUC(GridView10, sender);
                        break;
                    default:
                        break;
                }
        }

        protected void AddOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView10 = gvRowParent.FindControl("GridView10") as GridView;
            DropDownList dpl = (DropDownList)gvRowParent.FindControl("DropDownList2");
            if (dpl != null)
            {
                dpl.DataSource = OperationList;
                dpl.DataBind();
               
            }
            dpl = (DropDownList)gvRowParent.FindControl("EmployeeAddList");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();

            }

            BindChildgvwChildView(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView10);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        protected void CancelAddOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView10 = gvRowParent.FindControl("GridView10") as GridView;

            BindChildgvwChildView(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView10);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        protected void AddNowOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView10 = gvRowParent.FindControl("GridView10") as GridView;
            string JobItemID = gvRowParent.Cells[2].Text;
            DropDownList Operation = (DropDownList)gvRowParent.FindControl("DropDownList2");
            TextBox SetupCostBox = (TextBox)gvRowParent.FindControl("TextBox3");
            TextBox OperationCostBox = (TextBox)gvRowParent.FindControl("TextBox4");
            DropDownList EmployeeAddList = (DropDownList)gvRowParent.FindControl("EmployeeAddList");
            TextBox OrderBox = (TextBox)gvRowParent.FindControl("RequestedOrderBox");
            TextBox OpCommentBox = (TextBox)gvRowParent.FindControl("OpCommentBox");
            string OperationID = Operation.SelectedValue.ToString();
            string SetupCost = SetupCostBox.Text;
            string OperationCost = OperationCostBox.Text;
            string description = OpCommentBox.Text;
            string createemployee = EmployeeAddList.SelectedValue.ToString();
            string order = OrderBox.Text;


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand("AddOperationtoLot", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@jobitemID", Convert.ToInt32(JobItemID));
            com.Parameters.AddWithValue("@operationID", Convert.ToInt32(OperationID));
            com.Parameters.AddWithValue("@setupcost", SetupCost);
            com.Parameters.AddWithValue("@operationcost", OperationCost);
            com.Parameters.AddWithValue("@description", description);
            com.Parameters.AddWithValue("@employee", createemployee);
            com.Parameters.AddWithValue("@ProcessOrder", Convert.ToInt32(order));
            
            con.Open();
            com.ExecuteNonQuery();
           

            con.Close();

            BindChildgvwChildView(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView10);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        protected void AddAlloc_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView10 = gvRowParent.FindControl("GridView10") as GridView;
            
            BindChildgvwChildView(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView10);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        protected void CancelAddAlloc_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView10 = gvRowParent.FindControl("GridView10") as GridView;

            BindChildgvwChildView(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView10);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }



        protected void LogHoursGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
       
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;

            if (i > -1)
            {

                dpl = (DropDownList)Row.FindControl("Empl");
                if (dpl != null)
                {
                    dpl.DataSource = EmployeeList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdEmpl")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }
            }

        }

        protected void GridView10_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
            string JobSetupID="0";
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            GridView LogHoursGrid = (GridView)Row.FindControl("LogHoursGrid");

            if (Row.RowIndex > -1)
            {
                JobSetupID = Grid.DataKeys[Row.RowIndex].Values[0].ToString();
                LogHoursGridSource.SelectCommand = "SELECT ProcessID, JobSetupID, Name, Hours, QuantityIn, QuantityOut, EmployeeID FROM LoggedHoursSummary WHERE JobSetupID = " + JobSetupID;
                LogHoursGrid.DataSource = LogHoursGridSource;
                LogHoursGrid.DataBind();
            }

            if (i > -1)
            {
                
                dpl = (DropDownList)Row.FindControl("ProcDescList");
                if (dpl != null)
                {
                    if ((string.IsNullOrEmpty(((HiddenField)Row.FindControl("hdProcIDList")).Value.Trim())))
                    {
                        dpl.DataSource = WorkcodeList;
                        dpl.DataTextField = "Workcode";
                        dpl.DataValueField = "WorkcodeID";
                        dpl.DataBind();
                        string val = ((HiddenField)Row.FindControl("hdProcDescList")).Value.Trim();
                        dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                    }
                    else
                    {
                        dpl.DataSource = OperationList;
                        dpl.DataTextField = "OperationName";
                        dpl.DataValueField = "OperationID";
                        dpl.DataBind();
                        string val = ((HiddenField)Row.FindControl("hdProcDescList")).Value.Trim();
                        dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                    }
                }
            }
        }

        protected void LogNewNow_Command(object sender, CommandEventArgs e)
        {
            string MonseesConnectionString;

            Int32 HoursValue;
            Int32 QtyInValue;
            Int32 QtyOutValue;

            System.Web.UI.WebControls.GridViewRow gvwChild = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView gvChild = (GridView)gvwChild.Parent.Parent;
            GridView gvChildChild = (GridView)gvwChild.FindControl("LogHoursGrid");
            TextBox Hours = (TextBox)gvwChild.FindControl("HoursAdd");
            TextBox QtyIn = (TextBox)gvwChild.FindControl("QtyInAdd");
            TextBox QtyOut = (TextBox)gvwChild.FindControl("QtyOutAdd");
            DropDownList EmplID = (DropDownList)gvwChild.FindControl("EmployeeList2");
            CheckBox Checked = (CheckBox)gvwChild.FindControl("MoveOn");


            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView10") as GridView;

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
                HoursValue = Convert.ToInt32(Hours.Text);
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
                gvChildChild.DataBind();
            }
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvChild).Parent.Parent.Parent.Parent as GridView;
            BindChildgvwChildLog(gvChild.DataKeys[gvwChild.RowIndex].Values[0].ToString(), gvChildChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }

        }

        protected void LogNew_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView10 = gvRowParent.FindControl("GridView10") as GridView;
            GridViewRow LogHoursGrid = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            DropDownList dpl = (DropDownList)LogHoursGrid.FindControl("EmployeeList2");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();
               
            }
            //BindChildgvwChildView(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView10);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        
        }

        protected void CancelLog_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView10 = gvRowParent.FindControl("GridView10") as GridView;
            BindChildgvwChildView(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView10);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        
        }


        protected void StockMatlGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView13 = (GridView)gvRowParent.FindControl("GridView13");
            GridView StockMaterialGrid = (GridView)sender;
            ListView ListView3 = (ListView)gvRowParent.FindControl("ListView3");
            ListViewItem ListItem = ListView3.Items[0];            

            string JobItemID = NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Values[0].ToString();
            string MatPriceID = StockMaterialGrid.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
            

            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            //string updatequery = "INSERT INTO MaterialAlloc (JobItemID, MatlPriceID, pct) VALUES (@jobitemID, @matpriceID, @pct)";

            SqlCommand com = new SqlCommand("AllocateMaterial", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@jobitemID", Convert.ToInt32(JobItemID));
            com.Parameters.AddWithValue("@matpriceID", Convert.ToInt32(MatPriceID));
            SqlParameter returnval = com.Parameters.Add("@returnval", SqlDbType.Int);
            returnval.Direction = ParameterDirection.ReturnValue;

            
            con.Open();
            com.ExecuteNonQuery();
            if ((int)returnval.Value == 0) MessageBox("This material looks to be insufficient for this lot.  You will need to reach out to the material manager to allocate this material to the current lot.");

            con.Close();
            View AllocView = (View)gvRowParent.FindControl("ExistAllocView");
            MultiView AllocMulti = (MultiView)gvRowParent.FindControl("MatAllocMulti");
            AllocMulti.SetActiveView(AllocView);

            BindChildgvwChildViewGrid5(NotClearViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView13);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('divUC" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        
        }

        //This is where the new code is starts pasting
        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
            string JobSetupID = "0";
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            GridView LogHoursGrid = (GridView)Row.FindControl("LogHoursGridGV2");

            if (Row.RowIndex > -1)
            {
                dpl = (DropDownList)Row.FindControl("ProcDescList");
                if (dpl != null)
                {
                    dpl.DataSource = OperationList;
                    dpl.DataBind();
                    string val = ((HiddenField)Row.FindControl("hdProcIDList")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
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
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
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
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }


        protected void CancelAddOpGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        protected void AddNowOpGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            string JobItemID = gvRowParent.Cells[2].Text;
            DropDownList Operation = (DropDownList)gvRowParent.FindControl("DropDownList2GV2");
            TextBox SetupCostBox = (TextBox)gvRowParent.FindControl("TextBox3GV2");
            TextBox OperationCostBox = (TextBox)gvRowParent.FindControl("TextBox3GV2");
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
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
        }

        protected void LogNewNowGV2_Command(object sender, CommandEventArgs e)
        {
            string MonseesConnectionString;

            Int32 HoursValue;
            Int32 QtyInValue;
            Int32 QtyOutValue;

            System.Web.UI.WebControls.GridViewRow gvwChild = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView gvChild = (GridView)gvwChild.Parent.Parent;
            GridView gvChildChild = (GridView)gvwChild.FindControl("LogHoursGridGV2");
            TextBox Hours = (TextBox)gvwChild.FindControl("HoursAddGV2");
            TextBox QtyIn = (TextBox)gvwChild.FindControl("QtyInAddGV2");
            TextBox QtyOut = (TextBox)gvwChild.FindControl("QtyOutAddGV2");
            DropDownList EmplID = (DropDownList)gvwChild.FindControl("EmployeeListGV2");
            CheckBox Checked = (CheckBox)gvwChild.FindControl("MoveOnGV2");


            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
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
                HoursValue = Convert.ToInt32(Hours.Text);
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
                gvChildChild.DataBind();
            }
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvChild).Parent.Parent.Parent.Parent as GridView;
            BindChildgvwChildLog(gvChild.DataKeys[gvwChild.RowIndex].Values[0].ToString(), gvChildChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }

        }

        protected void LogHoursGridGV2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void KeepExpandedLogGV2(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvwChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridViewRow gvRowParentParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            BindChildgvwChildLogGV2(gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParentParent.Cells[2].Text + "').style.display = 'inline';", true);
            }
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
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            GridViewRow LogHoursGrid = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            DropDownList dpl = (DropDownList)LogHoursGrid.FindControl("EmployeeListGV2");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();

            }
            //BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }

        }

        protected void CancelLogGV2_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent.Cells[2].Text + "').style.display = 'inline';", true);
            }

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
                comm2.Parameters.AddWithValue("@WorkcodeID", Convert.ToInt32(OperationList.SelectedValue.ToString()));
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
    }
}
 

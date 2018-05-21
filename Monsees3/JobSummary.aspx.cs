using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
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
    public partial class JobSummary : DataPage
    {
        private string MonseesConnectionString;
        public Int32 jobId = 0;
        private Int32 index;
        private List<OpenOperationLine> EditTracking = new List<OpenOperationLine>();
        private string SourceVal = "0";
        public List<MaterialModel> MaterialList { get; set; }
        public List<DimensionModel> DimensionList { get; set; }
        public List<MaterialSizeModel> SizeList { get; set; }
        public List<WorkcodeModel> WorkcodeList { get; set; }
        public List<VendorListModel> VendorList { get; set; }
        public List<ViewJobItem> JobItemData { get; set; }
        public List<OperationListModel> OperationList { get; set; }
        ViewJobItem SpecificData { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            Int32.TryParse(Request["JobID"], out jobId);

            // Check if the user is already logged in or not
            if (!IsPostBack)
            {
                

                SqlDataSource2.SelectCommand = "SELECT [JobID], [JobNumber], [IsOpen], [CreateDate], [ClosedDate], [CompanyName], [ContactName], Clear FROM [JobSummary] WHERE JobID=" + jobId;
                ListView1.DataSource = SqlDataSource2;
                ListView1.DataBind();
                SqlDataSource1.SelectCommand = "SELECT [Notes], [Quantity], [IsOpen], [PartNumber], [DrawingNumber], [JobItemID], [Revision Number] AS Revision_Number, [NextDelivery], [FileType], CAST([RTS] AS Bit) AS [RTS], [MaxOfCurrDelivery], Clear, [Active Version], Hot, NewRenew, CAbbr, NewPart, ITAR FROM [JobItem] WHERE Quantity > 0 And JobID = " + jobId;
                GridView1.DataSource = SqlDataSource1;
                GridView1.DataBind();
            }
            GetData();
            
        }

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            MaterialList = inspectionRepository.GetMaterials();
            DimensionList = inspectionRepository.GetDimensions();
            SizeList = inspectionRepository.GetMaterialSizes();
            WorkcodeList = inspectionRepository.GetWorkcodes();
            VendorList = inspectionRepository.GetVendors();
            OperationList = inspectionRepository.GetOperations();

            this.UnitOfWork.End();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {

            string command_name = e.CommandName;

            if ((command_name == "GetFile") || (command_name == "Clear") || (command_name == "AddFixt") || (command_name == "AllocFixt") || (command_name == "InitCAR") || (command_name == "PartHistory") || (command_name == "Inspection") || (command_name == "QuickFixture"))
            {

                string JobItemID = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[0].ToString();
                string RevisionID = GridView1.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
                switch (e.CommandName)
                {
                    case "GetFile":
                        String PartNumber;
                        String RevNumber;
                        string updatequery = "";
                        GridViewRow clickedRow = GridView1.Rows[Convert.ToInt32(e.CommandArgument)];
                        PartNumber = clickedRow.Cells[2].Text;
                        RevNumber = clickedRow.Cells[3].Text;
                        Response.Redirect("pdfhandler.ashx?FileID=" + RevisionID + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);
                        break;

                    case "Clear":
                        bool Cleared = ((CheckBox)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[15].Controls[0]).Checked;
                        if (Cleared)
                        {
                            updatequery = "UPDATE [Job Item] SET Clear = 0 WHERE JobItemID = " + JobItemID;
                        }
                        else
                        {
                            updatequery = "UPDATE [Job Item] SET Clear = 1 WHERE JobItemID = " + JobItemID;
                        }
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con.Open();

                        System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(updatequery, con);
                        comm2.ExecuteNonQuery();
                        con.Close();
                        SqlDataSource1.SelectCommand = "SELECT [Notes], [Quantity], [IsOpen], [PartNumber], [DrawingNumber], [JobItemID], [Revision Number] AS Revision_Number, [NextDelivery], [FileType], CAST([RTS] AS Bit) AS [RTS], [MaxOfCurrDelivery], Clear, [Active Version], NewRenew, Hot, NewPart, CAbbr, ITAR FROM [JobItem] WHERE Quantity > 0 And JobID = " + jobId;
                        GridView1.DataSource = SqlDataSource1;
                        GridView1.DataBind();
                        break;

                    case "AddFixt":
                        Response.Write("<script type='text/javascript'>window.open('AddFixture.aspx?SourceLot=" + JobItemID + "','_blank');</script>");
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {


                            if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                            {
                                GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                            }

                            if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                            {
                                GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                            }


                            if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                            {
                                GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                            }

                            if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                            {
                                string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                                if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                                {
                                    GridView1.Rows[i].Font.Bold = true;

                                }


                            }


                        }
                        break;

                    case "AllocFixt":
                        Response.Write("<script type='text/javascript'>window.open('AllocateFixture.aspx?SourceLot=" + JobItemID + "','_blank');</script>");
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {


                            if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                            {
                                GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                            }

                            if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                            {
                                GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                            }


                            if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                            {
                                GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                            }

                            if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                            {
                                string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                                if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                                {
                                    GridView1.Rows[i].Font.Bold = true;

                                }


                            }


                        }
                        break;


                    case "InitCAR":


                        //Check to see if user is already logged in                        
                        Response.Write("<script type='text/javascript'>window.open('CARInitiate.aspx?id=" + JobItemID + "');</script>");
                        break;

                    default:

                        break;

                }
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                

                if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#ffc880");

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

        [WebMethod]
        protected void ExpandCollapse(object sender, EventArgs e)
        {
            GridViewRow ProdGrid = (GridViewRow)((Button)sender).Parent.Parent;
            GridView Prod = (GridView)((Button)sender).Parent.Parent.Parent.Parent;
            if (ProdGrid.RowType == DataControlRowType.DataRow)
            {
                index = ProdGrid.RowIndex;
                string JobItemID = GridView1.DataKeys[index].Values[0].ToString();
                string DetailID = "0";
                string RevisionID = "0";

                ListView ListView2 = ProdGrid.FindControl("ListView2") as ListView;
                GridView DeliveryViewGrid = ProdGrid.FindControl("DeliveryViewGrid") as GridView;

                GridView GridView2 = ProdGrid.FindControl("GridView2") as GridView;
                GridView GridView3 = ProdGrid.FindControl("GridView3") as GridView;
                GridView GridView4 = ProdGrid.FindControl("GridView4") as GridView;
                GridView GridView5 = ProdGrid.FindControl("GridView5") as GridView;
                GridView GridView6 = ProdGrid.FindControl("RevisionFixtureOrders") as GridView;
                GridView CertGrid = ProdGrid.FindControl("CertGrid") as GridView;
                GridView GridView8 = ProdGrid.FindControl("GridView8") as GridView;
                GridView GridView9 = ProdGrid.FindControl("GridView9") as GridView;
                GridView StockMatlGrid = ProdGrid.FindControl("StockMatlGrid") as GridView;

                GridView CARView = ProdGrid.FindControl("CARView") as GridView;

                string MaterialID = "";
                string MatDimID = "";


                MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

                DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
                DeliveryViewGrid.DataBind();

                SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [Length], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID, HeatTreatID, PlatingID, SubcontractID, SubcontractID2, MaterialID, [Material Dimension], [Material Size], [Active Version], [ScrapRate], [MaterialSource], ProjectManager, Abbr FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

                ListView2.DataSource = SqlDataSource3;
                ListView2.DataBind();

                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, Hours, QtyIn, QtyOut, [JobItemID], [ID], [Comments], SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";


                GridView2.DataSource = SqlDataSource4;
                GridView2.DataBind();




                SqlDataSource5.SelectCommand = "SELECT [SubcontractID], [WorkCode], [Quantity], [DueDate], CAST(CASE WHEN [HasDetail]=1 THEN 0 ELSE 1 END As Bit) As [Received] FROM [SubcontractItems] WHERE [JobItemID] = " + JobItemID;

                GridView3.DataSource = SqlDataSource5;
                GridView3.DataBind();

                SqlDataSource6.SelectCommand = "SELECT [MatQueueID], [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending], [SuggVendor], [ReqdApproval], [Size], [VendorName], [MaterialID], [MatDimID], [MatSizeID] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

                GridView4.DataSource = SqlDataSource6;
                GridView4.DataBind();

                SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], pct, MatPriceID, MatlAllocationID FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

                GridView5.DataSource = SqlDataSource7;
                GridView5.DataBind();

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

                StockInventorySource.SelectCommand = "SELECT [MaterialName], [Dimension], [Diameter] As D, [Height] As H, [Width] As W, [Length] As L, [quantity] As Qty, MatPriceID, PurchasedCut, pct FROM [StockMaterialSummary] WHERE [MaterialID] =" + (string.IsNullOrEmpty(MaterialID) ? "0" : MaterialID) + " And MatDimID = " + (string.IsNullOrEmpty(MatDimID) ? "0" : MatDimID);

                StockMatlGrid.DataSource = StockInventorySource;
                StockMatlGrid.DataBind();



                SqlDataSource8.SelectCommand = "SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName], Location, Note, OperationName, FixtureRevID FROM [FixtureOrders] WHERE [DetailUsingID] =" + DetailID;

                GridView6.DataSource = SqlDataSource8;
                GridView6.DataBind();

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

                SqlDataSourceCert.SelectCommand = "SELECT [CertCompReqd], [MatlCertReqd], [PlateCertReqd], [SerializationReqd] FROM Version WHERE RevisionID = " + RevisionID;

                CertGrid.DataSource = SqlDataSourceCert;
                CertGrid.DataBind();


                SqlDataSource10.SelectCommand = "SELECT [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [PerAssembly], [NextOp] FROM [AssemblyItemsSummary] WHERE [AssemblyLot] = " + JobItemID;

                GridView8.DataSource = SqlDataSource10;
                GridView8.DataBind();

                SqlDataSource11.SelectCommand = "SELECT [DrawingNumber], [PerAssy], [ItemNumber], [VendorName], [Each], [Weblink] FROM [BOMItemSummary] WHERE [AssyRevisionID] = " + RevisionID;

                GridView9.DataSource = SqlDataSource11;
                GridView9.DataBind();

                SqlDataSource12.SelectCommand = "SELECT * FROM CorrectiveActionView WHERE [DetailID] = " + DetailID;

                CARView.DataSource = SqlDataSource12;
                CARView.DataBind();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {


                    if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                    {
                        GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                    }

                    if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                    {
                        GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                    }


                    if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                    {
                        GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                    }

                    if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                    {
                        string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                        if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                        {
                            GridView1.Rows[i].Font.Bold = true;

                        }


                    }


                }

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

        [WebMethod]
        protected void ExpandCollapseIndependent(object sender)
        {
            GridViewRow ProdGrid = (GridViewRow)((Button)sender).Parent.Parent;
            GridView Prod = (GridView)((Button)sender).Parent.Parent.Parent.Parent;
            if (ProdGrid.RowType == DataControlRowType.DataRow)
            {
                index = ProdGrid.RowIndex;
                string JobItemID = GridView1.DataKeys[index].Values[0].ToString();
                string DetailID = "0";
                string RevisionID = "0";

                ListView ListView2 = ProdGrid.FindControl("ListView2") as ListView;
                GridView DeliveryViewGrid = ProdGrid.FindControl("DeliveryViewGrid") as GridView;

                GridView GridView2 = ProdGrid.FindControl("GridView2") as GridView;
                GridView GridView3 = ProdGrid.FindControl("GridView3") as GridView;
                GridView GridView4 = ProdGrid.FindControl("GridView4") as GridView;
                GridView GridView5 = ProdGrid.FindControl("GridView5") as GridView;
                GridView GridView6 = ProdGrid.FindControl("RevisionFixtureOrders") as GridView;
                GridView CertGrid = ProdGrid.FindControl("CertGrid") as GridView;
                GridView GridView8 = ProdGrid.FindControl("GridView8") as GridView;
                GridView GridView9 = ProdGrid.FindControl("GridView9") as GridView;
                GridView StockMatlGrid = ProdGrid.FindControl("StockMatlGrid") as GridView;

                GridView CARView = ProdGrid.FindControl("CARView") as GridView;

                string MaterialID = "";
                string MatDimID = "";


                MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

                DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
                DeliveryViewGrid.DataBind();

                SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [Length], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID, HeatTreatID, PlatingID, SubcontractID, SubcontractID2, MaterialID, [Material Dimension], [Material Size], [Active Version], [ScrapRate], [MaterialSource], ProjectManager, Abbr FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

                ListView2.DataSource = SqlDataSource3;
                ListView2.DataBind();

                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, Hours, QtyIn, QtyOut, [JobItemID], [ID], [Comments], SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";


                GridView2.DataSource = SqlDataSource4;
                GridView2.DataBind();




                SqlDataSource5.SelectCommand = "SELECT [SubcontractID], [WorkCode], [Quantity], [DueDate], CAST(CASE WHEN [HasDetail]=1 THEN 0 ELSE 1 END As Bit) As [Received] FROM [SubcontractItems] WHERE [JobItemID] = " + JobItemID;

                GridView3.DataSource = SqlDataSource5;
                GridView3.DataBind();

                SqlDataSource6.SelectCommand = "SELECT [MatQueueID], [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending], [SuggVendor], [ReqdApproval], [Size], [VendorName], [MaterialID], [MatDimID], [MatSizeID] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

                GridView4.DataSource = SqlDataSource6;
                GridView4.DataBind();

                SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], pct, MatPriceID, MatlAllocationID FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

                GridView5.DataSource = SqlDataSource7;
                GridView5.DataBind();

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

                StockInventorySource.SelectCommand = "SELECT [MaterialName], [Dimension], [Diameter] As D, [Height] As H, [Width] As W, [Length] As L, [quantity] As Qty, MatPriceID, PurchasedCut, pct FROM [StockMaterialSummary] WHERE [MaterialID] =" + (string.IsNullOrEmpty(MaterialID) ? "0" : MaterialID) + " And MatDimID = " + (string.IsNullOrEmpty(MatDimID) ? "0" : MatDimID);

                StockMatlGrid.DataSource = StockInventorySource;
                StockMatlGrid.DataBind();



                SqlDataSource8.SelectCommand = "SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName], Location, Note, OperationName, FixtureRevID FROM [FixtureOrders] WHERE [DetailUsingID] =" + DetailID;

                GridView6.DataSource = SqlDataSource8;
                GridView6.DataBind();

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

                SqlDataSourceCert.SelectCommand = "SELECT [CertCompReqd], [MatlCertReqd], [PlateCertReqd], [SerializationReqd] FROM Version WHERE RevisionID = " + RevisionID;

                CertGrid.DataSource = SqlDataSourceCert;
                CertGrid.DataBind();


                SqlDataSource10.SelectCommand = "SELECT [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [PerAssembly], [NextOp] FROM [AssemblyItemsSummary] WHERE [AssemblyLot] = " + JobItemID;

                GridView8.DataSource = SqlDataSource10;
                GridView8.DataBind();

                SqlDataSource11.SelectCommand = "SELECT [DrawingNumber], [PerAssy], [ItemNumber], [VendorName], [Each], [Weblink] FROM [BOMItemSummary] WHERE [AssyRevisionID] = " + RevisionID;

                GridView9.DataSource = SqlDataSource11;
                GridView9.DataBind();

                SqlDataSource12.SelectCommand = "SELECT * FROM CorrectiveActionView WHERE [DetailID] = " + DetailID;

                CARView.DataSource = SqlDataSource12;
                CARView.DataBind();

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {


                    if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                    {
                        GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                    }

                    if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                    {
                        GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                    }


                    if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                    {
                        GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                    }

                    if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                    {
                        string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                        if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                        {
                            GridView1.Rows[i].Font.Bold = true;

                        }


                    }


                }

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

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }

        protected void LogHoursGrid_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpandedLog(gvwChild, sender);

        }

        protected void GridView2_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpanded(gvwChild, sender);
            
        }

        protected void GridView2_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;
            string JobSetupID;
           
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            TextBox Setup = (TextBox)gvrow.FindControl("SetupBox");
            TextBox Operation = (TextBox)gvrow.FindControl("OperationBox");
            TextBox Comment = (TextBox)gvrow.FindControl("CommentBox");
            DropDownList OperationList = (DropDownList)gvrow.FindControl("ProcDescList");
            CheckBox Completed = (CheckBox)gvrow.FindControl("Done");
            JobSetupID = gvwChild.DataKeys[e.RowIndex].Values[0].ToString();
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

        protected void GridView2_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = e.NewEditIndex;

            KeepExpanded(gvwChild, sender);



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

        protected void GridView4_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpandedGrid4(gvwChild, sender);

        }

        protected void GridView4_RowUpdate(Object sender, GridViewUpdateEventArgs e)
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

        protected void GridView4_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = e.NewEditIndex;

            KeepExpandedGrid4(gvwChild, sender);



        }

        protected void GridView4_RowDelete(Object sender, GridViewDeleteEventArgs e)
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

        protected void ListView2_ItemEditing(object sender, ListViewEditEventArgs e)
        {
            Int32 projectmanager = 0;
            Int32 matlsource = 0;
            System.Web.UI.WebControls.ListView lvwChild = ((System.Web.UI.WebControls.ListView)sender);
            lvwChild.EditIndex = e.NewEditIndex;
            //GridViewRow gvRowParent = ((System.Web.UI.WebControls.ListView)sender).Parent.Parent as GridViewRow;
            //string JobItemID = GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString();
            //SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID, HeatTreatID, PlatingID, SubcontractID, SubcontractID2, MaterialID, [Material Dimension], [Material Size] FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

            //lvwChild.DataBind();
            //ListViewItem item = lvwChild.Items[e.NewEditIndex];
            //if (item != null)
            //{
            DropDownList MatlSourceDrop = (lvwChild.Items[0].FindControl("MatlSourceList")) as DropDownList;
            Label MatlSourceText = (lvwChild.Items[0].FindControl("MatlSourceLabel")) as Label;
           
                switch (MatlSourceText.Text)
                {
                    case "Purchased":
                        SourceVal = "3";
                        break;
                    case "Stock":
                        SourceVal = "1";
                        break;
                    case "Customer":
                        SourceVal = "2";
                        break;
                    default:
                        break;
                }
                

            
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
        protected void ListView2_ItemUpdating(object sender, ListViewUpdateEventArgs e)
            {
                Int32 heattreat=0;
                Int32 plating=0;
                Int32 subcontract=0;
                Int32 subcontract2=0;
                Int32 projectmanager = 0;
                string comments="";
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
                string drillsizestring;
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
                TextBox CommentsBox = (lvwChild.Items[e.ItemIndex].FindControl("CommentsBox")) as TextBox;
                if (CommentsBox != null)
                    comments = CommentsBox.Text;
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
                
                string UpdateQuery = "UPDATE [Version] SET [HeatTreatID] = @heattreat, [PlatingID] = @plating, [SubcontractID] = @subcontract, [SubcontractID2] = @subcontract2, [Notes] = @comments, [MaterialID] = @material, [Material Dimension] = @dimension, [Material Size] = @size, [Length per Part]=@length, StockCut=@stockcut, PartsPerCut=@partspercut, PurchaseCut=@purchasecut, Drill=@drill, DrillSize=@drillsize WHERE [RevisionID] = @revision";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                con.Open();
                SqlCommand com = new SqlCommand(UpdateQuery, con);
                com.Parameters.AddWithValue("@heattreat", heattreat);
                com.Parameters.AddWithValue("@plating", plating);
                com.Parameters.AddWithValue("@subcontract", subcontract);
                com.Parameters.AddWithValue("@subcontract2", subcontract2);
                com.Parameters.AddWithValue("@comments", comments);
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

        protected void ListView2_ItemCanceling(object sender, ListViewCancelEventArgs e)
          {
              System.Web.UI.WebControls.ListView lvwChild = ((System.Web.UI.WebControls.ListView)sender);
              lvwChild.EditIndex = -1;
              KeepExpandedList(lvwChild, sender);
          }

        protected void KeepExpanded(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent as GridViewRow;
            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);            
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                    {
                        GridView1.Rows[i].Font.Bold = true;

                    }


                }


            }
        }

        protected void KeepExpandedGrid4(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent as GridViewRow;
            BindChildgvwChildViewGrid4(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                    {
                        GridView1.Rows[i].Font.Bold = true;

                    }


                }


            }
        }

        protected void KeepExpandedList(System.Web.UI.WebControls.ListView lvwChild, object sender)
        {
            GridViewRow lvitemParent = (GridViewRow)lvwChild.Parent.Parent.Parent;
            BindChildgvwChildViewList(GridView1.DataKeys[lvitemParent.RowIndex].Value.ToString(), lvwChild);
            index = lvitemParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                    {
                        GridView1.Rows[i].Font.Bold = true;

                    }


                }


            }
        }

        protected void KeepExpandedLog(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvwChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridViewRow gvRowParentParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            BindChildgvwChildLog(gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), gvwChild);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                    {
                        GridView1.Rows[i].Font.Bold = true;

                    }


                }


            }
        }

        private void BindChildgvwChildView(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [JobItemID], [ID], [Comments], SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

            gvChild.DataSource = SqlDataSource4;
            gvChild.DataBind();
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
            SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], pct, MatPriceID FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

            gvChild.DataSource = SqlDataSource7;
            gvChild.DataBind();
        }

        private void BindChildgvwChildViewStockRet(string MaterialID, System.Web.UI.WebControls.GridView gvChild)
        {
            
            RetMatlSource.SelectCommand = "SELECT MatPriceID, [MaterialName], [Dimension], [Diameter] As D, [Height] As H, [Width] As W, [Length] As L, [quantity] As Qty, MatlAllocationID FROM [StockMaterialSummary] WHERE [MatPriceID] =" + (string.IsNullOrEmpty(MaterialID) ? "0" : MaterialID);

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

     

        protected void ListView2_ItemDataBound(object sender, ListViewItemEventArgs e)
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
                dpl.DataSource = WorkcodeList;
                dpl.DataBind();
                string val = ((HiddenField)item.FindControl("hdHeatTreat")).Value.Trim();
                dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
            }

            dpl = (DropDownList)item.FindControl("PlatingList");
            if (dpl != null)
            {
                dpl.DataSource = WorkcodeList;
                dpl.DataBind();
                string val = ((HiddenField)item.FindControl("hdPlating")).Value.Trim();
                dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
            }

            dpl = (DropDownList)item.FindControl("SubcontractList");
            if (dpl != null)
            {
                dpl.DataSource = WorkcodeList;
                dpl.DataBind();
                string val = ((HiddenField)item.FindControl("hdSubcontract")).Value.Trim();
                dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
            }

            dpl = (DropDownList)item.FindControl("Subcontract2List");
            if (dpl != null)
            {
                dpl.DataSource = WorkcodeList;
                dpl.DataBind();
                string val = ((HiddenField)item.FindControl("hdSubcontract2")).Value.Trim();
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

            dpl = (DropDownList)item.FindControl("PMList");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();
                string val = ((HiddenField)item.FindControl("hdPM")).Value.Trim();
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

                dpl = (DropDownList)item.FindControl("MatlSourceList");
                if (dpl != null)
                {

                    
                    dpl.SelectedValue = SourceVal;
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

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
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
                    dpl.DataSource = SizeList.FindAll(Size => Size.MaterialDimID.Equals(Convert.ToInt32(string.IsNullOrEmpty(((HiddenField)Row.FindControl("hdQuoteDim")).Value.Trim()) ? "0" : ((HiddenField)Row.FindControl("hdQuoteDim")).Value.Trim())));
                    
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

        protected void ListView2_ItemCommand(object sender, ListViewCommandEventArgs e)
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
                GridViewRow ParentRow = (GridViewRow)e.Item.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent;

                UnitOfWork uw = new UnitOfWork();
                uw.Context.Open();
                Int32 totalrows = GridView1.Rows.Count;
                Int32 indexval = 0;

                JobItemID = lvwChild.DataKeys[0].Values[0].ToString();
                string RevisionID = lvwChild.DataKeys[0].Values[1].ToString();
                for (int i = 0; i <= totalrows - 1; i++)
                {
                    if (GridView1.DataKeys[i].Value.ToString() == JobItemID)
                    {
                        indexval = i;
                        break;
                    }
                }


                GridView lvwMatQuote = (GridView)GridView1.Rows[indexval].FindControl("GridView4");

                CheckBox ApprovalLabel = (e.Item.FindControl("Approval")) as CheckBox;
                approval = ApprovalLabel.Checked;

                string UpdateQuery = "INSERT INTO MatQuoteItem (JobItemID, MaterialID, MatDimID, MatSizeID, Length, Quantity, Cut, SuggVendor, ReqdApproval) VALUES (@JobItemID, @material, @dimension, @size, @totlength, @quantity, @purchasecut, @suggested, @approvalreqd);";
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                con.Open();
                SqlCommand com = new SqlCommand("AddMatQuote", con);
                com.CommandType = CommandType.StoredProcedure;

                com.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(JobItemID));
                com.Parameters.AddWithValue("@revision", Convert.ToInt32(RevisionID));

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
            byte[] buffer;
            Guid fileId = Guid.NewGuid();
            string SetupID;
            SetupID = GridView2.DataKeys[index].Values[2].ToString();

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
                            com2.Parameters.AddWithValue("@OldProcessOrder", NewProcessOrder-1);
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
                case "OrderFixture":
                    Response.Write("<script type='text/javascript'>window.open('AddFixture.aspx?SourceLot=" + JobItemID + "&SourceSetup=" + SetupID + "','_blank');</script>");
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {


                        if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                        }

                        if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                        }


                        if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                        }

                        if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                        {
                            string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                            if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                            {
                                GridView1.Rows[i].Font.Bold = true;

                            }


                        }
                    }
                    KeepExpanded(GridView2, sender);
                    break;
                case "Attach":
                    FileUpload myFileTest = (FileUpload)GridView2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("filMyFileTest");
                    if (myFileTest.HasFile)
                    {
                        buffer = new byte[(int)myFileTest.FileContent.Length];
                        myFileTest.FileContent.Read(buffer, 0, buffer.Length);
                        if (myFileTest.FileContent.Length > 0)
                        {
                            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                            {
                                con.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SetupImageAdd", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@SetupID", SetupID);
                                cmd.Parameters.AddWithValue("@SetupImageID", fileId);
                                cmd.Parameters.AddWithValue("@SetupImage", buffer);


                                cmd.ExecuteNonQuery();


                                con.Close();
                            }



                        }
                        KeepExpanded(GridView2, sender);



                    }
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {


                        if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                        }

                        if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                        }


                        if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                        }

                        if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                        {
                            string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                            if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                            {
                                GridView1.Rows[i].Font.Bold = true;

                            }


                        }


                    }
                    break;
                case "QuickFixture":
                    Response.Write("<script type='text/javascript'>window.open('QuickFixture.aspx?SourceLot=" + JobItemID + "&SourceSetup=" + SetupID + "','_blank');</script>");
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {


                        if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                        }

                        if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                        }


                        if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                        {
                            GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                        }

                        if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                        {
                            string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                            if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                            {
                                GridView1.Rows[i].Font.Bold = true;

                            }


                        }
                    }
                    KeepExpanded(GridView2, sender);
                    break;
                default:
                        break;
                }
        }

        protected void AddOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
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

            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void CancelAddOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;

            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void AddNowOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            string JobItemID = gvRowParent.Cells[1].Text;
            DropDownList Operation = (DropDownList)gvRowParent.FindControl("DropDownList2");
            TextBox SetupCostBox = (TextBox)gvRowParent.FindControl("TextBox3");
            TextBox OperationCostBox = (TextBox)gvRowParent.FindControl("TextBox4");
            TextBox OpCommentBox = (TextBox)gvRowParent.FindControl("OpCommentBox");
            DropDownList EmployeeAddList = (DropDownList)gvRowParent.FindControl("EmployeeAddList");
            TextBox OrderBox = (TextBox)gvRowParent.FindControl("RequestedOrderBox");
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

            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void AddSub_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
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

            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }


        protected void CancelAddSub_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;

            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void AddNowSub_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            string JobItemID = gvRowParent.Cells[1].Text;
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

            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }


        protected void AddAlloc_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            
            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void CancelAddAlloc_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;

            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
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

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
            string JobSetupID="0";
            string SetupID = "";
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            GridView LogHoursGrid = (GridView)Row.FindControl("LogHoursGrid");
            GridView SetupFixtureOrders = (GridView)Row.FindControl("SetupFixtureOrders");

            if (Row.RowIndex > -1)
            {
                JobSetupID = Grid.DataKeys[Row.RowIndex].Values[0].ToString();
                SetupID = Grid.DataKeys[Row.RowIndex].Values[3].ToString();
                LogHoursGridSource.SelectCommand = "SELECT ProcessID, JobSetupID, Name, Hours, QuantityIn, QuantityOut, EmployeeID FROM LoggedHoursSummary WHERE JobSetupID = " + JobSetupID;
                LogHoursGrid.DataSource = LogHoursGridSource;
                LogHoursGrid.DataBind();


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
                    JobSetupID = Grid.DataKeys[Row.RowIndex].Values[0].ToString();
                    SetupID = Grid.DataKeys[Row.RowIndex].Values[3].ToString();

                    if (SetupID != "")
                    {
                        LogHoursGridSource.SelectCommand = "SELECT ProcessID, JobSetupID, Name, Hours, QuantityIn, QuantityOut, EmployeeID FROM LoggedHoursSummary WHERE JobSetupID = " + JobSetupID;
                        LogHoursGrid.DataSource = LogHoursGridSource;
                        LogHoursGrid.DataBind();
                        SetupFixtureSource.SelectCommand = "SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName], Location, FixtureRevID, Note FROM [FixtureOrdersbySetup] WHERE [SetupUsingID] =" + SetupID;
                        SetupFixtureOrders.DataSource = SetupFixtureSource;
                        SetupFixtureOrders.DataBind();
                    }
                }
            }
        }

        protected void LogNewNow_Command(object sender, CommandEventArgs e)
        {
            string MonseesConnectionString;

            double HoursValue;
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
                gvChildChild.DataBind();
            }
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvChild).Parent.Parent.Parent.Parent as GridView;
            BindChildgvwChildLog(gvChild.DataKeys[gvwChild.RowIndex].Values[0].ToString(), gvChildChild);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;



        }

        protected void LogNew_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            GridViewRow LogHoursGrid = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            DropDownList dpl = (DropDownList)LogHoursGrid.FindControl("EmployeeList2");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();
               
            }
            //BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;

        }

        protected void CancelLog_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;

        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {
           
           
            string updatequery = "UPDATE [Job] SET Clear = 1 WHERE JobID = " + jobId;
            
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();

            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(updatequery, con);
            comm2.ExecuteNonQuery();
            con.Close();
            SqlDataSource2.SelectCommand = "SELECT [JobID], [JobNumber], [IsOpen], [CreateDate], [ClosedDate], [CompanyName], [ContactName], Clear FROM [JobSummary] WHERE JobID=" + jobId;
            ListView1.DataSource = SqlDataSource2;
            ListView1.DataBind();
        }

        protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
        {
             if (!((CheckBox)e.Item.FindControl("ClearCheck")).Checked)
            {
                ClearJob.Visible = true;
            }
             else
             {
                 ClearJob.Visible = false;
             }
        }

        protected void StockMatlGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView5 = (GridView)gvRowParent.FindControl("GridView5");
            GridView StockMaterialGrid = (GridView)sender;
            ListView ListView2 = (ListView)gvRowParent.FindControl("ListView2");
            ListViewItem ListItem = ListView2.Items[0];            

            string JobItemID = GridView1.DataKeys[gvRowParent.RowIndex].Values[0].ToString();
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

            BindChildgvwChildViewGrid5(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView5);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;

        }

        protected void QuoteDimList_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            GridViewRow gvrow = (GridViewRow)(((System.Web.UI.WebControls.DropDownList)sender).Parent.Parent);
            DropDownList SizeListDL = (DropDownList)gvrow.FindControl("QuoteSizeList");

            if (SizeListDL != null)
            {

                SizeListDL.DataSource = SizeList.FindAll(Size => Size.MaterialDimID.Equals(Convert.ToInt32(string.IsNullOrEmpty(((DropDownList)gvrow.FindControl("QuoteDimList")).SelectedValue.Trim()) ? "0" : ((DropDownList)gvrow.FindControl("QuoteDimList")).SelectedValue.Trim())));
                SizeListDL.DataBind();
                string val = ((HiddenField)gvrow.FindControl("hdQuoteSize")).Value.Trim();
                SizeListDL.SelectedValue = PreventUnlistedValueError(SizeListDL, val);
                KeepExpandedGrid4((GridView)gvrow.Parent.Parent, sender);
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
                GridViewRow lvitemParent = (GridViewRow)item.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent;

                index = 0;
                HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
                object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
                ExpandCollapseIndependent(button);
                div.Visible = true;

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

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                    {
                        GridView1.Rows[i].Font.Bold = true;

                    }


                }


            }
        }

        protected void StockRetGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent as GridViewRow;
            
            GridViewRow gvRow;
            GridView gv = (GridView)sender;
            MultiView MatAllocMulti = (MultiView)((GridViewRow)((GridView)sender).Parent.Parent.Parent).FindControl("MatAllocMulti");
            View MatlRetView = (View)((GridViewRow)((GridView)sender).Parent.Parent.Parent).FindControl("StockRetView");
            gvRow = gv.Rows[Convert.ToInt32(e.CommandArgument)];
            string MaterialID = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
            GridView StockRetGrid = ((GridViewRow)((GridView)sender).Parent.Parent.Parent).FindControl("StockRetGrid") as GridView;
            RetMatlSource.SelectCommand = "SELECT MatPriceID, [MaterialName], [Dimension], [Diameter] As D, [Height] As H, [Width] As W, [Length] As L, [quantity] As Qty, [MatlAllocationID] FROM [StockMaterialSummary] WHERE [MatlAllocationID] =" + (string.IsNullOrEmpty(MaterialID) ? "0" : MaterialID);

            StockRetGrid.DataSource = RetMatlSource;
            StockRetGrid.DataBind();
            MatAllocMulti.SetActiveView(MatlRetView);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                    {
                        GridView1.Rows[i].Font.Bold = true;

                    }


                }


            }
            BindChildgvwChildViewStockRet(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), gv);
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void StockRetGrid_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow ParentRow;
            GridViewRow gvRow;
            GridView gv = (GridView)sender;
            MultiView MatAllocMulti = (MultiView)((GridView)sender).Parent.Parent;
           
            ParentRow = (GridViewRow)((GridView)sender).Parent.Parent.Parent.Parent.Parent;
            GridView GridView5 = (GridView)ParentRow.FindControl("GridView5");
            View MatlRetView = (View)((GridViewRow)((GridView)sender).Parent.Parent.Parent.Parent.Parent).FindControl("ExistAllocView");
            string MatPriceID = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
            gvRow = gv.Rows[Convert.ToInt32(e.CommandArgument)];
            string Len = ((TextBox)gvRow.FindControl("LengthBox")).Text;
            string Qty = ((TextBox)gvRow.FindControl("QtyBox")).Text;
            string Loc = ((TextBox)gvRow.FindControl("LocBox")).Text;
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            con.Open();
            SqlCommand com = new SqlCommand("ReturnMaterial", con);
            com.CommandType = CommandType.StoredProcedure;
            string JobItemID = GridView1.DataKeys[ParentRow.RowIndex].Values[0].ToString();
            com.Parameters.AddWithValue("@MatPriceID", Convert.ToInt32(MatPriceID));
            com.Parameters.AddWithValue("@Length", Convert.ToDouble(Len));
            com.Parameters.AddWithValue("@Qty", Convert.ToInt32(Qty));
            com.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(JobItemID));
            com.Parameters.AddWithValue("@Loc", Loc);
            com.ExecuteNonQuery();
            con.Dispose();
            con.Close();

            MatAllocMulti.SetActiveView(MatlRetView);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                    {
                        GridView1.Rows[i].Font.Bold = true;

                    }


                }


            }
            BindChildgvwChildViewGrid5(GridView1.DataKeys[ParentRow.RowIndex].Value.ToString(), GridView5);
            index = ParentRow.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void RevisionFixtureOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            HtmlGenericControl locdiv;
            HtmlGenericControl orderdiv;
            locdiv = (HtmlGenericControl)Row.FindControl("loclabeldiv");
            orderdiv = (HtmlGenericControl)Row.FindControl("loctextdiv");

            if (Row.RowIndex > -1)
            {
                if (String.IsNullOrEmpty(Grid.DataKeys[Row.RowIndex].Value.ToString()))
                {
                    locdiv.Visible = false;
                    orderdiv.Visible = true;
                }
                else
                {
                    locdiv.Visible = true;
                    orderdiv.Visible = false;
                }

            }
        }

        protected void SetupFixtureOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            HtmlGenericControl locdiv;
            HtmlGenericControl orderdiv;
            locdiv = (HtmlGenericControl)Row.FindControl("loclabeldiv");
            orderdiv = (HtmlGenericControl)Row.FindControl("loctextdiv");

            if (Row.RowIndex > -1)
            {
                if (String.IsNullOrEmpty(Grid.DataKeys[Row.RowIndex].Value.ToString()))
                {
                    locdiv.Visible = false;
                    orderdiv.Visible = true;
                }
                else
                {
                    locdiv.Visible = true;
                    orderdiv.Visible = false;
                }

            }
        }

        protected void FixtureCloseButton_Click(object sender, EventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent as GridViewRow;
            GridView gvParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent2 = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridViewRow gvRowParent3 = gvRowParent2.Parent.Parent.Parent.Parent as GridViewRow;


            string RevID = gvParent.DataKeys[gvRowParent.RowIndex].Values[1].ToString();
            TextBox Fixtloc = (TextBox)gvRowParent.FindControl("fixloctext");
            TextBox FixtNote = (TextBox)gvRowParent.FindControl("fixnotetext");
            string location = Fixtloc.Text;
            string sqlstring = "INSERT INTO FixtureInventory (RevisionID, Location, Note) VALUES (@FixtRevID, @location, @note)";

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand(sqlstring, con);

            com.Parameters.AddWithValue("@FixtRevID", Convert.ToInt32(RevID));
            com.Parameters.AddWithValue("@location", Fixtloc.Text);

            com.Parameters.AddWithValue("@note", FixtNote);

            con.Open();
            com.CommandType = CommandType.Text;
            com.ExecuteNonQuery();

            con.Close();


            gvParent.DataBind();

            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent3.Cells[2].Text + "').style.display = 'inline';", true);
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                    {
                        GridView1.Rows[i].Font.Bold = true;

                    }


                }


            }
        }

        protected void FixtureCloseButton2_Click(object sender, EventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
            GridView gvParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent2 = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            GridViewRow gvRowParent3 = gvRowParent2.Parent.Parent.Parent.Parent as GridViewRow;


            string RevID = gvParent.DataKeys[gvRowParent.RowIndex].Values[1].ToString();
            TextBox Fixtloc = (TextBox)gvRowParent.FindControl("fixloctext");
            TextBox FixtNote = (TextBox)gvRowParent.FindControl("fixnotetext");
            string location = Fixtloc.Text;
            string sqlstring = "INSERT INTO FixtureInventory (RevisionID, Location, Note) VALUES (@FixtRevID, @location, @note)";

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand("CloseFixture", con);

            com.Parameters.AddWithValue("@FixtRevID", Convert.ToInt32(RevID));
            com.Parameters.AddWithValue("@location", Fixtloc.Text);

            com.Parameters.AddWithValue("@note", FixtNote);

            con.Open();
            com.CommandType = CommandType.StoredProcedure;
            com.ExecuteNonQuery();

            con.Close();


            gvParent.DataBind();

            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)GridView1.Rows[index].FindControl("div1");
            object button = (object)GridView1.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {


                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                }

                if (Convert.ToInt32(((HiddenField)GridView1.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                }


                if (((HiddenField)GridView1.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                }

                if (((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value != "")
                {
                    string hot = ((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value;
                    if (Convert.ToBoolean(((HiddenField)GridView1.Rows[i].FindControl("Hot")).Value))
                    {
                        GridView1.Rows[i].Font.Bold = true;

                    }


                }


            }
        }
    }
}
 

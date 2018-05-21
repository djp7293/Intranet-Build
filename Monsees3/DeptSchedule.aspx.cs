using System;
using System.IO;
using System.Collections;
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
using System.Drawing;
using Monsees.Security;
using Monsees.Pages;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using System.Collections.Generic;
using Monsees.Services;
using System.Web.Mvc;
using System.Web.Script.Serialization;



namespace Monsees
{
    public partial class _Default_Dept : DataPage
    {
        private string MonseesConnectionString;        
        private Int32 index;
        private string[] EmployeeLoginName;
        private Int32 EmployeeID;
        public List<EmployeeModel> Employees { get; set; }
        public List<OperationListModel> OperationList { get; set; }
       // public DataTable dtCarry;
        public List<MachineModel> MachineList { get; set; }
        
        //public DataTable dt;
       

        protected void Page_Load(object sender, EventArgs e)
        {
           
            //MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
           

            //if (!IsPostBack)
            //{
            //    EmployeeLoginName = User.Identity.Name.Split('\\');

            //    string sqlstring = "Select [EmployeeID], [Name] FROM [Employees] WHERE [WindowsAuthLogin] = 'jspurling';";                
            //    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            //    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            //    System.Data.SqlClient.SqlDataReader reader;
            //    con.Open();

            //    reader = comm.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        EmployeeID = Convert.ToInt32(reader["EmployeeID"].ToString());
            //    }
            //    con.Close();

                //string controlstring;


                //DropDownList mycontrol;
                //string[] fieldarray = new string[5];

                //for (int y = 0; y < fieldarray.Length; y++)
                //{
                //    controlstring = "Op" + y.ToString();
                //    mycontrol = Master.FindControl("bodyContent").FindControl(controlstring) as DropDownList;
                //    fieldarray[y] = mycontrol.SelectedValue.ToString();
                //}

                //using (SqlConnection cnx = new SqlConnection(MonseesConnectionString))
                //using (SqlCommand cmd = new SqlCommand("DeptSchedule", cnx) { CommandType = CommandType.StoredProcedure })
                //using (dt = new DataTable())
                //{
                //    cmd.CommandTimeout = 120;
                //    cmd.Parameters.AddWithValue("@filter1", "Multiaxis");
                //    cmd.Parameters.AddWithValue("@filter2", "Blank");
                //    cmd.Parameters.AddWithValue("@filter3", "Blank");
                //    cmd.Parameters.AddWithValue("@filter4", "Blank");
                //    cmd.Parameters.AddWithValue("@filter5", "Blank");
                //    try
                //    {
                //        cnx.Open();
                //        dt.Load(cmd.ExecuteReader());
                //    }
                //    catch (Exception ex)
                //    {
                //        throw new Exception("Error executing procedure.", ex);
                //    }
                //}

                //Session["Data"] = dt;
                //Session["DataLoaded"] = "yes";




            //}
            //else
            //{
            //    ReloadData();
            //}
        }


        [WebMethod]
        public static string GetColumns()
        {
            String daresult = null;
            List<string> Columns = new List<string>();

            Columns.Add("Job #");
            Columns.Add("Lot #");
            Columns.Add("Next Delivry");
            Columns.Add("CC");
            Columns.Add("RevID");
            Columns.Add("Part #");
            Columns.Add("Rev #");
            Columns.Add("Description");
            Columns.Add("PM");
            Columns.Add("Qty");
            Columns.Add("Late Start");
            Columns.Add("Matl");
            Columns.Add("AreFixtures");
            Columns.Add("Avail.");
            Columns.Add("ITAR");
            Columns.Add("Hot");
            Columns.Add("NewRenew");
            Columns.Add("NewPart");
            Columns.Add("Multiaxis");
            Columns.Add("Blank");
            Columns.Add("Blank");
            Columns.Add("Blank");
            Columns.Add("Blank");

            JavaScriptSerializer json = new JavaScriptSerializer();

            daresult = json.Serialize(Columns);
            return daresult;

        }

        [WebMethod]
        public static string LoadData(string fil1, string fil2, string fil3, string fil4, string fil5)
        {
            string controlstring;
            DropDownList mycontrol;
            DataTable dt1;
            DataSet ds = new DataSet();
            String daresult = null;
            string[] fieldarray = new string[5];
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            //for (int y = 0; y < fieldarray.Length; y++)
            //{
            //    controlstring = "Op" + y.ToString();
            //    mycontrol = Master.FindControl("bodyContent").FindControl(controlstring) as DropDownList;
            //    fieldarray[y] = mycontrol.SelectedValue.ToString();
            //}

            using (SqlConnection cnx = new SqlConnection(MonseesConnectionString))
            using (SqlCommand cmd = new SqlCommand("DeptSchedule", cnx) { CommandType = CommandType.StoredProcedure })
            using (dt1 = new DataTable())
            {
                cmd.CommandTimeout = 120;
                cmd.Parameters.AddWithValue("@filter1", fil1);
                cmd.Parameters.AddWithValue("@filter2", fil2);
                cmd.Parameters.AddWithValue("@filter3", fil3);
                cmd.Parameters.AddWithValue("@filter4", fil4);
                cmd.Parameters.AddWithValue("@filter5", fil5);
                try
                {
                    cnx.Open();
                    dt1.Load(cmd.ExecuteReader());
                }
                catch (Exception ex)
                {
                    throw new Exception("Error executing procedure.", ex);
                }
            }



            List<DeptSchedule> DeptSchedList = new List<DeptSchedule>();

            DeptSchedList = (from DataRow row in dt1.Rows
                             select new DeptSchedule
                             {
                                 
                                 JobItemID = Convert.ToInt32(row["Lot #"]),
                                 JobNumber = row["Job #"].ToString(),
                                 NextDelivery = ((DateTime)row["NxtDelivry"]).ToString("MM-dd-yyyy"),
                                 CustCode = row["CAbbr"].ToString(),
                                 RevisionID = Convert.ToInt32(row["RevisionID"]),
                                 PartNumber = row["PartNumber"].ToString(),
                                 RevisionNumber = row["Rev #"].ToString(),
                                 DrawingNumber = row["Description"].ToString(),
                                 PM = row["ProjectManager"].ToString(),
                                 qty = Convert.ToInt32(row["Qty"]),
                                 LateStart = ((DateTime)row["Late Start"]).ToString("MM-dd-yyyy"),
                                 MatlReady = row["MatlReady"].ToString(),
                                 AreFixtures = Convert.ToBoolean(row["AreFixtures"]),
                                 Available = ((DateTime)row["Available"]).ToString("MM-dd-yyyy"),
                                 ITAR = Convert.ToBoolean(row["ITAR"]),
                                 Hot = Convert.ToBoolean(row["Hot"]),
                                 NewRenew = Convert.ToInt32(row["NewRenew"]),
                                 NewPart = Convert.ToInt32(row["NewPart"]),
                                 filter1 = String.Format("{0:0.0}", row[fil1] == DBNull.Value ? 0 : row[fil1]),
                                 filter2 = String.Format("{0:0.0}", fil2 == "Blank" ? row[fil1] == DBNull.Value ? 0 : row[fil1] : row[fil2] == DBNull.Value ? 0 : row[fil2]),
                                 filter3 = String.Format("{0:0.0}", fil3 == "Blank" ? row[fil1] == DBNull.Value ? 0 : row[fil1] : row[fil3] == DBNull.Value ? 0 : row[fil3]),
                                 filter4 = String.Format("{0:0.0}", fil4 == "Blank" ? row[fil1] == DBNull.Value ? 0 : row[fil1] : row[fil4] == DBNull.Value ? 0 : row[fil4]),
                                 filter5 = String.Format("{0:0.0}", fil5 == "Blank" ? row[fil1] == DBNull.Value ? 0 : row[fil1] : row[fil5] == DBNull.Value ? 0 : row[fil5])
                             }).ToList();

            JavaScriptSerializer json = new JavaScriptSerializer();

            daresult = json.Serialize(DeptSchedList);
            return daresult;
           
        }

        [WebMethod]
        public static string LoadControl(string id)
        {
            Page page = new Page();
            Control control = page.LoadControl("~/NestedActiveJobsCtrl.ascx");
            NestedActiveJobsCtrl UserCtrl = (NestedActiveJobsCtrl)control;
            UserCtrl.SetJobItem(id);
            HtmlForm form = new HtmlForm();
            form.Controls.Add(UserCtrl);            
            page.Controls.Add(form);
            StringWriter writer = new StringWriter();
            HttpContext.Current.Server.Execute(page, writer, false);
            return writer.ToString();
        }

        //protected void restore_columns()
        //{
        //    DataControlFieldCollection columns = (DataControlFieldCollection)Cache["cols"];
        //    ProductionViewGrid.Columns.Clear(); // gvSchluessel is the GridView
        //    foreach (DataControlField field in columns)
        //    {
        //        ProductionViewGrid.Columns.Add(field);
        //        if (field.GetType() == typeof(TemplateField))
        //        {
        //            TemplateField tf = (TemplateField)field;
        //            int i = ProductionViewGrid.Columns.IndexOf(tf);
        //            if (tf.HeaderTemplate != null)
        //            {
        //                tf.HeaderTemplate.InstantiateIn(ProductionViewGrid.HeaderRow.Cells[i]);
        //            }
        //            foreach (GridViewRow row in ProductionViewGrid.Rows)
        //            {
        //                if (tf.ItemTemplate != null)
        //                {
        //                    tf.ItemTemplate.InstantiateIn(row.Cells[i]);
        //                }
        //            }
        //            if (tf.FooterTemplate != null)
        //            {
        //                tf.FooterTemplate.InstantiateIn(ProductionViewGrid.FooterRow.Cells[i]);
        //            }
        //        }
        //    }
        //}

        //protected void Reload_click(object sender, EventArgs e)
        //{
        //    Session["DataLoaded"] = "no";
        //}

        //private void ReloadData()
        //{               
        //    string controlstring;
        //    string textvalue;
        //    string textvaluelbl;
        //    string textvalueft;
        //    DataTable dt;
        //    DataTable Expanded;
        //    DropDownList mycontrol;
        //    string[] fieldarray = new string[5];                
                    
        //    for (int y = 0; y < fieldarray.Length; y++)
        //    {
        //        controlstring = "Op" + y.ToString();
        //        mycontrol = Master.FindControl("bodyContent").FindControl(controlstring) as DropDownList;
        //        fieldarray[y] = mycontrol.SelectedValue.ToString();
        //    }

        //    if ((string)Session["DataLoaded"] == "no")
        //    {                    
        //        using (SqlConnection cnx = new SqlConnection(MonseesConnectionString))
        //        using (SqlCommand cmd = new SqlCommand("DeptSchedule", cnx) { CommandType = CommandType.StoredProcedure })
        //        using (dt = new DataTable())
        //        {
        //            cmd.CommandTimeout = 120;
        //            cmd.Parameters.AddWithValue("@filter1", fieldarray[0]);
        //            cmd.Parameters.AddWithValue("@filter2", fieldarray[1]);
        //            cmd.Parameters.AddWithValue("@filter3", fieldarray[2]);
        //            cmd.Parameters.AddWithValue("@filter4", fieldarray[3]);
        //            cmd.Parameters.AddWithValue("@filter5", fieldarray[4]);
        //            try
        //            {
        //                cnx.Open();
        //                dt.Load(cmd.ExecuteReader());
        //            }
        //            catch (Exception ex)
        //            {
        //                throw new Exception("Error executing procedure.", ex);
        //            }
        //        }
                
        //        Session["Data"] = dt;
        //        Session["DataLoaded"] = "yes";
        //    }
        //    else
        //    {
        //        dt = (DataTable)Session["Data"];
        //    }

        //    Int32 counter = 0;

        //    for (int x = 0; x < fieldarray.Length; x++)
        //    {
        //        //Declare the bound field and allocate memory for the bound field.
        //        if ((fieldarray[x] != "Blank") && (fieldarray[x] != "%"))
        //        {
        //            textvalue = fieldarray[x];
        //            textvaluelbl = textvalue + "lbl";
        //            textvalueft = textvalue + "ft";

        //            TemplateField tfield = new TemplateField();
        //            tfield.ItemTemplate = new myLabel(DataControlRowType.DataRow, textvaluelbl, textvalue);
        //            tfield.FooterTemplate = new myLabel(DataControlRowType.Footer, textvalueft, dt.Compute("sum([" + fieldarray[x] + "])", "").ToString());

        //            tfield.HeaderText = fieldarray[x];                    
        //            tfield.SortExpression = fieldarray[x];

        //            //Add the newly created bound field to the GridView.
        //            ProductionViewGrid.Columns.Insert(22 + counter, tfield);
        //            counter = counter + 1;
        //        }
        //    }

        //    ProductionViewGrid.DataSource = dt;
        //    ProductionViewGrid.DataBind();            
        //}

        //protected void LoadThumbnail(HttpContext context)
        //{
        //    string imageid = context.Request.QueryString["ImID"];
        //    if (imageid == null || imageid == "")
        //    {
        //        //Set a default imageID
        //        imageid = "1";
        //    }
        //    SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
        //    connection.Open();
        //    SqlCommand command = new SqlCommand("select SetupImage from SetupImage where ImageID=" + imageid, connection);
        //    SqlDataReader dr = command.ExecuteReader();
        //    dr.Read();

        //    Stream str = new MemoryStream((Byte[])dr[0]);

        //    Bitmap loBMP = new Bitmap(str);
        //    Bitmap bmpOut = new Bitmap(100, 100);

        //    Graphics g = Graphics.FromImage(bmpOut);
        //    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //    g.FillRectangle(Brushes.White, 0, 0, 100, 100);
        //    g.DrawImage(loBMP, 0, 0, 100, 100);

        //    MemoryStream ms = new MemoryStream();
        //    bmpOut.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
        //    byte[] bmpBytes = ms.GetBuffer();
        //    bmpOut.Dispose();
        //    ms.Close();
        //    context.Response.BinaryWrite(bmpBytes);
        //    connection.Close();
        //    context.Response.End();

        //}

        //protected void ProductionViewGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewRenew")).Value.ToString()) == 1)
        //        {
        //            e.Row.BackColor = System.Drawing.Color.FromName("#fbffb5");

        //        }

        //        if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewPart")).Value.ToString()) <= 1)
        //        {
        //            e.Row.BackColor = System.Drawing.Color.FromName("#ffad5c");

        //        }


        //        if (((HiddenField)e.Row.FindControl("CAbbr")).Value.ToString() == "MG")
        //        {
        //            e.Row.BackColor = System.Drawing.Color.FromName("#8CFF8C");

        //        }


        //        if (((HiddenField)e.Row.FindControl("Hot")).Value != "")
        //        {
        //            string hot = ((HiddenField)e.Row.FindControl("Hot")).Value;
        //            if (Convert.ToBoolean(((HiddenField)e.Row.FindControl("Hot")).Value))
        //            {
        //                e.Row.Font.Bold = true;

        //            }
        //        }
        //    }
        //}

        //protected void ProductionViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    //GridView cells are 0 based
        //    GridViewRow gvRow;
        //    string LotID;

        //    string command_name = e.CommandName;
            
        //    if ((command_name == "ViewOps") || (command_name == "GetFile") || (command_name == "PartHistory") || (command_name == "Login") || (command_name == "Inspection") || (command_name == "Fixturing"))
        //    {
        //        Int32 totrows = ProductionViewGrid.Rows.Count;
        //        index = Convert.ToInt32(e.CommandArgument);
        //        gvRow = ProductionViewGrid.Rows[index];
        //        LotID = gvRow.Cells[2].Text;
                
        //        switch (e.CommandName)
        //        {
        //            case "Login":                                          
        //                Response.Write("<script type='text/javascript'>window.open('Login.aspx?JobItemID=" + LotID + "&EmpID=" + EmployeeID + "','_blank');</script>");
        //                Formatting();

        //                break;
        //            case "ViewOps":                        
        //                Response.Write("<script type='text/javascript'>window.open('ViewOps.aspx?JobItemID=" + LotID + "','_blank');</script>");
        //                Formatting();
        //                break;

        //            case "Inspection":
        //                string pageName = (Page.IsInMappedRole("Inspection")) ? "InspectionReport.aspx" : "InspectionReportPrint.aspx";
        //                Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobItemID=" + LotID + "');</script>");
        //                Formatting();
        //                break;

        //            case "PartHistory":
        //                string DetailID = "1";
        //                string sqlstring = "Select [DetailID] from [Job Item] where [JobItemID] = " + LotID;                        
        //                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
        //                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
        //                System.Data.SqlClient.SqlDataReader reader;
        //                con.Open();

        //                reader = comm.ExecuteReader();
        //                while (reader.Read())
        //                {
        //                    DetailID = reader["DetailID"].ToString();
        //                }
        //                con.Close();

        //                Response.Write("<script type='text/javascript'>window.open('PartHistory.aspx?DetailID=" + DetailID + "','_blank');</script>");
        //                Formatting();
        //                break;

        //            case "GetFile":
        //                String PartNumber;
        //                String RevNumber;                        
        //                string RevID = ProductionViewGrid.DataKeys[index].Values[1].ToString();
        //                PartNumber = gvRow.Cells[5].Text;
        //                RevNumber = gvRow.Cells[6].Text;
        //                Response.Redirect("pdfhandler.ashx?FileID=" + RevID + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);
        //                break;

        //            case "Fixturing":                       
        //                Response.Write("<script type='text/javascript'>window.open('Fixturing.aspx?JobItemID=" + LotID + "');</script>");
        //                Formatting();
        //                break;

        //            default:

        //                break;

        //        }
        //    }

        //}

        //private void GetData()
        //{
        //    this.UnitOfWork.Begin();
        //    InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
        //    MachineList = inspectionRepository.GetMachines();
        //    Employees = inspectionRepository.GetActiveEmployees();
        //    OperationList = inspectionRepository.GetOperations();
        //    this.UnitOfWork.End();
        //}

        //private void Formatting()
        //{
        //    //for (int i = 0; i < ProductionViewGrid.Rows.Count; i++)
        //    //{


        //    //    if (Convert.ToInt32(((HiddenField)ProductionViewGrid.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
        //    //    {
        //    //        ProductionViewGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

        //    //    }

        //    //    if (Convert.ToInt32(((HiddenField)ProductionViewGrid.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
        //    //    {
        //    //        ProductionViewGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

        //    //    }


        //    //    if (((HiddenField)ProductionViewGrid.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
        //    //    {
        //    //        ProductionViewGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

        //    //    }

        //    //    if (((HiddenField)ProductionViewGrid.Rows[i].FindControl("Hot")).Value != "")
        //    //    {
        //    //        string hot = ((HiddenField)ProductionViewGrid.Rows[i].FindControl("Hot")).Value;
        //    //        if (Convert.ToBoolean(((HiddenField)ProductionViewGrid.Rows[i].FindControl("Hot")).Value))
        //    //        {
        //    //            ProductionViewGrid.Rows[i].Font.Bold = true;

        //    //        }


        //    //    }
        //    //}
        //}

        //[WebMethod]
        //protected void ExpandCollapse(object sender, EventArgs e)
        //{
        //    GridViewRow ProdGrid = (GridViewRow)((Button)sender).Parent.Parent;
        //    ExpColBody(ProdGrid);
        //}

        //protected void ExpandCollapseIndependent(object sender)
        //{
        //    GridViewRow ProdGrid = (GridViewRow)((Button)sender).Parent.Parent;
        //    ExpColBody(ProdGrid);
        //}


        //private void ExpColBody(GridViewRow ProdGrid)
        //{
        //    GridView Prod = (GridView)ProdGrid.Parent.Parent;
        //    if (ProdGrid.RowType == DataControlRowType.DataRow)
        //    {
        //        index = ProdGrid.RowIndex;
        //        string JobItemID = ProductionViewGrid.DataKeys[index].Values[0].ToString();
        //        string DetailID = "0";
        //        string RevisionID = "0";
        //        PlaceHolder Holder = (PlaceHolder)ProdGrid.FindControl("holder");
                
        //        ListView PartSummaryView = ProdGrid.FindControl("PartSummaryView") as ListView;
        //        GridView DeliveryViewGrid = ProdGrid.FindControl("DeliveryViewGrid") as GridView;
        //        GridView GridView2 = ProdGrid.FindControl("GridView2") as GridView;
        //        GridView GridView3 = ProdGrid.FindControl("GridView3") as GridView;
        //        GridView MatlQuoteReqView = ProdGrid.FindControl("MatlQuoteReqView") as GridView;
        //        GridView MatlOrderView = ProdGrid.FindControl("MatlOrderView") as GridView;
        //        GridView GridView6 = ProdGrid.FindControl("RevisionFixtureOrders") as GridView;
        //        GridView CertGrid = ProdGrid.FindControl("CertGrid") as GridView;
        //        GridView PurchasedComponentsView = ProdGrid.FindControl("PurchasedComponentsView") as GridView;
        //        GridView GridView9 = ProdGrid.FindControl("GridView9") as GridView;
        //        GridView CARView = ProdGrid.FindControl("CARView") as GridView;

        //        Control NestedCtrl = LoadControl("~/NestedLoginCtrl.ascx");
        //        NestedCtrl.ID = "nestedctrl" + JobItemID;


        //        UserControl.NestedLoginCtrl CtrlVariableCont = (UserControl.NestedLoginCtrl)NestedCtrl;
        //        CtrlVariableCont.JobItemID = JobItemID;

        //        Holder.Controls.Add(NestedCtrl);

                

        //        MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

        //        DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
        //        DeliveryViewGrid.DataBind();

        //        PartSummaryDataSource.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

        //        PartSummaryView.DataSource = PartSummaryDataSource;
        //        PartSummaryView.DataBind();

        //        SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID, SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

                
        //        GridView3.DataSource = SqlDataSource5;
        //        GridView3.DataBind();

        //        MatlQuoteReqDataSource.SelectCommand = "SELECT [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

        //        MatlQuoteReqView.DataSource = MatlQuoteReqDataSource;
        //        MatlQuoteReqView.DataBind();

        //        MatlOrderDataSource.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], [MatPriceID], pct, MatlAllocationID FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

        //        MatlOrderView.DataSource = MatlOrderDataSource;
        //        MatlOrderView.DataBind();


        //        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
        //        string sqlstring = "Select DetailID, [Active Version] FROM [Job Item] WHERE [JobItemID] = " + JobItemID + ";";
        //        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
        //        System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
        //        System.Data.SqlClient.SqlDataReader reader;
        //        con.Open();
                
        //        reader = comm.ExecuteReader();

        //        while (reader.Read())
        //        {
        //            DetailID = reader["DetailID"].ToString();
        //            RevisionID = reader["Active Version"].ToString();
        //        }

        //        con.Close();

        //        SqlDataSource8.SelectCommand = "SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName], Location, Note, OperationName, FixtureRevID FROM [FixtureOrders] WHERE [DetailUsingID] =" + DetailID;

        //        GridView6.DataSource = SqlDataSource8;
        //        GridView6.DataBind();

        //        SqlDataSourceCert.SelectCommand = "SELECT [CertCompReqd], [MatlCertReqd], [PlateCertReqd], [SerializationReqd] FROM Version WHERE RevisionID = " + RevisionID;

        //        CertGrid.DataSource = SqlDataSourceCert;
        //        CertGrid.DataBind();

        //        PurchasedComponentsSource.SelectCommand = "SELECT [PartNumber], [Revision Number] AS Revision_Number, [DrawingNumber], [PerAssembly], [NextOp] FROM [AssemblyItemsSummary] WHERE [AssemblyLot] = " + JobItemID;

        //        PurchasedComponentsView.DataSource = PurchasedComponentsSource;
        //        PurchasedComponentsView.DataBind();

        //        SqlDataSource11.SelectCommand = "SELECT [DrawingNumber], [PerAssy], [ItemNumber], [VendorName], [Each], [Weblink] FROM [BOMItemSummary] WHERE [AssyRevisionID] = " + RevisionID;

        //        GridView9.DataSource = SqlDataSource11;
        //        GridView9.DataBind();

        //        SqlDataSource12.SelectCommand = "SELECT * FROM CorrectiveActionView WHERE [DetailID] = " + DetailID;

        //        CARView.DataSource = SqlDataSource12;
        //        CARView.DataBind();

        //        Formatting();

        //        HtmlGenericControl div = (HtmlGenericControl)ProdGrid.FindControl("div1");
        //        Button ExpCol = (Button)ProdGrid.FindControl("ExpColMain");


        //        if (div.Visible == false)
        //        {
        //            div.Visible = true;
        //            ExpCol.Text = "-";

        //        }
        //        else {
        //            div.Visible = false;
        //            ExpCol.Text = "+";
        //        }

        //    }
        //}

        

        //protected string PreventUnlistedValueError(DropDownList li, string val)
        //{
        //    if (li.Items.FindByValue(val) == null)
        //    {
        //        if (val == "") val = "0";
        //        ListItem lit = new ListItem();
        //        lit.Text = val;
        //        lit.Value = val;
        //        li.Items.Insert(li.Items.Count, lit);                
        //    }
        //    return val;
        //}

        //protected void KeepExpanded(System.Web.UI.WebControls.GridView gvwChild, object sender)
        //{
        //    GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent as GridViewRow;
        //    index = gvRowParent.RowIndex;
        //    HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
        //    object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
        //    ExpandCollapseIndependent(button);
        //    div.Visible = true;
        //    BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
        //    Formatting();
        //}

        //private void BindChildgvwChildView(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        //{
        //    string JobItemID = jobitemId;
        //    SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID, SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";
        //    gvChild.DataSource = SqlDataSource4;
        //    gvChild.DataBind();
        //}

       
        

        //protected void OrderFixture_Click(object sender, CommandEventArgs e)
        //{
        //    int JobSetupID = Convert.ToInt32(e.CommandArgument.ToString());
        //    string JobItemID="0";
        //    string SetupID="0";
        //    string sqlstring = "Select [JobItemID], [SetupID] FROM [JobSetup] WHERE [JobSetupID] = " + JobSetupID + ";";
        //    System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
        //    System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
        //    System.Data.SqlClient.SqlDataReader reader;
        //    con.Open();

        //    reader = comm.ExecuteReader();

        //    while (reader.Read())
        //    {
        //        JobItemID = reader["JobItemID"].ToString();
        //        SetupID = reader["SetupID"].ToString();
        //    }
        //    con.Close();

        //    Response.Write("<script type='text/javascript'>window.open('AddFixture.aspx?SourceLot=" + JobItemID + "&SourceSetup=" + SetupID + "','_blank');</script>");
        //    Formatting();
        //    for (int i = 0; i < ProductionViewGrid.Rows.Count; i++)
        //    {
        //        if (ProductionViewGrid.DataKeys[i].Values[0].ToString() == JobItemID)
        //        {
        //            ExpandCollapseIndependent((GridViewRow)ProductionViewGrid.Rows[i]);
        //        }
        //    }
        //}



        

        //protected void reload_Click(object sender, EventArgs e)
        //{
        //    ProductionViewGrid.DataBind();
        //}

        //protected void CARView_RowCommand(object sender, GridViewCommandEventArgs e)
        //{

        //    GridViewRow gvRow;
        //    GridView gv;
        //    string CARID;


        //    switch (e.CommandName)
        //    {
        //        case "ViewCAR":
        //            gv = (GridView)sender;
        //            gvRow = gv.Rows[Convert.ToInt32(e.CommandArgument)];
        //            CARID = gvRow.Cells[0].Text;
        //            //Check to see if user is already logged in
        //            string pageName = (Page.IsInMappedRole("Inspection")) ? "CARComplete.aspx" : "CARComplete.aspx";
        //            Response.Write("<script type='text/javascript'>window.open('" + pageName + "?id=" + CARID + "');</script>");
        //            break;

        //        default:
        //            break;
        //    }
        //    Formatting();
        //}

        //private void BindChildgvwChildViewGrid5(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        //{
        //    string JobItemID = jobitemId;
        //    MatlOrderDataSource.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], pct, MatPriceID, MatlAllocationID FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

        //    gvChild.DataSource = MatlOrderDataSource;
        //    gvChild.DataBind();
        //}

        //private void BindChildgvwChildViewStockRet(string MaterialID, System.Web.UI.WebControls.GridView gvChild)
        //{

        //    RetMatlSource.SelectCommand = "SELECT MatPriceID, [MaterialName], [Dimension], [D], [H], [W], [L], [Qty] FROM [JobItemMatlPurchaseSummary] WHERE [MatPriceID] =" + (string.IsNullOrEmpty(MaterialID) ? "0" : MaterialID);

        //    gvChild.DataSource = MatlOrderDataSource;
        //    gvChild.DataBind();
        //}

        //protected void StockRetGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent as GridViewRow;
        //    index = gvRowParent.RowIndex;
        //    GridViewRow gvRow;
        //    GridView gv = (GridView)sender;
        //    MultiView MatAllocMulti = (MultiView)((GridViewRow)((GridView)sender).Parent.Parent.Parent).FindControl("MatAllocMulti");
        //    View MatlRetView = (View)((GridViewRow)((GridView)sender).Parent.Parent.Parent).FindControl("StockRetView");
        //    gvRow = gv.Rows[Convert.ToInt32(e.CommandArgument)];
        //    string MaterialID = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
        //    GridView StockRetGrid = ((GridViewRow)((GridView)sender).Parent.Parent.Parent).FindControl("StockRetGrid") as GridView;
        //    RetMatlSource.SelectCommand = "SELECT MatPriceID, [MaterialName], [Dimension], [D], [H], [W], [L], [Qty] FROM [JobItemMatlPurchaseSummary] WHERE [MatlAllocationID] =" + (string.IsNullOrEmpty(MaterialID) ? "0" : MaterialID);

        //    StockRetGrid.DataSource = RetMatlSource;
        //    StockRetGrid.DataBind();
        //    MatAllocMulti.SetActiveView(MatlRetView);
        //    Formatting();
        //    BindChildgvwChildViewStockRet(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), gv);
        //    HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
        //    object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
        //    ExpandCollapseIndependent(button);
        //    div.Visible = true;
        //}

        //protected void StockRetGrid_RowCommand1(object sender, GridViewCommandEventArgs e)
        //{
        //    GridViewRow ParentRow;
        //    GridViewRow gvRow;
        //    GridView gv = (GridView)sender;
        //    MultiView MatAllocMulti = (MultiView)((GridView)sender).Parent.Parent;

        //    ParentRow = (GridViewRow)((GridView)sender).Parent.Parent.Parent.Parent.Parent;
        //    index = ParentRow.RowIndex;
        //    GridView MatlOrderView = (GridView)ParentRow.FindControl("MatlOrderView");
        //    View MatlRetView = (View)((GridViewRow)((GridView)sender).Parent.Parent.Parent.Parent.Parent).FindControl("ExistAllocView");
        //    string MatPriceID = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
        //    gvRow = gv.Rows[Convert.ToInt32(e.CommandArgument)];
        //    string Len = ((TextBox)gvRow.FindControl("LengthBox")).Text;
        //    string Qty = ((TextBox)gvRow.FindControl("QtyBox")).Text;
        //    string Loc = ((TextBox)gvRow.FindControl("LocBox")).Text;
        //    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        //    con.Open();
        //    SqlCommand com = new SqlCommand("ReturnMaterial", con);
        //    com.CommandType = CommandType.StoredProcedure;
        //    string JobItemID = ProductionViewGrid.DataKeys[ParentRow.RowIndex].Values[0].ToString();
        //    com.Parameters.AddWithValue("@MatPriceID", Convert.ToInt32(MatPriceID));
        //    com.Parameters.AddWithValue("@Length", Convert.ToDouble(Len));
        //    com.Parameters.AddWithValue("@Qty", Convert.ToInt32(Qty));
        //    com.Parameters.AddWithValue("@Loc", Loc);
        //    com.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(JobItemID));

        //    com.ExecuteNonQuery();
        //    con.Dispose();
        //    con.Close();

        //    MatAllocMulti.SetActiveView(MatlRetView);

        //    Formatting();
        //    BindChildgvwChildViewGrid5(ProductionViewGrid.DataKeys[ParentRow.RowIndex].Value.ToString(), MatlOrderView);
        //    HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
        //    object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
        //    ExpandCollapseIndependent(button);
        //    div.Visible = true;
        //}

       

        //protected void RevisionFixtureOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    GridView Grid = (GridView)sender;
        //    Int32 i = Grid.EditIndex;
        //    GridViewRow Row = e.Row;
        //    HtmlGenericControl locdiv;
        //    HtmlGenericControl orderdiv;
        //    locdiv = (HtmlGenericControl)Row.FindControl("loclabeldiv");
        //    orderdiv = (HtmlGenericControl)Row.FindControl("loctextdiv");

        //    if (Row.RowIndex > -1)
        //    {
        //        if (String.IsNullOrEmpty(Grid.DataKeys[Row.RowIndex].Value.ToString()))
        //        {
        //            locdiv.Visible = false;
        //            orderdiv.Visible = true;
        //        }
        //        else
        //        {
        //            locdiv.Visible = true;
        //            orderdiv.Visible = false;
        //        }

        //    }
        //}

        //protected void FixtureCloseButton2_Click(object sender, EventArgs e)
        //{
        //    GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent as GridViewRow;
        //    index = gvRowParent.RowIndex;
        //    GridView gvParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridView;
        //    GridViewRow gvRowParent2 = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
        //    GridViewRow gvRowParent3 = gvRowParent2.Parent.Parent.Parent.Parent as GridViewRow;


        //    string RevID = gvParent.DataKeys[gvRowParent.RowIndex].Values[1].ToString();
        //    TextBox Fixtloc = (TextBox)gvRowParent.FindControl("fixloctext");
        //    TextBox FixtNote = (TextBox)gvRowParent.FindControl("fixnotetext");
        //    string location = Fixtloc.Text;
            
        //    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        //    SqlCommand com = new SqlCommand("CloseFixture", con);

        //    com.Parameters.AddWithValue("@FixtRevID", Convert.ToInt32(RevID));
        //    com.Parameters.AddWithValue("@location", Fixtloc.Text);

        //    com.Parameters.AddWithValue("@note", FixtNote.Text);

        //    con.Open();
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.ExecuteNonQuery();

        //    con.Close();

        //    int indexparent = gvRowParent2.RowIndex;


        //    HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[indexparent].FindControl("div1");
        //    object button = (object)ProductionViewGrid.Rows[indexparent].FindControl("ExpColMain");
        //    ExpandCollapseIndependent(button);
        //    div.Visible = true;

        //    Formatting();
        //}

       
        //private void MessageBox(string msg)
        //{
        //    Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        //}
    }
}
 

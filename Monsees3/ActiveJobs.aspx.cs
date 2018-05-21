using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Services;
using Monsees.Security;
using Monsees.Pages;
using Monsees.Database;
using Monsees.DataModel;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.SqlTypes;
using System.Drawing;

namespace Monsees
{
    public partial class ActiveJobsBase : DataPage
    {
        protected string MonseesConnectionString;
        protected string EmployeeLogin;
        protected string[] EmployeeLoginName;
        protected string EmployeeName;
        protected Int32 EmployeeID;
        protected Int32 index;
        protected Int32 indexl;
        public List<EmployeeModel> Employees { get; set; }
        public List<MachineModel> MachineList { get; set; }
        public List<OperationListModel> OperationList { get; set; }





        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not

            EmployeeLoginName = User.Identity.Name.Split('\\');

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            bool toggle = false;
            StringBuilder query = new StringBuilder("Select * From ProductionViewWP1");


            if (!String.IsNullOrEmpty(CompanyFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  CompanyName LIKE '%" + CompanyFilter.Text + "%'");
                }
            }

            if (!String.IsNullOrEmpty(PartFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE PartNumber LIKE '%" + PartFilter.Text + "%'");
                }
                else query.Append(" And PartNumber LIKE '%" + PartFilter.Text + "%'");
            }

            if (!String.IsNullOrEmpty(PMFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE Abbr LIKE '%" + PMFilter.Text + "%'");
                }
                else query.Append(" And Abbr LIKE '%" + PMFilter.Text + "%'");
            }

            if (!String.IsNullOrEmpty(LotFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE JobItemID LIKE '%" + LotFilter.Text + "%'");
                }
                else query.Append(" And JobItemID LIKE '%" + LotFilter.Text + "%'");
            }


            if (!String.IsNullOrEmpty(DescFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  DrawingNumber LIKE '%" + DescFilter.Text + "%'");
                }
                else query.Append(" And  DrawingNumber LIKE '%" + DescFilter.Text + "%'");
            }

            query.Append(" ORDER BY LateStartInt");

            MonseesSqlDataSource.SelectCommand = query.ToString();

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
            
            if (!IsPostBack)
            {
                LoadUsers();
            }

            GetData();

            BindPage();

        }


        protected virtual void BindPage()
        {
            UserNameLabel.Text = "Employee History";

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
            
            EmployeeLogin = User.Identity.Name;
            

            if (Page.IsPostBack == false)
            {

                UsersDropDownList.Visible = true;
            }


            Last_Refreshed.Text = "Last Refreshed : " + DateTime.Now;
        }


        protected void DeptSchedule_Click(object sender, EventArgs e)
        {

            Response.Write("<script type='text/javascript'>window.open('DeptSchedule.aspx');</script>");

        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {

            ProductionViewGrid.DataBind();

        }

        protected void ProductionViewGrid_SelectedIndexChanged(object sender, EventArgs e)
        {


        }

        protected void LogOutButton_Click(object sender, EventArgs e)
        {
            Session["Authenticate"] = false;
            Session["Employee"] = "";
            Session["EmployeeID"] = "";

            Response.Redirect("Default.aspx");
        }

        private void GetData()
        {
            this.UnitOfWork.Begin();
            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);

            MachineList = inspectionRepository.GetMachines();
            OperationList = inspectionRepository.GetOperations();
            this.UnitOfWork.End();
        }


        protected void LoadThumbnail(HttpContext context)
        {
            string imageid = context.Request.QueryString["ImID"];
            if (imageid == null || imageid == "")
            {
                //Set a default imageID
                imageid = "1";
            }
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select SetupImage from SetupImage where ImageID=" + imageid, connection);
            SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            Stream str = new MemoryStream((Byte[])dr[0]);

            Bitmap loBMP = new Bitmap(str);
            Bitmap bmpOut = new Bitmap(100, 100);

            Graphics g = Graphics.FromImage(bmpOut);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(Brushes.White, 0, 0, 100, 100);
            g.DrawImage(loBMP, 0, 0, 100, 100);

            MemoryStream ms = new MemoryStream();
            bmpOut.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bmpBytes = ms.GetBuffer();
            bmpOut.Dispose();
            ms.Close();
            context.Response.BinaryWrite(bmpBytes);
            connection.Close();
            context.Response.End();

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            bool toggle = false;
            StringBuilder query = new StringBuilder("Select * From ProductionViewWP1");


            if (!String.IsNullOrEmpty(CompanyFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  CompanyName LIKE '%" + CompanyFilter.Text + "%'");
                }
            }

            if (!String.IsNullOrEmpty(PartFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE PartNumber LIKE '%" + PartFilter.Text + "%'");
                }
                else query.Append(" And PartNumber LIKE '%" + PartFilter.Text + "%'");
            }

            if (!String.IsNullOrEmpty(PMFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE Abbr LIKE '%" + PMFilter.Text + "%'");
                }
                else query.Append(" And Abbr LIKE '%" + PMFilter.Text + "%'");
            }

            if (!String.IsNullOrEmpty(LotFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE JobItemID LIKE '%" + LotFilter.Text + "%'");
                }
                else query.Append(" And JobItemID LIKE '%" + LotFilter.Text + "%'");
            }


            if (!String.IsNullOrEmpty(DescFilter.Text))
            {
                if (toggle == false)
                {
                    toggle = true;
                    query.Append(" WHERE  DrawingNumber LIKE '%" + DescFilter.Text + "%'");
                }
                else query.Append(" And  DrawingNumber LIKE '%" + DescFilter.Text + "%'");
            }

            query.Append(" ORDER BY LateStartInt");

            MonseesSqlDataSource.SelectCommand = query.ToString();
            ProductionViewGrid.DataBind();

        }

        protected void ProductionViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            GridView gv;
            string LotID;
            string ProcessID;

            string command_name = e.CommandName;

            if ((command_name == "ThisOp") || (command_name == "Other") || (command_name == "Logout") || (command_name == "GetFile") || (command_name == "Deliveries") || (command_name == "PartHistory") || (command_name == "Inspection") || (command_name == "AddFixture") || (command_name == "InitCAR") || (command_name == "QuickFixture"))
            {
                Int32 totrows = ProductionViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "Other":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in

                        Response.Write("<script type='text/javascript'>window.open('Login.aspx?JobItemID=" + LotID + "&EmpID=" + EmployeeID + "','_blank');</script>");
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


                        break;

                    case "Inspection":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in
                        string pageName = (Page.IsInMappedRole("Inspection")) ? "InspectionReport.aspx" : "InspectionReportPrint.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobItemID=" + LotID + "');</script>");
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
                        break;

                    case "AddFixture":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in                        
                        Response.Write("<script type='text/javascript'>window.open('AddFixture.aspx?SourceLot=" + LotID + "');</script>");
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
                        break;

                    case "QuickFixture":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in                        
                        Response.Write("<script type='text/javascript'>window.open('QuickFixture.aspx?SourceLot=" + LotID + "');</script>");
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
                        break;

                    case "ThisOp":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in

                        // bool success = CreateProcessRecord();
                        //  if (success == true)
                        // {

                        // }
                        // else
                        // {
                        //MessageBox("This login failed.");
                        //  }

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

                        break;

                    case "Deliveries":
                        gvRow = ProductionViewGrid.Rows[index];
                        gv = (GridView)gvRow.FindControl("DeliveryViewGrid");

                        LotID = gvRow.Cells[2].Text;
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                        MonseesSqlDataSourceDeliveries.ConnectionString = MonseesConnectionString;
                        MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + LotID;
                        gv.DataSource = MonseesSqlDataSourceDeliveries;
                        gv.DataBind();

                        break;

                    case "GetFile":
                        String PartNumber;
                        String RevNumber;
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                        PartNumber = ((LinkButton)clickedRow.FindControl("lbpart")).Text;
                        RevNumber = clickedRow.Cells[5].Text;
                        Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);
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
                        break;

                    case "InitCAR":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in                        
                        Response.Write("<script type='text/javascript'>window.open('CARInitiate.aspx?id=" + LotID + "');</script>");
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


        private void LoadUsers()
        {
            this.UnitOfWork.Begin();
            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);

            Employees = inspectionRepository.GetActiveEmployees();
            UsersDropDownList.DataSource = Employees;

            UsersDropDownList.DataBind();
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
                GridView GridView6 = ProdGrid.FindControl("RevisionFixtureOrders") as GridView;
                GridView CertGrid = ProdGrid.FindControl("CertGrid") as GridView;
                GridView GridView8 = ProdGrid.FindControl("GridView8") as GridView;
                GridView GridView9 = ProdGrid.FindControl("GridView9") as GridView;
                GridView CARView = ProdGrid.FindControl("CARView") as GridView;

                MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

                DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
                DeliveryViewGrid.DataBind();

                SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

                ListView2.DataSource = SqlDataSource3;
                ListView2.DataBind();

                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID, SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

                GridView2.DataSource = SqlDataSource4;
                GridView2.DataBind();

                SqlDataSource5.SelectCommand = "SELECT [SubcontractID], [WorkCode], [Quantity], [DueDate], CAST(CASE WHEN [HasDetail]=1 THEN 0 ELSE 1 END As Bit) As [Received] FROM [SubcontractItems] WHERE [JobItemID] = " + JobItemID;

                GridView3.DataSource = SqlDataSource5;
                GridView3.DataBind();

                SqlDataSource6.SelectCommand = "SELECT [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

                GridView4.DataSource = SqlDataSource6;
                GridView4.DataBind();

                SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], [MatPriceID], pct, MatlAllocationID FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

                GridView5.DataSource = SqlDataSource7;
                GridView5.DataBind();

                
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

                //SqlDataSource9.SelectCommand = "SELECT [PartNumber], [Description], [Loc], [Material] FROM [FixtureInvSummary] WHERE [DetailUsingID] = " + DetailID;

                //GridView7.DataSource = SqlDataSource9;
                // GridView7.DataBind();

                SqlDataSource8.SelectCommand = "SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName], Location, Note, OperationName, FixtureRevID FROM [FixtureOrders] WHERE [DetailUsingID] =" + DetailID;

                GridView6.DataSource = SqlDataSource8;
                GridView6.DataBind();

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
                GridView GridView6 = ProdGrid.FindControl("RevisionFixtureOrders") as GridView;
                GridView CertGrid = ProdGrid.FindControl("CertGrid") as GridView;
                GridView GridView8 = ProdGrid.FindControl("GridView8") as GridView;
                GridView GridView9 = ProdGrid.FindControl("GridView9") as GridView;
                GridView CARView = ProdGrid.FindControl("CARView") as GridView;

                MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

                DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
                DeliveryViewGrid.DataBind();

                SqlDataSource3.SelectCommand = "SELECT HeatTreatLabel AS Heat_Treat, PlatingLabel AS Plating, SubcontractLabel AS Subcontract, Subcontract2Label AS Subcontract2, [Estimated Hours] AS EstimatedTotalHours, [Notes], [Quantity], [Revision Number] AS Rev, [Material], [Dimension], [MaterialSize], [StockCut], [PartsPerCut], [PurchaseCut], [Drill], [DrillSize], [Comments], [Expr1], DetailID FROM [ViewJobItem] WHERE [JobItemID] = " + JobItemID;

                ListView2.DataSource = SqlDataSource3;
                ListView2.DataBind();

                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID, SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

                GridView2.DataSource = SqlDataSource4;
                GridView2.DataBind();

                SqlDataSource5.SelectCommand = "SELECT [SubcontractID], [WorkCode], [Quantity], [DueDate], CAST(CASE WHEN [HasDetail]=1 THEN 0 ELSE 1 END As Bit) As [Received] FROM [SubcontractItems] WHERE [JobItemID] = " + JobItemID;

                GridView3.DataSource = SqlDataSource5;
                GridView3.DataBind();

                SqlDataSource6.SelectCommand = "SELECT [MaterialName], [Dimension], [Diameter], [Height], [Width], [Length], [Quantity], [Cut], [OrderPending] FROM [MatQuoteQueue] WHERE [JobItemID] =" + JobItemID;

                GridView4.DataSource = SqlDataSource6;
                GridView4.DataBind();

                SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], [MatPriceID], pct, MatlAllocationID FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

                GridView5.DataSource = SqlDataSource7;
                GridView5.DataBind();


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

                //SqlDataSource9.SelectCommand = "SELECT [PartNumber], [Description], [Loc], [Material] FROM [FixtureInvSummary] WHERE [DetailUsingID] = " + DetailID;

                //GridView7.DataSource = SqlDataSource9;
                // GridView7.DataBind();

                SqlDataSource8.SelectCommand = "SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName], Location, Note, OperationName, FixtureRevID FROM [FixtureOrders] WHERE [DetailUsingID] =" + DetailID;

                GridView6.DataSource = SqlDataSource8;
                GridView6.DataBind();

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

        


        protected void GridView2_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpanded(gvwChild, sender);

        }

        protected void GridView2_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;

            Int32 HoursValue;
            Int32 MachineValue;
            Int32 EmployeeValue;
            Int32 QtyInValue;
            Int32 QtyOutValue;

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            TextBox Hours = (TextBox)gvrow.FindControl("Hours");
            TextBox QtyIn = (TextBox)gvrow.FindControl("QtyIn");
            TextBox QtyOut = (TextBox)gvrow.FindControl("QtyOut");
            DropDownList EmplID = (DropDownList)gvrow.FindControl("Empl");
            CheckBox Checked = (CheckBox)gvrow.FindControl("Completed");
            DropDownList MachineID = (DropDownList)gvwChild.FindControl("MachineList");
            CheckBox Fix = (CheckBox)gvwChild.FindControl("FixAdd");
            TextBox Description = (TextBox)gvwChild.FindControl("DescAdd");

            gvwChild.EditIndex = -1;

            KeepExpanded(gvwChild, sender);


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

            if (MachineID.SelectedValue != "")
            {
                MachineValue = Convert.ToInt32(MachineID.SelectedValue);
            }
            else
            {
                MachineValue = 0;
            };

            if (EmplID.SelectedValue != "")
            {
                EmployeeValue = Convert.ToInt32(EmplID.SelectedValue);
            }
            else
            {
                EmployeeValue = 0;
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
            comm2.Parameters.AddWithValue("@JobSetupID", Convert.ToInt32(gvwChild.DataKeys[e.RowIndex].Value.ToString()));
            comm2.Parameters.AddWithValue("@ProgramNum", "");
            comm2.Parameters.AddWithValue("@CheckMoveOn", Convert.ToBoolean(Checked.Checked));
            comm2.Parameters.AddWithValue("@MachineID", MachineValue);
            comm2.Parameters.AddWithValue("@Fix", Convert.ToBoolean(Fix.Checked));
            comm2.Parameters.AddWithValue("@Description", Description.Text);
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

        protected void GridView2_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = e.NewEditIndex;

            KeepExpanded(gvwChild, sender);



        }

        protected void GridView2_RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = e.RowIndex;
            KeepExpanded(gvwChild, sender);

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
            index = gvRowParent.RowIndex;
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
         
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

        private void BindChildgvwChildView(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID, SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

            gvChild.DataSource = SqlDataSource4;
            gvChild.DataBind();
        }

        protected void UserNameLabel_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.open('EmployeeHistory.aspx?Employee=" + UsersDropDownList.SelectedValue + "','_blank');</script>");

        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
            string JobSetupID = "0";
            string SetupID = "0";
            Int32 i = Grid.EditIndex;
            GridViewRow Row = e.Row;
            GridView LogHoursGrid = (GridView)Row.FindControl("LogHoursGrid");
            GridView SetupFixtureOrders = (GridView)Row.FindControl("SetupFixtureOrders");
            GridView SetupHistoryGrid = (GridView)Row.FindControl("SetupHistoryGrid");
            GridView SetupEntries = (GridView)Row.FindControl("SetupEntries");
            DropDownList EmployeeCommentDrop = (DropDownList)Row.FindControl("EmployeeCommentDrop");

            
            if (Row.RowIndex > -1)
            {
                JobSetupID = Grid.DataKeys[Row.RowIndex].Values[0].ToString();
                SetupID = Grid.DataKeys[Row.RowIndex].Values[2].ToString();
                EmployeeCommentDrop.DataSource = EmployeeList;
                EmployeeCommentDrop.DataBind();
                EmployeeCommentDrop.SelectedValue = EmployeeID.ToString();
                if (SetupID != "")
                { 
                    LogHoursGridSource.SelectCommand = "SELECT ProcessID, JobSetupID, Name, Hours, QuantityIn, QuantityOut, EmployeeID, Login, Logout, Fix, Description, MachineID, Completed FROM LoggedHoursSummary WHERE JobSetupID = " + JobSetupID;
                    LogHoursGrid.DataSource = LogHoursGridSource;
                    LogHoursGrid.DataBind();
                    SetupFixtureSource.SelectCommand = "SELECT [PartNumber], [DrawingNumber], [Quantity], [ContactName], Location, FixtureRevID, Note FROM [FixtureOrdersbySetup] WHERE [SetupUsingID] =" + SetupID;
                    SetupFixtureOrders.DataSource = SetupFixtureSource;
                    SetupFixtureOrders.DataBind();
                    SetupHistorySource.SelectCommand = "SELECT JobSetupID, JobItemID, JobNumber, Name, Machine, Quantity, QuantityIn, QuantityOut, Hours, Logout FROM SetupHistory WHERE Completed = 1 And SetupID = " + SetupID + " ORDER BY JobItemID DESC";
                    SetupHistoryGrid.DataSource = SetupHistorySource;
                    SetupHistoryGrid.DataBind();
                    SetupEntrySource.SelectCommand = "SELECT SetupEntryID, SetupID, Name, Entry, Timestamp FROM SetupEntries LEFT OUTER JOIN Employees ON SetupEntries.EmployeeID = Employees.EmployeeID WHERE SetupID = " + SetupID;
                    SetupEntries.DataSource = SetupEntrySource;
                    SetupEntries.DataBind();
                }

            }

        }

        protected void LogHoursGrid_RowCancel(Object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
            KeepExpandedLog(gvwChild, sender);

        }

        protected void LogHoursGrid_RowUpdate(Object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;

            double HoursValue;

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            GridView gvParent = (GridView)gvwChild.Parent.Parent.Parent.Parent.Parent.Parent;
            GridViewRow gvrowParent = ((GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            TextBox Hours = (TextBox)gvrow.FindControl("Hours");

            TextBox QtyIn = (TextBox)gvrow.FindControl("QtyIn");
            TextBox QtyOut = (TextBox)gvrow.FindControl("QtyOut");
            DropDownList EmplID = (DropDownList)gvrow.FindControl("Empl");
            CheckBox Checked = (CheckBox)gvrow.FindControl("MoveOn");
            DropDownList MachineID = (DropDownList)gvrow.FindControl("Machine");
            CheckBox Fix = (CheckBox)gvrow.FindControl("Fix");
            TextBox Description = (TextBox)gvrow.FindControl("ProcDesc");

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
            //comm2.Parameters.AddWithValue("@Fix", Convert.ToBoolean(Fix.Checked));
            //comm2.Parameters.AddWithValue("@MachineID", Convert.ToInt32(MachineID.SelectedValue));
           // comm2.Parameters.AddWithValue("@Description", Description.Text);

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

            ProductionViewGrid.DataBind();
            KeepExpandedLogSub(gvwChild, sender);
            string divtxt = "div" + gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString();
            BindChildgvwChildLog(gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('" + divtxt + "').style.display = 'inline';", true);
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

        protected void LogHoursGrid_RowEditing(Object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            GridView gvParent = (GridView)gvwChild.Parent.Parent.Parent.Parent.Parent.Parent;
            GridViewRow gvrowParent = ((GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
            gvwChild.EditIndex = e.NewEditIndex;

            string divtxt = "div" + gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString();
            BindChildgvwChildLog(gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('" + divtxt + "').style.display = 'inline';", true);
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
                dpl = (DropDownList)Row.FindControl("Machine");
                if (dpl != null)
                {
                    dpl.DataSource = MachineList;
                    dpl.DataBind();
                    dpl.Items.Insert(0, "None");
                    string val = ((HiddenField)Row.FindControl("hdMach")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);                    

                }
            }

        }
        
        protected void AddOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
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

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }


        protected void CancelAddOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void AddNowOp_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            string JobItemID = gvRowParent.Cells[2].Text;
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


            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

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

            BindChildgvwChildView(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), GridView2);
            ProductionViewGrid.DataBind();
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
        }

        protected void LogNewNow_Command(object sender, CommandEventArgs e)
        {
            string MonseesConnectionString;

            double HoursValue;
            Int32 MachineValue;
            Int32 EmployeeValue;
            Int32 QtyInValue;
            Int32 QtyOutValue;
            Double RuntimeValue;
            int index;
            System.Web.UI.WebControls.GridViewRow gvwChild = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView gvChild = (GridView)gvwChild.Parent.Parent;
            GridView gvChildChild = (GridView)gvwChild.FindControl("LogHoursGrid");
            TextBox Hours = (TextBox)gvwChild.FindControl("HoursAdd");
            TextBox QtyIn = (TextBox)gvwChild.FindControl("QtyInAdd");
            TextBox QtyOut = (TextBox)gvwChild.FindControl("QtyOutAdd");
            TextBox Runtime = (TextBox)gvwChild.FindControl("RuntimeAdd");

            DropDownList EmplID = (DropDownList)gvwChild.FindControl("EmployeeList2");

            CheckBox Checked = (CheckBox)gvwChild.FindControl("MoveOn");
            DropDownList MachineID = (DropDownList)gvwChild.FindControl("MachineList");
            CheckBox Fix = (CheckBox)gvwChild.FindControl("FixAdd");
            TextBox Description = (TextBox)gvwChild.FindControl("DescAdd");


            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            string JobItemID = ProductionViewGrid.DataKeys[index].Value.ToString();
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

            if (MachineID.SelectedValue != "")
            {
                MachineValue = Convert.ToInt32(MachineID.SelectedValue);
            }
            else
            {
                MachineValue = 0;
            };

            if (EmplID.SelectedValue != "")
            {
                EmployeeValue = Convert.ToInt32(EmplID.SelectedValue);
            }
            else
            {
                EmployeeValue = 0;
            };

            if (Runtime.Text.Trim() != "")
            {
                RuntimeValue = Convert.ToDouble(Runtime.Text);
            }
            else
            {
                RuntimeValue = 0;
            };

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("MoveProcessNew", con);
            comm2.CommandType = CommandType.StoredProcedure;
            comm2.Parameters.AddWithValue("@QuantityIn", QtyInValue);
            comm2.Parameters.AddWithValue("@QuantityOut", QtyOutValue);
            comm2.Parameters.AddWithValue("@Hours", HoursValue);
            comm2.Parameters.AddWithValue("@Runtime", RuntimeValue);
            comm2.Parameters.AddWithValue("@Logout", DateTime.Now);
            comm2.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(JobItemID));
            comm2.Parameters.AddWithValue("@EmployeeID", EmployeeValue);
            
            comm2.Parameters.AddWithValue("@JobSetupID", Convert.ToInt32(gvChild.DataKeys[gvwChild.RowIndex].Value.ToString()));
            comm2.Parameters.AddWithValue("@ProgramNum", "");
            
            comm2.Parameters.AddWithValue("@CheckMoveOn", Convert.ToBoolean(Checked.Checked));
            comm2.Parameters.AddWithValue("@MachineID", MachineValue);
            comm2.Parameters.AddWithValue("@Fix", Convert.ToBoolean(Fix.Checked));
            comm2.Parameters.AddWithValue("@Description", Description.Text);
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
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            BindChildgvwChildLog(gvChild.DataKeys[gvwChild.RowIndex].Values[0].ToString(), gvChildChild);
            ProductionViewGrid.DataBind();
            foreach (GridViewRow gr in ProductionViewGrid.Rows)
            {
                if (ProductionViewGrid.DataKeys[gr.RowIndex].Value.ToString() == JobItemID) index = Convert.ToInt32(gr.RowIndex.ToString());
            }
            KeepExpandedLogSub(gvChildChild, sender);
            KeepExpandedSetup(GridView2, sender);
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
                    string SetupID;
                    
                    Int32 totrows = GridView2.Rows.Count;
                    

                    switch (e.CommandName)
                    {
                        case "Attach":
                                index = Convert.ToInt32(e.CommandArgument) % totrows;
                                gvRow = GridView2.Rows[index];
                                JobSetupID = GridView2.DataKeys[index].Values[0].ToString();
                                JobItemID = GridView2.DataKeys[index].Values[1].ToString();
                                SetupID = GridView2.DataKeys[index].Values[2].ToString();
                                string CurrProcessOrder = GridView2.DataKeys[index].Values[2].ToString();
                                byte[] buffer;
                                Guid fileId = Guid.NewGuid();
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
                                break;
                case "OrderFixture":
                    index = Convert.ToInt32(e.CommandArgument) % totrows;
                    gvRow = GridView2.Rows[index];
                    JobSetupID = GridView2.DataKeys[index].Values[0].ToString();
                    JobItemID = GridView2.DataKeys[index].Values[1].ToString();
                    SetupID = GridView2.DataKeys[index].Values[2].ToString();
                    Response.Write("<script type='text/javascript'>window.open('AddFixture.aspx?SourceLot=" + JobItemID + "&SourceSetup=" + SetupID + "','_blank');</script>");
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
                    KeepExpanded(GridView2, sender);
                    break;
                case "QuickFixture":
                    index = Convert.ToInt32(e.CommandArgument) % totrows;
                    gvRow = GridView2.Rows[index];
                    JobSetupID = GridView2.DataKeys[index].Values[0].ToString();
                    JobItemID = GridView2.DataKeys[index].Values[1].ToString();
                    SetupID = GridView2.DataKeys[index].Values[2].ToString();
                    Response.Write("<script type='text/javascript'>window.open('QuickFixture.aspx?SourceLot=" + JobItemID + "&SourceSetup=" + SetupID + "','_blank');</script>");
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
                    KeepExpanded(GridView2, sender);
                    break;

                case "OpenSetupSheet":
                    index = Convert.ToInt32(e.CommandArgument) % totrows;
                    gvRow = GridView2.Rows[index];
                    JobSetupID = GridView2.DataKeys[index].Values[0].ToString();
                    JobItemID = GridView2.DataKeys[index].Values[1].ToString();
                    SetupID = GridView2.DataKeys[index].Values[2].ToString();
                    Response.Write("<script type='text/javascript'>window.open('SetupSheet.aspx?JobItemID=" + JobItemID + "&JobSetupID=" + JobSetupID + "&EmpID=" + EmployeeID + "','_blank');</script>");
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
                    KeepExpanded(GridView2, sender);
                    break;
                case "AddComment":
                    index = Convert.ToInt32(e.CommandArgument) % totrows;
                    gvRow = GridView2.Rows[index];
                    JobSetupID = GridView2.DataKeys[index].Values[0].ToString();
                    JobItemID = GridView2.DataKeys[index].Values[1].ToString();
                    SetupID = GridView2.DataKeys[index].Values[2].ToString();
                    using (System.Data.SqlClient.SqlConnection con7 = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {
                        DropDownList EmployeeDrop = (DropDownList)gvRow.FindControl("EmployeeCommentDrop");
                        TextBox Entry = (TextBox)gvRow.FindControl("EntryText");
                        con7.Open();

                        System.Data.SqlClient.SqlCommand cmd7 = new System.Data.SqlClient.SqlCommand("SetupCommentAdd", con7);
                        cmd7.CommandType = CommandType.StoredProcedure;

                        cmd7.Parameters.AddWithValue("@SetupID", SetupID);
                        cmd7.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(EmployeeDrop.SelectedValue));
                        cmd7.Parameters.AddWithValue("@EntryTxt", Entry.Text);
                        cmd7.ExecuteNonQuery();

                        con7.Close();
                    }
                              
                    KeepExpanded(GridView2, sender);
        
                    for (int i = 0; i<ProductionViewGrid.Rows.Count; i++)
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
                    break;
                default:
                                break;
                    }
                }

        protected void KeepExpandedSetup(System.Web.UI.WebControls.GridView gvParent, object sender)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            string divtxt = "div" + gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString();
            //BindChildgvwChildView(GridView2.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('" + divtxt + "').style.display = 'inline';", true);
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

        protected void KeepExpandedLog(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvwChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent = (gvwChild).Parent.Parent.Parent.Parent as GridViewRow;
            
            GridViewRow gvRowParentParent = (gvwChild).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            int indexparent = gvRowParentParent.RowIndex;
            index = gvRowParent.RowIndex;
            BindChildgvwChildLog(gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), gvwChild);
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[indexparent].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
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

        protected void KeepExpandedLogSub(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridView gvParent = ((System.Web.UI.WebControls.GridView)gvwChild).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent = (gvwChild).Parent.Parent.Parent.Parent as GridViewRow;

            GridViewRow gvRowParentParent = (gvwChild).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            int indexparent = gvRowParentParent.RowIndex;
            index = gvRowParentParent.RowIndex;
            BindChildgvwChildLog(gvParent.DataKeys[gvRowParent.RowIndex].Values[0].ToString(), gvwChild);
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[indexparent].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
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

        private void BindChildgvwChildLog(string jobsetupId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobSetupID = jobsetupId;
            LogHoursGridSource.SelectCommand = "SELECT ProcessID, JobSetupID, Name, Hours, QuantityIn, QuantityOut, EmployeeID, MachineID, Login, Logout, Fix, Description, Completed FROM LoggedHoursSummary WHERE JobSetupID = " + JobSetupID;

            gvChild.DataSource = LogHoursGridSource;
            gvChild.DataBind();
            
        }

        protected void LogHoursGrid_RowDelete(Object sender, GridViewDeleteEventArgs e)
        {
            string MonseesConnectionString;

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            GridView gvParent = (GridView)gvwChild.Parent.Parent.Parent.Parent.Parent.Parent;
            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];
            GridViewRow gvrowParent = ((GridView)sender).Parent.Parent.Parent.Parent as GridViewRow;
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
                gvwChild.DataBind();
            }
            string divtxt = "div" + gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString();
            BindChildgvwChildLog(gvParent.DataKeys[gvrowParent.RowIndex].Values[0].ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('" + divtxt + "').style.display = 'inline';", true);
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
            dpl = (DropDownList)LogHoursGrid.FindControl("MachineList");
            if (dpl != null)
            {
                dpl.DataSource = MachineList;
                dpl.DataBind();

            }

            KeepExpandedSetup(GridView2, sender);
            

        }

        protected void CancelLog_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridView GridView2 = gvRowParent.FindControl("GridView2") as GridView;
            KeepExpandedSetup(GridView2, sender);

        }

        protected void reload_Click(object sender, EventArgs e)
        {
            ProductionViewGrid.DataBind();
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

        private void BindChildgvwChildViewGrid5(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            SqlDataSource7.SelectCommand = "SELECT [MaterialName], [Dimension], [D], [H], [W], [L], [Qty], [Cut], [received], [Prepared], [Location], [MaterialSource], pct, MatPriceID, MatlAllocationID FROM [JobItemMatlPurchaseSummary] WHERE [JobItemID] =" + JobItemID;

            gvChild.DataSource = SqlDataSource7;
            gvChild.DataBind();
        }

        private void BindChildgvwChildViewStockRet(string MaterialID, System.Web.UI.WebControls.GridView gvChild)
        {

            RetMatlSource.SelectCommand = "SELECT MatPriceID, [MaterialName], [Dimension], [D], [H], [W], [L], [Qty] FROM [JobItemMatlPurchaseSummary] WHERE [MatPriceID] =" + (string.IsNullOrEmpty(MaterialID) ? "0" : MaterialID);

            gvChild.DataSource = SqlDataSource7;
            gvChild.DataBind();
        }

        protected void StockRetGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridViewRow gvRow;
            GridView gv = (GridView)sender;
            MultiView MatAllocMulti = (MultiView)((GridViewRow)((GridView)sender).Parent.Parent.Parent).FindControl("MatAllocMulti");
            View MatlRetView = (View)((GridViewRow)((GridView)sender).Parent.Parent.Parent).FindControl("StockRetView");
            gvRow = gv.Rows[Convert.ToInt32(e.CommandArgument)];
            string MaterialID = gv.DataKeys[Convert.ToInt32(e.CommandArgument)].Values[1].ToString();
            GridView StockRetGrid = ((GridViewRow)((GridView)sender).Parent.Parent.Parent).FindControl("StockRetGrid") as GridView;
            RetMatlSource.SelectCommand = "SELECT MatPriceID, [MaterialName], [Dimension], [D], [H], [W], [L], [Qty] FROM [JobItemMatlPurchaseSummary] WHERE [MatlAllocationID] =" + (string.IsNullOrEmpty(MaterialID) ? "0" : MaterialID);

            StockRetGrid.DataSource = RetMatlSource;
            StockRetGrid.DataBind();
            MatAllocMulti.SetActiveView(MatlRetView);
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
            BindChildgvwChildViewStockRet(ProductionViewGrid.DataKeys[gvRowParent.RowIndex].Value.ToString(), gv);
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
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
            index = ParentRow.RowIndex;
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
            string JobItemID = ProductionViewGrid.DataKeys[ParentRow.RowIndex].Values[0].ToString();
            com.Parameters.AddWithValue("@MatPriceID", Convert.ToInt32(MatPriceID));
            com.Parameters.AddWithValue("@Length", Convert.ToDouble(Len));
            com.Parameters.AddWithValue("@Qty", Convert.ToInt32(Qty));
            com.Parameters.AddWithValue("@Loc", Loc);
            com.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(JobItemID));

            com.ExecuteNonQuery();
            con.Dispose();
            con.Close();

            MatAllocMulti.SetActiveView(MatlRetView);

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
            BindChildgvwChildViewGrid5(ProductionViewGrid.DataKeys[ParentRow.RowIndex].Value.ToString(), GridView5);
            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[index].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[index].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;
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
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridView gvParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent2 = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
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

            int indexparent = gvRowParent3.RowIndex;
            

            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[indexparent].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[indexparent].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;

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

        protected void FixtureCloseButton2_Click(object sender, EventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent as GridViewRow;
            index = gvRowParent.RowIndex;
            GridView gvParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent as GridView;
            GridViewRow gvRowParent2 = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent.Parent.Parent as GridViewRow;
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

            com.Parameters.AddWithValue("@note", FixtNote.Text);

            con.Open();
            com.CommandType = CommandType.StoredProcedure;
            com.ExecuteNonQuery();

            con.Close();

            int indexparent = gvRowParent2.RowIndex;
            

            HtmlGenericControl div = (HtmlGenericControl)ProductionViewGrid.Rows[indexparent].FindControl("div1");
            object button = (object)ProductionViewGrid.Rows[indexparent].FindControl("ExpColMain");
            ExpandCollapseIndependent(button);
            div.Visible = true;

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

        protected void orderfixturebutton_Click(object sender, EventArgs e)
        {

        }

        protected void quickfixture_Click(object sender, EventArgs e)
        {

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

        protected void CommentButton_Click(object sender, EventArgs e)
        {

        }
    }
}
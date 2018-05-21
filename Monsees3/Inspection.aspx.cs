using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
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
    public partial class Inspection : System.Web.UI.Page
    {
        private string MonseesConnectionString;
        
        private Int32 index;

        string LinkID;

   

        protected void Page_Load(object sender, EventArgs e)
        {


            // Check if the user is already logged in or not

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MonseesSqlDataSource.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSourceInvW.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSourceInvUC.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSourceDel.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSourceLots.ConnectionString = MonseesConnectionString;

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

            //string sqlstring = "Select [EmployeeID], [Name] FROM [Employees] WHERE [WindowsAuthLogin] = 'jspurling';";
            // create a connection with sqldatabase 
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            // create a sql command which will user connection string and your select statement string
            //System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
            System.Data.SqlClient.SqlDataReader reader;
            // open a connection with sqldatabase

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

        protected void ProdViewButton_Click(object sender, EventArgs e)
        {

            int CurrentView = InspectionMultiview.ActiveViewIndex;

            if (CurrentView != 0)
            {
                ProductionViewGrid.DataBind();

                InspectionMultiview.SetActiveView(Production);

            }

        }

        protected void InventoryViewButton_Click(object sender, EventArgs e)
        {

            int CurrentView = InspectionMultiview.ActiveViewIndex;

            if (CurrentView != 1)
            {
                InventoryViewGridW.DataBind();
                InventoryViewGridUC.DataBind();

                InspectionMultiview.SetActiveView(InventoryB);

            }

        }

        protected void DeliveriesViewButton_Click(object sender, EventArgs e)
        {

            int CurrentView = InspectionMultiview.ActiveViewIndex;

            if (CurrentView != 2)
            {
                
                InspectionMultiview.SetActiveView(Deliveries);

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

        protected void DeliveryViewGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                try
                {
                    index = e.Row.RowIndex;
                    string DeliveryID = DeliveryViewGrid.DataKeys[index].Values[0].ToString();
                    GridView LotViewGrid = e.Row.FindControl("LotViewGrid") as GridView;
                    MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    MonseesSqlDataSourceLots.ConnectionString = MonseesConnectionString;
                    MonseesSqlDataSourceLots.SelectCommand = "SELECT [LotNumber], [Quantity], [JobNumber], [RTS], [PCert], [MCert] FROM [FormLots] WHERE DeliverID=" + DeliveryID;
                    LotViewGrid.DataSource = MonseesSqlDataSourceLots;
                    LotViewGrid.DataBind();
                }
                catch(Exception f)
                {

                }
            }

        }
        
        protected void ProductionViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;
            
            string command_name = e.CommandName;

            if ((command_name == "ViewReport") || (command_name == "InitiateCA") || (command_name == "GetFile") || (command_name == "Deliveries") || (command_name == "Close Part") || (command_name == "PrintReport") || (command_name == "InitCAR"))
            {
                Int32 totrows = ProductionViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job
                
                switch (e.CommandName)
                {
                    case "ViewReport":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con3.Open();
                        
                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("CreateInspection", con3);
                        comm3.CommandType = CommandType.StoredProcedure;

                        
                        comm3.Parameters.AddWithValue("@JobItemID", LotID);
                        
                        comm3.ExecuteNonQuery();

                        con3.Close();

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

                    case "InitCAR":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        string pageNameprt = "CARInitiate.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?id=" + LotID + "');</script>");
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
                    case "PrintReport":
                        gvRow = ProductionViewGrid.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con6 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con6.Open();

                        System.Data.SqlClient.SqlCommand comm6 = new System.Data.SqlClient.SqlCommand("CreateInspection", con6);
                        comm6.CommandType = CommandType.StoredProcedure;


                        comm6.Parameters.AddWithValue("@JobItemID", LotID);

                        comm6.ExecuteNonQuery();

                        con6.Close();

                        pageNameprt = "InspectionReportPrint.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?JobItemID=" + LotID + "');</script>");
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
                        LotID = gvRow.Cells[2].Text;
                        Response.Write("<script type='text/javascript'>window.open('Deliveries.aspx?JobItemID=" + LotID + "','_blank');</script>");
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

        protected void InventoryViewGridW_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;

            string command_name = e.CommandName;

            if ((command_name == "ViewReport") || (command_name == "GetFile") || (command_name == "Deliveries") || (command_name == "PrintReport"))
            {
                Int32 totrows = InventoryViewGridW.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "ViewReport":
                        gvRow = InventoryViewGridW.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in

                        string pageName = (Page.IsInMappedRole("Inspection")) ? "InspectionReport.aspx" : "InspectionReportPrint.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobItemID=" + LotID + "');</script>");

                        break;
                    case "PrintReport":
                        gvRow = InventoryViewGridW.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        //Check to see if user is already logged in

                        string pageNameprt = (Page.IsInMappedRole("Inspection")) ? "InspectionReportPrint.aspx" : "InspectionReportPrint.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?JobItemID=" + LotID + "');</script>");

                        break;
                    case "Deliveries":
                        gvRow = InventoryViewGridW.Rows[index];
                        LotID = gvRow.Cells[2].Text;
                        Response.Write("<script type='text/javascript'>window.open('Deliveries.aspx?JobItemID=" + LotID + "','_blank');</script>");
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

        protected void InventoryViewGridUC_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;
            
            string command_name = e.CommandName;

            if ((command_name == "ViewReport") || (command_name == "Confirm") || (command_name == "GetFile") || (command_name == "Release"))
            {
                Int32 totrows = InventoryViewGridUC.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;
                int result;
                LinkID = InventoryViewGridUC.DataKeys[index].Values[0].ToString();
                string SqlStr;
                System.Data.SqlClient.SqlConnection con;
                System.Data.SqlClient.SqlCommand comm2;
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "ViewReport":
                        gvRow = InventoryViewGridUC.Rows[index];
                        LotID = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con3.Open();
                        
                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("CreateInspection", con3);
                        comm3.CommandType = CommandType.StoredProcedure;

                        
                        comm3.Parameters.AddWithValue("@JobItemID", LotID);
                        
                        comm3.ExecuteNonQuery();

                        con3.Close();

                        string pageName = (Page.IsInMappedRole("Inspection")) ? "InspectionReport.aspx" : "InspectionReportPrint.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobItemID=" + LotID + "');</script>");

                        break;
                   
                    case "GetFile":
                        String PartNumber;
                        String RevNumber;
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                        PartNumber = clickedRow.Cells[2].Text;
                        RevNumber = clickedRow.Cells[3].Text;
                        Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);

                        break;

                    case "Confirm":
                        gvRow = InventoryViewGridUC.Rows[index];
                        Int32 InventoryQty = Convert.ToInt32(gvRow.Cells[5].Text);
                        LotID = gvRow.Cells[0].Text;
                        Int32 Qty = Convert.ToInt32(gvRow.Cells[1].Text);
                        string status = gvRow.Cells[6].Text;
                        
                        if (status == "Incomplete" || status == "Discrepant")
                        {
                            MessageBox("This inventory is either marked incomplete or discrepant.  Its lot will be reactivated and returned to the production schedule for completion or rework.");
                        }
                        System.Data.SqlClient.SqlConnection con4 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm5 = new System.Data.SqlClient.SqlCommand("ConfirmInventoryPart1", con4);
                        comm5.CommandType = System.Data.CommandType.StoredProcedure;
                        comm5.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(LotID));
                        comm5.Parameters.AddWithValue("@InventoryQty", InventoryQty);                        
                        comm5.Parameters.AddWithValue("@DeliveryItemID", LinkID);
                        comm5.Parameters.AddWithValue("@Quantity", Qty);

                        
                        System.Data.SqlClient.SqlCommand comm4 = new System.Data.SqlClient.SqlCommand("ConfirmInventoryPart2", con4);
                        comm4.CommandType = System.Data.CommandType.StoredProcedure;
                        comm4.Parameters.AddWithValue("@Question", 6);
                                                
                        comm4.Parameters.AddWithValue("@DeliveryItemID", LinkID);
                        comm4.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(LotID));
                        comm4.Parameters.AddWithValue("@Status", status);


                        con4.Open();

                        // execute sql command and store a return values in reade
                        comm5.ExecuteNonQuery();
                        comm4.ExecuteNonQuery();
                        con4.Close();
                        
                        Response.Write("<script type='text/javascript'>window.open('/Reports/Label.aspx?id=" + LotID + "');</script>");
                        InventoryViewGridUC.DataBind();
                        break;

                    case "Release":
                        gvRow = InventoryViewGridUC.Rows[index];
                        LotID = gvRow.Cells[0].Text;
                        
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            
                        con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con.Open();
                        
                        SqlStr = "DELETE [DeliveryItem] WHERE DeliveryItemID = @LotID;";
                        comm2 = new System.Data.SqlClient.SqlCommand(SqlStr, con);
                        comm2.CommandType = CommandType.Text;

                        
                        comm2.Parameters.AddWithValue("@LotID", LotID);
                        
                        result = comm2.ExecuteNonQuery();
                        InventoryViewGridUC.DataBind();
                        break;
                        


                    default:

                        break;

                }
            }
        }

        protected void DeliveryViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            GridView gv;
            string DeliveryID;

            string command_name = e.CommandName;

            if ((command_name == "GetFile"))
            {
                Int32 totrows = DeliveryViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {

                   

                    case "GetFile":
                        String PartNumber;
                        String RevNumber;
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                        PartNumber = clickedRow.Cells[6].Text;
                        RevNumber = clickedRow.Cells[7].Text;
                        Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);
                        break;
                    default:

                        break;

                }
            }
        }

        protected void DeliveryViewGrid_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


        protected void LotViewGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            string LotID;

            string command_name = e.CommandName;
            GridView view = sender as GridView;

           

                Int32 totrows = DeliveryViewGrid.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;
                
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "ViewReport":
                        
                        gvRow = view.Rows[index];
                        LotID = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in
                        //MessageBox("The index fired is " + index);
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con3.Open();
                        
                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("CreateInspection", con3);
                        comm3.CommandType = CommandType.StoredProcedure;

                        
                        comm3.Parameters.AddWithValue("@JobItemID", LotID);
                        
                        comm3.ExecuteNonQuery();

                        con3.Close();
                        string pageName = (Page.IsInMappedRole("Inspection")) ? "InspectionReport.aspx" : "InspectionReportPrint.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobItemID=" + LotID + "');</script>");
                        break;
                    case "Print Report":

                        gvRow = view.Rows[index];
                        LotID = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in
                        //MessageBox("The index fired is " + index);
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con6 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con6.Open();

                        System.Data.SqlClient.SqlCommand comm6 = new System.Data.SqlClient.SqlCommand("CreateInspection", con6);
                        comm6.CommandType = CommandType.StoredProcedure;


                        comm6.Parameters.AddWithValue("@JobItemID", LotID);

                        comm6.ExecuteNonQuery();

                        con6.Close();
                        string pageNameprt = "InspectionReportPrint.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?JobItemID=" + LotID + "');</script>");
                        break;
                    case "Close":
                        gvRow = view.Rows[index];
                        LotID = gvRow.Cells[0].Text;
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            
                        System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con.Open();
                        int result;
                        string SqlStr = "UPDATE [Job Item] SET [IsOpen] = False WHERE JobItemID = @LotID; UPDATE DeliveryItem SET RTS = True WHERE LotNumber = @LotID";
                        System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(SqlStr, con);
                        comm2.CommandType = CommandType.Text;

                        
                        comm2.Parameters.AddWithValue("@LotID", LotID);
                        
                        result = comm2.ExecuteNonQuery();           
            
           
                        break;

              

                default:

                        break;

                }
           
        }

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }

        protected void SerialNumbersMain_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvRow;
            string SerialID;

            string command_name = e.CommandName;
            GridView view = sender as GridView;

            index = Convert.ToInt32(e.CommandArgument);

            //TO DO: Check to see if the user is already logged into the given job

            switch (e.CommandName)
            {
                case "Input":
                    //gvRow = ProductionViewGrid.Rows[index];
                    SerialID = view.DataKeys[index].Value.ToString();
                    //Check to see if user is already logged in
                    

                    string pageName = (Page.IsInMappedRole("Inspection")) ? "InspectionReport.aspx" : "InspectionReportPrint.aspx";
                    Response.Write("<script type='text/javascript'>window.open('" + pageName + "?SerialNumID=" + SerialID + "');</script>");
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

               
                case "View":
                    //gvRow = ProductionViewGrid.Rows[index];
                    SerialID = view.DataKeys[index].Value.ToString();
                    //Check to see if user is already logged in
                    

                    string pageNameprt = "InspectionReportPrint.aspx";
                    Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?SerialNumID=" + SerialID + "');</script>");
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

                GridView SerialNumbersMain = ProdGrid.FindControl("SerialNumbersMain") as GridView;
                GridView DeliveryViewGrid = ProdGrid.FindControl("DeliveryViewGrid") as GridView;
                GridView GridView2 = ProdGrid.FindControl("GridView2") as GridView;
                GridView GridView3 = ProdGrid.FindControl("GridView3") as GridView;
                GridView CertGrid = ProdGrid.FindControl("CertGrid") as GridView;
                GridView CARView = ProdGrid.FindControl("CARView") as GridView;

                SerialMainSource.SelectCommand = "SELECT Serialization.SerialNumID, Serialization.LotIncrement, Serialization.JobItemID, Serialization.InternalSerialNumber, Serialization.CustSerialNumber, Serialization.OrderDriven, Serialization.ReportGenerated, DeliveryItem.LotNumber FROM Serialization LEFT JOIN DeliveryItem ON Serialization.DeliveryItemID = DeliveryItem.DeliveryItemID WHERE LotNumber = " + JobItemID;

                SerialNumbersMain.DataSource = SerialMainSource;
                SerialNumbersMain.DataBind();

                SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [ID], [Comments], JobItemID, SetupID, SetupImageID FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

                GridView2.DataSource = SqlDataSource4;
                GridView2.DataBind();

                SqlDataSource5.SelectCommand = "SELECT [SubcontractID], [WorkCode], [Quantity], [DueDate], CAST(CASE WHEN [HasDetail]=1 THEN 0 ELSE 1 END As Bit) As [Received] FROM [SubcontractItems] WHERE [JobItemID] = " + JobItemID;

                GridView3.DataSource = SqlDataSource5;
                GridView3.DataBind();

                MonseesSqlDataSourceDeliveries.SelectCommand = "SELECT [JobItemID], [Quantity], [CurrDelivery], [PONumber], [Shipped], [Ready], [Suspended] FROM [Monsees2].[dbo].[FormDeliveries] WHERE JobItemID=" + JobItemID;

                DeliveryViewGrid.DataSource = MonseesSqlDataSourceDeliveries;
                DeliveryViewGrid.DataBind();

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

                SqlDataSource12.SelectCommand = "SELECT * FROM CorrectiveActionView WHERE [DetailID] = " + DetailID;

                CARView.DataSource = SqlDataSource12;
                CARView.DataBind();

                SqlDataSourceCert.SelectCommand = "SELECT [CertCompReqd], [MatlCertReqd], [PlateCertReqd], [SerializationReqd] FROM Version WHERE RevisionID = " + RevisionID;

                CertGrid.DataSource = SqlDataSourceCert;
                CertGrid.DataBind();

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

        protected void ExpandCollapseIndependent(object sender, EventArgs e)
        {
            GridViewRow ProdGrid = (GridViewRow)((Button)sender).Parent.Parent;
            GridView Prod = (GridView)((Button)sender).Parent.Parent.Parent.Parent;
            if (ProdGrid.RowType == DataControlRowType.DataRow)
            {
                index = ProdGrid.RowIndex;
                string JobItemID = ProductionViewGrid.DataKeys[index].Values[0].ToString();
                string DetailID = "0";
                string RevisionID = "0";

                GridView SerialNumbersMain = ProdGrid.FindControl("SerialNumbersMain") as GridView;

                SerialMainSource.SelectCommand = "SELECT Serialization.SerialNumID, Serialization.LotIncrement, Serialization.JobItemID, Serialization.InternalSerialNumber, Serialization.CustSerialNumber, Serialization.OrderDriven, Serialization.ReportGenerated, DeliveryItem.LotNumber FROM Serialization LEFT JOIN DeliveryItem ON Serialization.DeliveryItemID = DeliveryItem.DeliveryItemID WHERE LotNumber = " + JobItemID;

                SerialNumbersMain.DataSource = SerialNumbersMain;
                SerialNumbersMain.DataBind();

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
 

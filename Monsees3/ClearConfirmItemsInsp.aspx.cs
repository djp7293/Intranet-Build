using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Security;
using System.Collections.Generic;
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

using ICSharpCode.SharpZipLib.Zip;
using iTextSharp.text;

using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;

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
    public partial class _Default_ClearConfirmInsp : DataPage
    {

        protected Int32 index;
        protected string MonseesConnectionString;
        protected string[] EmployeeLoginName;
        protected string EmployeeName;
        protected List<EmployeeModel> EmployeeList;
        protected List<DecommissionModel> DecommissionList;
        protected List<GaugeTypeModel> GaugeTypeList;
        protected Int32 EmployeeID;
        public List<InvStatusModel> InvStatusList { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            
            Last_Refreshed.Text = "Last Refreshed : " + DateTime.Now;

            if (!IsPostBack)
            {
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
                
            }
            GetData();
        }

        protected void GetData()
        {

            this.UnitOfWork.Begin();
            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            InvStatusList = inspectionRepository.GetInvStatus();
            EmployeeList = inspectionRepository.GetActiveEmployees();
            DecommissionList = inspectionRepository.GetDecommissionList();
            GaugeTypeList = inspectionRepository.GetGaugeTypes();
            this.UnitOfWork.End();

        }        

        protected void NewJobs_RowCommand(object sender, GridViewCommandEventArgs e)
        {
              
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
                        string pageName = (Page.IsInMappedRole("Office")) ? "JobSummary.aspx" : "JobSummaryViewer.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobID=" + LinkID + "');</script>");
                        break;

                    default:

                        break;

                }
            }
        }

        protected void SubcontractGrid_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewRenew")).Value.ToString()) == 1)
                {
                    e.Row.BackColor = System.Drawing.Color.FromName("#fbffb5");

                }
                if (((HiddenField)e.Row.FindControl("NewPart")).Value != "")
                {
                    if (Convert.ToInt32(((HiddenField)e.Row.FindControl("NewPart")).Value.ToString()) <= 1)
                    {
                        e.Row.BackColor = System.Drawing.Color.FromName("#ffc880");

                    }
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

        protected void Inventory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID;
            string sqlstring;


            Int32 totrows = Inventory.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument);
            //Get the value of column from the DataKeys using the RowIndex.
            LinkID = Inventory.DataKeys[index].Values[1].ToString();

            string command_name = e.CommandName;

            if ((command_name == "Accept") || (command_name == "Reject") || (command_name == "Modify"))
            {
               
                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "Accept":
                        GridViewRow gvRow = Inventory.Rows[index];
                        Int32 InventoryQty = Convert.ToInt32(((Label)gvRow.FindControl("InvQtyLabel")).Text);
                        Int32 LotID = Convert.ToInt32(((Label)gvRow.FindControl("LotLabel")).Text);
                        Int32 Qty = Convert.ToInt32(((Label)gvRow.FindControl("QtyLabel")).Text);
                        string status = ((Label)gvRow.FindControl("StatusLabel")).Text;
                        
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
                        
                        if (status == "Complete") Response.Write("<script type='text/javascript'>window.open('/Reports/Label.aspx?id=" + LotID + "');</script>");
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

       

        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID = GridView1.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            byte[] buffer;
            Guid fileId = Guid.NewGuid();
            switch (e.CommandName)
            {
                case "Attach":
                    FileUpload myFileTest = (FileUpload)GridView1.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("filMyFileTest");
                    if (myFileTest.HasFile)
                    {
                        buffer = new byte[(int)myFileTest.FileContent.Length];
                        myFileTest.FileContent.Read(buffer, 0, buffer.Length);
                        if (myFileTest.FileContent.Length > 0)
                        {
                            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                            {
                                con.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("PlateCertAdd", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@subcontractitemID", LinkID);
                                cmd.Parameters.AddWithValue("@fileType", myFileTest.PostedFile.ContentType);
                                cmd.Parameters.AddWithValue("@ID", fileId);
                                cmd.Parameters.AddWithValue("@Certification", buffer);


                                cmd.ExecuteNonQuery();


                                con.Close();
                            }



                        }
                        GridView1.DataBind();
                        for (int i = 0; i < SubcontractGrid.Rows.Count; i++)
                        {


                            if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                            {
                                SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                            }

                            if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                            {
                                SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                            }


                            if (((HiddenField)SubcontractGrid.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                            {
                                SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                            }

                            if (((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value != "")
                            {
                                string hot = ((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value;
                                if (Convert.ToBoolean(((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value))
                                {
                                    SubcontractGrid.Rows[i].Font.Bold = true;

                                }


                            }


                        }
                    }                   
                    break;
                case "Ignore":
                        string sqlstring = "UPDATE [Subcontract Item] SET IgnoreCertReq = 1 WHERE [SubcontractItemID] = " + LinkID;
                         
                        System.Data.SqlClient.SqlConnection con5 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                        
                        System.Data.SqlClient.SqlCommand comm5 = new System.Data.SqlClient.SqlCommand(sqlstring, con5);
                        
                           
                        con5.Open();

                        // execute sql command and store a return values in reade
                        comm5.ExecuteNonQuery();
                        con5.Close();
                        GridView1.DataBind();
                        for (int i = 0; i < SubcontractGrid.Rows.Count; i++)
                        {


                            if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewRenew")).Value.ToString()) == 1)
                            {
                                SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#fbffb5");

                            }

                            if (Convert.ToInt32(((HiddenField)SubcontractGrid.Rows[i].FindControl("NewPart")).Value.ToString()) <= 1)
                            {
                                SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#ffc880");

                            }


                            if (((HiddenField)SubcontractGrid.Rows[i].FindControl("CAbbr")).Value.ToString() == "MG")
                            {
                                SubcontractGrid.Rows[i].BackColor = System.Drawing.Color.FromName("#8CFF8C");

                            }

                            if (((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value != "")
                            {
                                string hot = ((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value;
                                if (Convert.ToBoolean(((HiddenField)SubcontractGrid.Rows[i].FindControl("Hot")).Value))
                                {
                                    SubcontractGrid.Rows[i].Font.Bold = true;

                                }


                            }


                        }
                    break;
                default:
                    break;
            }
        }

        protected void SubcontractGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            bool check;



            string command_name = e.CommandName;

            if ((command_name == "Received"))
            {
                int index = Convert.ToInt32(e.CommandArgument);
                string SubcontractItemID;

                gvRow = SubcontractGrid.Rows[index];
                SubcontractItemID = SubcontractGrid.DataKeys[index].Value.ToString();

                System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("ReceiveSubcontract", con3);
                comm3.CommandType = System.Data.CommandType.StoredProcedure;
                comm3.Parameters.AddWithValue("@SubItemID", SubcontractItemID);
                con3.Open();
                comm3.ExecuteNonQuery();
                con3.Close();
                SubcontractGrid.DataBind();


            }
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LinkID = GridView2.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            byte[] buffer;
            Guid fileId = Guid.NewGuid();
            switch (e.CommandName)
            {
                case "Attach":
                    FileUpload myFileTest = (FileUpload)GridView2.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("filMyFileTest");
                    if (myFileTest.HasFile)
                    {
                        buffer = new byte[(int)myFileTest.FileContent.Length];
                        myFileTest.FileContent.Read(buffer, 0, buffer.Length);
                        if (myFileTest.FileContent.Length > 0)
                        {
                            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                            {
                                con.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("MaterialCertAdd", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@MatPriceID", LinkID);
                                cmd.Parameters.AddWithValue("@fileType", myFileTest.PostedFile.ContentType);
                                cmd.Parameters.AddWithValue("@ID", fileId);
                                cmd.Parameters.AddWithValue("@Certification", buffer);


                                cmd.ExecuteNonQuery();


                                con.Close();
                            }



                        }
                        GridView2.DataBind();
                    }
                    break;
                case "Ignore":
                    string sqlstring = "UPDATE [Material_Price2] SET IgnoreCertReq = 1 WHERE [MatPriceID] = " + LinkID;

                    System.Data.SqlClient.SqlConnection con5 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                    System.Data.SqlClient.SqlCommand comm5 = new System.Data.SqlClient.SqlCommand(sqlstring, con5);


                    con5.Open();

                    // execute sql command and store a return values in reade
                    comm5.ExecuteNonQuery();
                    con5.Close();
                    GridView2.DataBind();
                    break;
                default:
                    break;
            }
        }

        protected void GridView6_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            GridViewRow gvRow;
            string LotID;

            string command_name = e.CommandName;
            GridView view = sender as GridView;

          
                Int32 totrows = view.Rows.Count;
                index = Convert.ToInt32(e.CommandArgument) % totrows;

                //TO DO: Check to see if the user is already logged into the given job

                switch (e.CommandName)
                {
                    case "ViewReport":

                        gvRow = view.Rows[index];
                        LotID = gvRow.Cells[0].Text;
                        //Check to see if user is already logged in
                        //MessageBox("The index fired is " + index);
                        string pageName = (Page.IsInMappedRole("Inspection")) ? "InspectionReportPrint.aspx" : "InspectionReportPrint.aspx";
                        Response.Write("<script type='text/javascript'>window.open('" + pageName + "?JobItemID=" + LotID + "');</script>");
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

                    case "PQueue":
                        gvRow = view.Rows[index];
                        LotID = gvRow.Cells[0].Text;
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con2 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con2.Open();
                        int result2;
                        
                        System.Data.SqlClient.SqlCommand comm3 = new System.Data.SqlClient.SqlCommand("RetToPlateQueue", con2);
                        comm3.CommandType = CommandType.StoredProcedure;


                        comm3.Parameters.AddWithValue("@LotNumber", LotID);

                        result2 = comm3.ExecuteNonQuery();
                        DataBind();

                        break;

                    case "MQueue":
                        gvRow = view.Rows[index];
                        LotID = gvRow.Cells[0].Text;
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con3.Open();
                        int result3;
                        
                        System.Data.SqlClient.SqlCommand comm4 = new System.Data.SqlClient.SqlCommand("RetToMatQueue", con3);
                        comm4.CommandType = CommandType.StoredProcedure;


                        comm4.Parameters.AddWithValue("@LotNumber", LotID);

                        result3 = comm4.ExecuteNonQuery();
                        DataBind();

                        break;

                    default:

                        break;

                }
           
        }

        protected void GridView4_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               // try
               // {
                    index = e.Row.RowIndex;
                    
                    GridView DeliveryViewGrid = e.Row.FindControl("GridView5") as GridView;
                    string PackingListID = ((GridView)sender).DataKeys[index].Values[0].ToString();
                    MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    ClearShipmentSub.ConnectionString = MonseesConnectionString;
                    ClearShipmentSub.SelectCommand = "SELECT * FROM [ShipmentClearSub] WHERE PackingListID=" + PackingListID;
                    DeliveryViewGrid.DataSource = ClearShipmentSub;
                    DeliveryViewGrid.DataBind();
               // }
                //catch (Exception f)
               // {

               // }
            }
        }

        protected void GridView5_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               // try
               // {
                    index = e.Row.RowIndex;
                    
                    GridView LotViewGrid = e.Row.FindControl("GridView6") as GridView;
                    string DeliveryID = ((GridView)sender).DataKeys[index].Values[0].ToString();
                    MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    DeliveryLots.ConnectionString = MonseesConnectionString;
                    DeliveryLots.SelectCommand = "SELECT [LotNumber], [DeliveryItemID], [Quantity], [JobNumber], [RTS], [PCert], [MCert], ITAR FROM [FormLots] WHERE DeliverID=" + DeliveryID;
                    LotViewGrid.DataSource = DeliveryLots;
                    LotViewGrid.DataBind();
              //  }
              //  catch (Exception f)
              //  {

              //  }
            }
        }

        protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based

            string LinkID;
            GridView view = (GridView)sender;
            Int32 totrows = view.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument) % totrows;
            //Get the value of column from the DataKeys using the RowIndex.
            
            
            LinkID = view.DataKeys[index].Values[0].ToString();

            string command_name = e.CommandName;

            if ((command_name == "Clear"))
            {

                switch (e.CommandName)
                {
                    case "Clear":
                        
                        
                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con3.Open();
                        int result3;
                        string sqltxt = "UPDATE Delivery SET Clear = 1 WHERE DeliveryID = @DeliveryID";
                        System.Data.SqlClient.SqlCommand comm4 = new System.Data.SqlClient.SqlCommand(sqltxt, con3);

                        comm4.CommandType = CommandType.Text;


                        comm4.Parameters.AddWithValue("@DeliveryID", LinkID);

                        result3 = comm4.ExecuteNonQuery();
                        DataBind();
                        break;

                    default:

                        break;

                }
            }
        }

        protected void GridView4_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based

            string LinkID;
            GridView view = (GridView)sender;
            Int32 totrows = view.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument) % totrows;
            List<FileList> filepaths = new List<FileList>();
            FileList record = new FileList();
            //Get the value of column from the DataKeys using the RowIndex.


            LinkID = view.DataKeys[index].Values[0].ToString();

            string command_name = e.CommandName;

            if ((command_name == "Clear") || (command_name == "MailZip"))
            {

                switch (e.CommandName)
                {
                    case "Clear":


                        MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

                        System.Data.SqlClient.SqlConnection con3 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                        con3.Open();
                        int result3;
                        string sql1 = "UPDATE Delivery SET Clear=1 WHERE PackingListID = @PackingList";
                        System.Data.SqlClient.SqlCommand comm4 = new System.Data.SqlClient.SqlCommand(sql1, con3);

                        comm4.CommandType = CommandType.Text;


                        comm4.Parameters.AddWithValue("@PackingList", LinkID);

                        result3 = comm4.ExecuteNonQuery();
                        DataBind();
                        break;

                    case "MailZip":
                       
                        SqlConnection objSqlCon = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                        objSqlCon.Open();
                        SqlTransaction objSqlTran = objSqlCon.BeginTransaction();

                        //objSqlCmd = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", objSqlCon, objSqlTran);
                           
                        //byte[] objContext = (byte[])objSqlCmd.ExecuteScalar();                       
                           
                        // HttpContext.Current.Response.ContentType = "application/octet-stream";
                        // HttpContext.Current.Response.AddHeader("Content-disposition", "attachment; filename=\"" + PartNumber + " - Rev" + RevNumber + ".PDF\"");
                        // Here you need to manage the download file stuff according to your need

                           
                        // HttpContext.Current.Response.BinaryWrite(buffer);
                        // HttpContext.Current.Response.Flush();
                        //THIS IS THE END OF THE ASHX FILE

                        Response.AddHeader("Content-Disposition", "attachment; filename=PackingList" + LinkID + ".zip");
                        Response.ContentType = "application/zip";

                        using (var zipStream = new ZipOutputStream(Response.OutputStream))
                        {
                            int indexval;
                            string sqlcommand;
                            sqlcommand = "SELECT dbo.Delivery.PackingListID, dbo.[Job Item].JobItemID FROM dbo.[Job Item] RIGHT OUTER JOIN dbo.DeliveryItem ON dbo.[Job Item].JobItemID = dbo.DeliveryItem.LotNumber RIGHT OUTER JOIN dbo.Delivery ON dbo.DeliveryItem.DeliverID = dbo.Delivery.DeliveryID WHERE (dbo.Delivery.PackingListID = "+LinkID+")";
                            SqlCommand objSqlCmd3 = new SqlCommand(sqlcommand, objSqlCon, objSqlTran);

                            using (SqlDataReader sdr = objSqlCmd3.ExecuteReader())
                            {

                                while (sdr.Read())
                                {
                                    string JobItemID = sdr[0].ToString();
                                    
                                    WebClient client = new WebClient();
                                    String htmlCode = client.DownloadString("/InspectionReportPrint.aspx?JobItemId=" + JobItemID);
                                    String filename = JobItemID + " Inspection";
                                    CreatePDFDocument(htmlCode, filename);

                                    byte[] fileBytes = System.IO.File.ReadAllBytes(JobItemID + " Inspection");                                    

                                    var fileEntry = new ZipEntry(JobItemID + " Inspection.pdf")
                                    {
                                        Size = fileBytes.Length
                                    };

                                    zipStream.PutNextEntry(fileEntry);
                                    zipStream.Write(fileBytes, 0, fileBytes.Length);
                                    
                                    
                                }
                            }

                            SqlCommand objSqlCmd = new SqlCommand("GetPlateCertsForZip", objSqlCon, objSqlTran);
                            objSqlCmd.CommandType = CommandType.StoredProcedure;
                            SqlParameter objSqlParam1 = new SqlParameter("@packinglist", SqlDbType.VarChar);
                            objSqlParam1.Value = LinkID;
                            objSqlCmd.Parameters.Add(objSqlParam1);

                            using (SqlDataReader sdr = objSqlCmd.ExecuteReader())
                            {

                                while (sdr.Read())
                                {
                                    record.ID = Convert.ToInt32(sdr[0].ToString());
                                    record.revisionID = Convert.ToInt32(sdr[1].ToString());
                                    record.revision = sdr[2].ToString();
                                    record.part = sdr[3].ToString();
                                    record.filetype = sdr[4].ToString();
                                    filepaths.Add(record);
                                }
                            }
                            indexval = 0;
                            foreach (FileList filePath in filepaths)
                            {
                                
                                //FileStream objSqlFileStream = new FileStream(filePath.part + " - Rev" + filePath.revision, FileMode.OpenOrCreate, FileAccess.Write);
                                //BinaryWriter bw = new BinaryWriter(objSqlFileStream);
                                //long FileSize = objSqlFileStream.Length;
                                
                                sqlcommand = "SELECT Certification FROM PlatingCerts WHERE ID = " + filePath.ID;
                                SqlConnection con1 = new SqlConnection(MonseesConnectionString);
                                SqlCommand comm1 = new SqlCommand(sqlcommand, con1);
                                SqlDataAdapter dp = new SqlDataAdapter(comm1);
                                DataSet ds = new DataSet("MyImages");

                                byte[] fileBytes = new byte[0];

                                dp.Fill(ds, "MyImages");
                                DataRow myRow;
                                myRow = ds.Tables["MyImages"].Rows[0];

                                fileBytes = (byte[])myRow["Certification"];
                                //FileStream objSqlFileStream2 = new FileStream(filePath.part + " - Rev" + filePath.revision, FileMode.Open, FileAccess.Read);
                               
                                //objSqlFileStream2.Read(fileBytes, 0, fileBytes.Length);
                                //objSqlFileStream2.Close();
                                //objSqlTran.Commit();
                                //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath.path);

                                var fileEntry = new ZipEntry(filePath.part + " - Rev" + filePath.revision + " - plate00" + indexval + "." + filePath.filetype)
                                {
                                    Size = fileBytes.Length
                                };

                                zipStream.PutNextEntry(fileEntry);
                                zipStream.Write(fileBytes, 0, fileBytes.Length);
                                indexval = indexval + 1;
                            }

                            filepaths.Clear();

                            SqlCommand objSqlCmd2 = new SqlCommand("GetMatlCertsForZip", objSqlCon, objSqlTran);
                            objSqlCmd2.CommandType = CommandType.StoredProcedure;
                            objSqlParam1 = new SqlParameter("@packinglist", SqlDbType.VarChar);
                            objSqlParam1.Value = LinkID;
                            objSqlCmd2.Parameters.Add(objSqlParam1);


                            using (SqlDataReader sdr = objSqlCmd2.ExecuteReader())
                            {

                                while (sdr.Read())
                                {
                                    record.ID = Convert.ToInt32(sdr[0].ToString());
                                    record.revisionID = Convert.ToInt32(sdr[1].ToString());
                                    record.revision = sdr[2].ToString();
                                    record.part = sdr[3].ToString();
                                    record.filetype = sdr[4].ToString();
                                    filepaths.Add(record);
                                }
                            }
                            objSqlCon.Close();
                            indexval = 0;
                            foreach (FileList filePath in filepaths)
                            {

                                //FileStream objSqlFileStream = new FileStream(filePath.part + " - Rev" + filePath.revision, FileMode.OpenOrCreate, FileAccess.Write);
                                //BinaryWriter bw = new BinaryWriter(objSqlFileStream);
                                //long FileSize = objSqlFileStream.Length;

                                sqlcommand = "SELECT MaterialCert FROM MaterialCerts WHERE SerialNumber = " + filePath.ID;
                                SqlConnection con1 = new SqlConnection(MonseesConnectionString);
                                SqlCommand comm1 = new SqlCommand(sqlcommand, con1);
                                SqlDataAdapter dp = new SqlDataAdapter(comm1);
                                DataSet ds = new DataSet("MyImages");

                                byte[] fileBytes = new byte[0];

                                dp.Fill(ds, "MyImages");
                                DataRow myRow;
                                myRow = ds.Tables["MyImages"].Rows[0];

                                fileBytes = (byte[])myRow["MaterialCert"];
                                //FileStream objSqlFileStream2 = new FileStream(filePath.part + " - Rev" + filePath.revision, FileMode.Open, FileAccess.Read);

                                //objSqlFileStream2.Read(fileBytes, 0, fileBytes.Length);
                                //objSqlFileStream2.Close();
                                //objSqlTran.Commit();
                                //byte[] fileBytes = System.IO.File.ReadAllBytes(filePath.path);

                                var fileEntry = new ZipEntry(filePath.part + " - Rev" + filePath.revision + " - matl00"+indexval+"."+filePath.filetype)
                                {
                                    Size = fileBytes.Length
                                };

                                zipStream.PutNextEntry(fileEntry);
                                zipStream.Write(fileBytes, 0, fileBytes.Length);
                                indexval = indexval + 1;
                            }
                            zipStream.Flush();
                            zipStream.Close();
                        }

                        break;

                    default:

                        break;

                }
            }
        }

        protected void Inventory_RowEditing(object sender, GridViewEditEventArgs e)
        {

            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            gvwChild.EditIndex = e.NewEditIndex;
        }

        protected void Inventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string MonseesConnectionString;


            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);

            GridViewRow gvrow = (GridViewRow)gvwChild.Rows[e.RowIndex];

            TextBox InvQtyBox = (TextBox)gvrow.FindControl("InventoryQty");
            TextBox QtyBox = (TextBox)gvrow.FindControl("Qty");

            DropDownList InvStatusList = (DropDownList)gvrow.FindControl("StatusList");

            string DelvryItemID = gvwChild.DataKeys[Convert.ToInt32(e.RowIndex)].Values[1].ToString();
            string InvID = gvwChild.DataKeys[Convert.ToInt32(e.RowIndex)].Values[0].ToString();
           
            gvwChild.EditIndex = -1;

            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

            con.Open();
            int result;
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand("ModifyInventory", con);
            comm2.CommandType = CommandType.StoredProcedure;

            comm2.Parameters.AddWithValue("@DeliveryItemID", DelvryItemID);
            comm2.Parameters.AddWithValue("@InventoryID", InvID);
            comm2.Parameters.AddWithValue("@Quantity", Convert.ToInt32(QtyBox.Text));
            comm2.Parameters.AddWithValue("@Status", Convert.ToInt32(InvStatusList.SelectedValue));
            comm2.Parameters.AddWithValue("@InventoryQuantity", Convert.ToInt32(InvQtyBox.Text));
            

                result = comm2.ExecuteNonQuery();


           
           
                con.Close();
                gvwChild.DataBind();
            

        }

       
        protected void Inventory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            System.Web.UI.WebControls.GridView gvwChild = ((System.Web.UI.WebControls.GridView)sender);
            gvwChild.EditIndex = -1;
        }

        protected void Inventory_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridView Grid = (GridView)sender;
            DropDownList dpl;
            Int32 i = Grid.EditIndex;
            GridViewRow row = e.Row;
            if (i > -1)
            {

                dpl = (DropDownList)row.FindControl("StatusList");

                if (dpl != null)
                {
                    dpl.DataSource = InvStatusList;
                    dpl.DataBind();
                    string val = ((HiddenField)row.FindControl("hdStatusList")).Value.Trim();
                    dpl.SelectedValue = PreventUnlistedValueError(dpl, val);
                }
            }
        }


        protected string PreventUnlistedValueError(DropDownList li, string val)
        {
            if (li.Items.FindByValue(val) == null)
            {
                //option 1: add the value to the list and display
                if (val == "") val = "0";
                System.Web.UI.WebControls.ListItem lit = new System.Web.UI.WebControls.ListItem();
                lit.Text = val;
                lit.Value = val;
                li.Items.Insert(li.Items.Count, lit);
                //option 2: set a default e.g.
                //val="";
            }
            return val;
        }

       

        private void CreatePDFDocument(string strHtml, string filename)
        {

            string strFileName = HttpContext.Current.Server.MapPath(filename+".pdf");
            // step 1: creation of a document-object
            Document document = new Document();
            // step 2:
            // we create a writer that listens to the document
            PdfWriter.GetInstance(document, new FileStream(strFileName, FileMode.Create));
            StringReader se = new StringReader(strHtml);
            HTMLWorker obj = new HTMLWorker(document);
            document.Open();
            obj.Parse(se);
            document.Close();
           
        }

        protected void KeepExpanded(System.Web.UI.WebControls.GridView gvwChild, object sender)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.GridView)sender).Parent.Parent as GridViewRow;
            BindChildgvwChildView(GridView1.DataKeys[gvRowParent.RowIndex].Value.ToString(), gvwChild);
            if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent.Cells[1].Text + "').style.display = 'inline';", true);
            }
        }

        private void BindChildgvwChildView(string jobitemId, System.Web.UI.WebControls.GridView gvChild)
        {
            string JobItemID = jobitemId;
            //SqlDataSource4.SelectCommand = "SELECT [JobSetupID], [OperationID], [ProcessOrder], [Label], [Setup Cost] AS Setup_Cost, [Operation Cost] AS Operation_Cost, [Completed], Name, QtyIn, QtyOut, Hours, [JobItemID], [ID], [Comments] FROM [JobItemSetupSummary] WHERE [JobItemID] = " + JobItemID + " ORDER BY [ProcessOrder]";

            //gvChild.DataSource = SqlDataSource4;
            //gvChild.DataBind();
        }

        protected void CompleteCal_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView GridView2 = gvRowParent.FindControl("GridView7") as GridView;
            DropDownList dpl = (DropDownList)gvRowParent.FindControl("PerformedBy");
            if (dpl != null)
            {
                dpl.DataSource = EmployeeList;
                dpl.DataBind();

            }
            dpl = (DropDownList)gvRowParent.FindControl("DecommCode");
            if (dpl != null)
            {
                dpl.DataSource = DecommissionList;
                dpl.DataBind();

            }
          
        }

        protected void CancelCompleteCal_Command(object sender, CommandEventArgs e)
        {
                        
        }

        protected void CompleteCalNow_Command(object sender, CommandEventArgs e)
        {
            
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            int GaugeID = Convert.ToInt32(gvRowParent.Cells[0].Text);
            DropDownList DecommissionCodeList = (DropDownList)gvRowParent.FindControl("DecommCode");
            CheckBox NIST = (CheckBox)gvRowParent.FindControl("NIST");
            CheckBox Success = (CheckBox)gvRowParent.FindControl("Success");
            CheckBox Decomm = (CheckBox)gvRowParent.FindControl("Decomm");
            DropDownList EmployeeAddList = (DropDownList)gvRowParent.FindControl("PerformedBy");
            
            
            
            int createemployee = Convert.ToInt32(EmployeeAddList.SelectedValue.ToString());
            int active = Convert.ToInt32(!Decomm.Checked);
            int traceable = Convert.ToInt32(NIST.Checked);
            int SuccessVal = Convert.ToInt32(Success.Checked);

            string decomm = DecommissionCodeList.SelectedValue.ToString();


            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand("CompleteCalibration", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@GaugeID", GaugeID);
            com.Parameters.AddWithValue("@PerformedBy", Convert.ToInt32(createemployee));
            com.Parameters.AddWithValue("@esig", EmployeeID);
            com.Parameters.AddWithValue("@traceable", traceable );
            com.Parameters.AddWithValue("@successful", SuccessVal );
            com.Parameters.AddWithValue("@active", active );
            com.Parameters.AddWithValue("@decommcode", decomm);

            con.Open();
            com.ExecuteNonQuery();


            con.Close();
            GridView7.DataBind();
           
        }

        protected void GridView8_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                
                    index = e.Row.RowIndex;
                    string DeliveryID = GridView8.DataKeys[index].Values[0].ToString();
                    GridView RMALotView = e.Row.FindControl("RMALotView") as GridView;
                    MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                    SqlDataSource4.ConnectionString = MonseesConnectionString;
                    SqlDataSource4.SelectCommand = "SELECT [LotNumber], [Quantity], [JobNumber], [RTS], [PCert], [MCert], InactiveInventory, RTSInventory FROM [FormLots] WHERE DeliverID=" + DeliveryID;
                    RMALotView.DataSource = SqlDataSource4;
                    RMALotView.DataBind();
               
               
            }
        }

        protected void RMALotView_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string LotID;
            GridView RMALotView = (GridView)sender;
            Int32 totrows = RMALotView.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument) % totrows;
            //Get the value of column from the DataKeys using the RowIndex.
            LotID = RMALotView.DataKeys[index].Value.ToString();

            string command_name = e.CommandName;

            switch (e.CommandName)
            {
                case "InitiateCA":
                    string pageName = (Page.IsInMappedRole("Inspection")) ? "CARInitiate.aspx" : "CARInitiate.aspx";
                    Response.Write("<script type='text/javascript'>window.open('" + pageName + "?id=" + LotID + "');</script>");
                    DataBind();
                    break;
                case "ReworkLot":
                    string pageName1 = (Page.IsInMappedRole("Inspection")) ? "ReworkLot.aspx" : "ReworkLot.aspx";
                    Response.Write("<script type='text/javascript'>window.open('" + pageName1 + "?id=" + LotID + "');</script>");
                    DataBind();
                    break;

                default:

                    break;

            }
            
        }

        protected void GridView10_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string CARID;
            GridView CARView = (GridView)sender;
            Int32 totrows = CARView.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument) % totrows;
            //Get the value of column from the DataKeys using the RowIndex.
            CARID = CARView.DataKeys[index].Value.ToString();

            string command_name = e.CommandName;

            switch (e.CommandName)
            {
                case "ManageCAR":
                    string pageName = (Page.IsInMappedRole("Inspection")) ? "CARComplete.aspx" : "CARComplete.aspx";
                    Response.Write("<script type='text/javascript'>window.open('" + pageName + "?id=" + CARID + "');</script>");
                    DataBind();
                    break;
                
                default:

                    break;

            }

        }

        protected void GridView9_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string CARID;
            GridView CARView = (GridView)sender;
            Int32 totrows = CARView.Rows.Count;
            index = Convert.ToInt32(e.CommandArgument) % totrows;
            //Get the value of column from the DataKeys using the RowIndex.
            CARID = CARView.DataKeys[index].Value.ToString();

            string command_name = e.CommandName;

            switch (e.CommandName)
            {
                case "Reject":
                    
                    string sqlstring = "DELETE FROM CorrectiveAction WHERE CARID = @CARID";

                    SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

                    SqlCommand com = new SqlCommand(sqlstring, con);

                    com.Parameters.AddWithValue("@CARID", Convert.ToInt32(CARID));

                    con.Open();
                    com.CommandType = CommandType.Text;
                    com.ExecuteNonQuery();

                    con.Close();
                    DataBind();
                    break;

                default:

                    break;

            }

        }
        protected void AddCalButton_Command(object sender, CommandEventArgs e)
        {
            if (TypeDropDown != null)
            {
                TypeDropDown.DataSource = GaugeTypeList;
                TypeDropDown.DataBind();
            }
            if (OwnerDropDown != null)
            {
                OwnerDropDown.DataSource = EmployeeList;
                OwnerDropDown.DataBind();
            }
        }

        protected void addequipbutton_Command(object sender, CommandEventArgs e)
        {
            int owner = Convert.ToInt32(TypeDropDown.SelectedValue.ToString());
            int active = Convert.ToInt32(activecheck.Checked);
            int inspequip = Convert.ToInt32(inspequipcheck.Checked);
            

           
            SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand("AddInspEquip", con);
            com.CommandType = System.Data.CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@Serial", SerialBox.Text);
            com.Parameters.AddWithValue("@Description", DescBox.Text);
            com.Parameters.AddWithValue("@Location", LocBox.Text);
            com.Parameters.AddWithValue("@GageTypeID", Convert.ToInt32(TypeDropDown.SelectedValue.ToString()));
            com.Parameters.AddWithValue("@Notes", NotesBox.Text);
            com.Parameters.AddWithValue("@Active", active);
            com.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(OwnerDropDown.SelectedValue.ToString()));
            com.Parameters.AddWithValue("@Resolution", ResBox.Text);
            com.Parameters.AddWithValue("@InspOfficeEquip", NotesBox.Text);

            con.Open();
            com.ExecuteNonQuery();


            con.Close();
            GridView7.DataBind();
        }

        protected void EmployeeToCAR_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            GridView gvParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent.Parent.Parent as GridView;
           


            string CARID = gvParent.DataKeys[gvRowParent.RowIndex].Value.ToString();
            DropDownList ImpEmpl = (DropDownList)gvRowParent.FindControl("ImpEmplDropdown");
            
            string ImpEmployee = ImpEmpl.SelectedValue.ToString();
            string sqlstring = "UPDATE CorrectiveAction SET ImpEmployee = @EmployeeID, Initiated=1 WHERE CARID = @CARID";

            SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            SqlCommand com = new SqlCommand(sqlstring, con);

            com.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(ImpEmployee));
           

            com.Parameters.AddWithValue("@CARID", Convert.ToInt32(CARID));

            con.Open();
            com.CommandType = CommandType.Text;
            com.ExecuteNonQuery();

            con.Close();


            gvParent.DataBind();
            DataBind();

            /*if (!ClientScript.IsStartupScriptRegistered("ChildGridIndex"))
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ChildGridIndex", "document.getElementById('div" + gvRowParent3.Cells[2].Text + "').style.display = 'inline';", true);
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


            }*/
        }

        protected void Unnamed_Command(object sender, CommandEventArgs e)
        {
            GridViewRow gvRowParent = ((System.Web.UI.WebControls.Button)sender).Parent.Parent.Parent.Parent as GridViewRow;
            DropDownList ImpEmpl = (DropDownList)gvRowParent.FindControl("ImpEmplDropdown");
            this.UnitOfWork.Begin();
            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            
            EmployeeList = inspectionRepository.GetActiveEmployees();
            
            this.UnitOfWork.End();
            ImpEmpl.DataSource = EmployeeList;
            ImpEmpl.DataBind();
        }
    }
}

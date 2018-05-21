using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Management;
using BasicFrame.WebControls;
using System.Net.Mail;
using System.Text;
using Monsees.Database;
using Monsees.DataModel;
using Monsees.Pages;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using System.Collections.Generic;




namespace Monsees
{
    public partial class AddFixture : DataPage
    {
        
        private Int32 POID;
        public JobDetailModel JobDetailModel { get; set; }
        public List<OperationModel> OperationList { get; set; }
        public List<MaterialModel> MaterialList { get; set; }
        public List<DimensionModel> DimensionList { get; set; }
        public List<ContactModel> OwnerList { get; set; }
        public List<MaterialSizeModel> SizeList { get; set; }
        public List<DeliveryModel> DeliveryList { get; set; }
        public List<WorkcodeModel> WorkcodeList { get; set; }
        public string SourceLot;
        public string SourceSetup="0";       
        private string MonseesConnectionString;
        private DropDownList Owner = new DropDownList();
        private DropDownList OperationDropDownList = new DropDownList();
        private DropDownList Material = new DropDownList();
        private DropDownList MaterialDim = new DropDownList();
        private DropDownList MaterialSize = new DropDownList();
        private DropDownList Plating = new DropDownList();
        private DropDownList HeatTreat = new DropDownList();


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
            if (Request.QueryString["SourceLot"] != null)
                SourceLot = Request.QueryString["SourceLot"];
            if (Request.QueryString["SourceSetup"] != null)
                SourceSetup = Request.QueryString["SourceSetup"];

           
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            MonseesSqlDataSourceFixtureOrders.ConnectionString = MonseesConnectionString;
            MonseesSqlDataSourceFixtureInventory.ConnectionString = MonseesConnectionString;

            MonseesSqlDataSourceFixtureOrders.EnableCaching = false;
            MonseesSqlDataSourceFixtureOrders.SelectCommand = "Select * From FixtureOrders WHERE SourceLot = " + SourceLot;
            MonseesSqlDataSourceFixtureInventory.EnableCaching = false;
            MonseesSqlDataSourceFixtureInventory.SelectCommand = "Select * From FixtureInvSummary WHERE SourceLot = " + SourceLot;





                string sqlstring2 = "Select JobItemID FROM JobSetup WHERE JobSetupID = " + SourceSetup + ";";
                
                System.Data.SqlClient.SqlConnection con1 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                System.Data.SqlClient.SqlCommand comm1 = new System.Data.SqlClient.SqlCommand(sqlstring2, con1);
                System.Data.SqlClient.SqlDataReader reader1;
                con1.Open();

                reader1 = comm1.ExecuteReader();

                while (reader1.Read())
                {
                    SourceLot = reader1["JobItemID"].ToString();
                }
                con1.Close(); System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                string sqlstring = "Select [POID] FROM [Purchase Order] WHERE [SourceLot] = " + SourceLot + ";";
                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
                System.Data.SqlClient.SqlDataReader reader;
                con.Open();

                reader = comm.ExecuteReader();

                while (reader.Read())
                {
                    POID = Convert.ToInt32(reader["POID"].ToString());
                }
                con.Close();
                OwnerList = new List<ContactModel>();

            GetData();
            if (!IsPostBack)
                {

                

                int[] nos = new int[1];
                for (int i = 0; i < nos.Length; i++)
                    nos[i] = i + 1;

                FormView1.DataSource = nos;
                FormView1.DataBind();


                OperationDropDownList = (DropDownList)FormView1.FindControl("OperationDropDownList");
                OperationDropDownList.DataSource = OperationList;
                if(SourceSetup != "0") OperationDropDownList.SelectedValue = SourceSetup;
                OperationDropDownList.DataBind();

                Owner = (DropDownList)FormView1.FindControl("Owner");
                Owner.DataSource = OwnerList;
                Owner.DataBind();

                Material = (DropDownList)FormView1.FindControl("Material");
                Material.DataSource = MaterialList;
                Material.DataBind();

                MaterialDim = (DropDownList)FormView1.FindControl("MaterialDim");
                MaterialDim.DataSource = DimensionList;
                MaterialDim.DataBind();

                MaterialSize = (DropDownList)FormView1.FindControl("MaterialSize");
                MaterialSize.DataSource = SizeList;
                MaterialSize.DataBind();

                Plating = (DropDownList)FormView1.FindControl("Plating");
                Plating.DataSource = WorkcodeList;
                Plating.DataBind();

                HeatTreat = (DropDownList)FormView1.FindControl("HeatTreat");
                HeatTreat.DataSource = WorkcodeList;

                HeatTreat.DataBind();
            }
        }

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            JobDetailModel = inspectionRepository.GetJobDetailModelByJobItemId(Convert.ToInt32(SourceLot));
            OwnerList = inspectionRepository.GetContactsbyCustomerId(988);
            MaterialList = inspectionRepository.GetMaterials();
            DimensionList = inspectionRepository.GetDimensions();
            SizeList = inspectionRepository.GetMaterialSizes();
            OperationList = inspectionRepository.GetOperationsByJobItemId(Convert.ToInt32(SourceLot));
            WorkcodeList = inspectionRepository.GetWorkcodes();

            this.UnitOfWork.End();
        }
                
        protected void btnInsert_Click(object sender, EventArgs e)
        {
            Guid fileId;
            System.Data.SqlClient.SqlConnection con1;
            System.Data.SqlClient.SqlCommand cmd1;
            TextBox PartNumber1;
            TextBox Description1;
            TextBox Qty;
            CheckBox IsTurn;
            TextBox Turn;
            CheckBox IsMill;
            TextBox Mill;
            CheckBox IsMultiaxis;
            TextBox Multiaxis;
            CheckBox IsGrind;
            TextBox Grind;
            CheckBox IsWireEDM;
            TextBox WireEDM;
            FileUpload myFileTest;
            BDPLite Delivery1;
            DropDownList FirstOpReq;
            DropDownList Plating;
            DropDownList HeatTreat;
            DropDownList Owner;
            DropDownList Material;
            DropDownList MaterialDim;
            DropDownList MaterialSize;
            TextBox Length;
            CheckBox IsPlating;
            CheckBox IsHeatTreat;

            int IsTurnVal;
            int IsMillVal;
            int IsMultiaxisVal;
            int IsGrindVal;
            int IsWireEDMVal;
            int IsPlatingVal;
            int IsHeatTreatVal;
            int ret;

            byte[] buffer;

           


                PartNumber1 = (TextBox)FormView1.FindControl("PartNumber1");
                Description1 = (TextBox)FormView1.FindControl("Description1");
                Qty = (TextBox)FormView1.FindControl("Quantity1");
                IsTurn = (CheckBox)FormView1.FindControl("IsTurn");
                Turn = (TextBox)FormView1.FindControl("Turn");
                IsMill = (CheckBox)FormView1.FindControl("IsMill");
                Mill = (TextBox)FormView1.FindControl("Mill");
                IsMultiaxis = (CheckBox)FormView1.FindControl("IsMultiaxis");
                Multiaxis = (TextBox)FormView1.FindControl("Multiaxis");
                IsGrind = (CheckBox)FormView1.FindControl("IsGrind");
                Grind = (TextBox)FormView1.FindControl("Grind");
                IsWireEDM = (CheckBox)FormView1.FindControl("IsWireEDM");
                WireEDM = (TextBox)FormView1.FindControl("WireEDM");
                myFileTest = (FileUpload)FormView1.FindControl("filMyFileTest");
                Delivery1 = (BDPLite)FormView1.FindControl("Delivery1");
                FirstOpReq = (DropDownList)FormView1.FindControl("OperationDropDownList");
                Plating = (DropDownList)FormView1.FindControl("Plating");
                HeatTreat = (DropDownList)FormView1.FindControl("HeatTreat");
                Owner = (DropDownList)FormView1.FindControl("Owner");
                Material = (DropDownList)FormView1.FindControl("Material");
                MaterialDim = (DropDownList)FormView1.FindControl("MaterialDim");
                MaterialSize = (DropDownList)FormView1.FindControl("MaterialSize");
                Length = (TextBox)FormView1.FindControl("Length");
                IsPlating = (CheckBox)FormView1.FindControl("IsPlating");
                IsHeatTreat = (CheckBox)FormView1.FindControl("IsHeatTreat");

                IsTurnVal = 0;
                if (IsTurn.Checked) 
                { 
                    IsTurnVal = 1;
                }
                else
                {
                    IsTurnVal = 0;
                }

                IsMillVal = 0;
                if (IsMill.Checked)
                {
                    IsMillVal = 1;
                }
                else
                {
                    IsMillVal = 0;
                }

                IsMultiaxisVal = 0;
                if (IsMultiaxis.Checked)
                {
                    IsMultiaxisVal = 1;
                }
                else
                {
                    IsMultiaxisVal = 0;
                }

                IsGrindVal = 0;
                if (IsGrind.Checked)
                {
                    IsGrindVal = 1;
                }
                else
                {
                    IsGrindVal = 0;
                }

                IsWireEDMVal = 0;
                if (IsWireEDM.Checked)
                {
                    IsWireEDMVal = 1;
                }
                else
                {
                    IsWireEDMVal = 0;
                }

                IsPlatingVal = 0;
                if (IsPlating.Checked)
                {
                    IsPlatingVal = 1;
                }
                else
                {
                    IsPlatingVal = 0;
                }

                IsHeatTreatVal = 0;
                if (IsHeatTreat.Checked)
                {
                    IsHeatTreatVal = 1;
                }
                else
                {
                    IsHeatTreatVal = 0;
                }



                con1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                cmd1 = new System.Data.SqlClient.SqlCommand("AddFixture", con1);
                cmd1.Parameters.Clear();
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@PartNumber", PartNumber1.Text);
                cmd1.Parameters.AddWithValue("@DrawingNumber", Description1.Text);
                cmd1.Parameters.AddWithValue("@POID", Convert.ToInt32(POID));
                cmd1.Parameters.AddWithValue("@Qty", Qty.Text);
                cmd1.Parameters.AddWithValue("@Delivery", Delivery1.SelectedDateFormatted);
                cmd1.Parameters.AddWithValue("@Notes", "NA");
                cmd1.Parameters.AddWithValue("@SourceLot", Convert.ToInt32(SourceLot));
                cmd1.Parameters.AddWithValue("@FirstOpReq", Convert.ToInt32(FirstOpReq.SelectedValue));
                cmd1.Parameters.AddWithValue("@Owner", Convert.ToInt32(Owner.SelectedValue));
                cmd1.Parameters.AddWithValue("@Material", Convert.ToInt32(Material.SelectedValue));
                cmd1.Parameters.AddWithValue("@MaterialDim", Convert.ToInt32(MaterialDim.SelectedValue));
                cmd1.Parameters.AddWithValue("@MaterialSize", Convert.ToInt32(MaterialSize.SelectedValue));
                cmd1.Parameters.AddWithValue("@Length", Length.Text);
                cmd1.Parameters.AddWithValue("@IsPlating", IsPlatingVal);
                cmd1.Parameters.AddWithValue("@Plating", Convert.ToInt32(Plating.SelectedValue));
                cmd1.Parameters.AddWithValue("@IsHeatTreat", IsHeatTreatVal);
                cmd1.Parameters.AddWithValue("@HeatTreat", Convert.ToInt32(HeatTreat.SelectedValue));
                cmd1.Parameters.AddWithValue("@IsTurn", IsTurnVal);
                cmd1.Parameters.AddWithValue("@Turn", Turn.Text);
                cmd1.Parameters.AddWithValue("@IsMill", IsMillVal);
                cmd1.Parameters.AddWithValue("@Mill", Mill.Text);
                cmd1.Parameters.AddWithValue("@IsMultiaxis", IsMultiaxisVal);
                cmd1.Parameters.AddWithValue("@Multiaxis", Multiaxis.Text);
                cmd1.Parameters.AddWithValue("@IsGrind", IsGrindVal);
                cmd1.Parameters.AddWithValue("@Grind", Grind.Text);
                cmd1.Parameters.AddWithValue("@IsWireEDM", IsWireEDMVal);
                cmd1.Parameters.AddWithValue("@WireEDM", WireEDM.Text);
                cmd1.Parameters.AddWithValue("@SourceSetup", Convert.ToInt32(FirstOpReq.SelectedValue));



                con1.Open();
                ret = cmd1.ExecuteNonQuery();
                con1.Close();
                con1.Dispose();

                fileId = Guid.NewGuid();
                if (myFileTest.HasFile)
                {
                    buffer = new byte[(int)myFileTest.FileContent.Length];
                    myFileTest.FileContent.Read(buffer, 0, buffer.Length);
                    if (myFileTest.FileContent.Length > 0)
                    {
                        using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                        {
                            con.Open();

                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("FileAdd", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@revisionID", ret);
                            cmd.Parameters.AddWithValue("@fileType", myFileTest.PostedFile.ContentType);
                            cmd.Parameters.AddWithValue("@ID", fileId);
                            cmd.Parameters.AddWithValue("@Drawing", buffer);


                            cmd.ExecuteNonQuery();


                            con.Close();
                        }

                  

                }
                   
            }
            FixtureOrderGrid.DataBind();
            FixtureInventoryGrid.DataBind();

            //Response.Redirect("Receipt.aspx?Session=" + sessionval);


        }


        protected void FixtureOrderGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;
            bool check;
            int count = 0;


            string command_name = e.CommandName;

            if ((command_name == "AttachFile") || (command_name == "GetFile"))
            {


                switch (e.CommandName)
                {
                    case "AttachFile":


                        gvRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                        FileUpload myFileTest = (FileUpload)gvRow.FindControl("filMyFileTest");
                        Guid fileId = Guid.NewGuid();
                        if (myFileTest.HasFile)
                        {
                            byte[] buffer = new byte[(int)myFileTest.FileContent.Length];
                            myFileTest.FileContent.Read(buffer, 0, buffer.Length);
                            if (myFileTest.FileContent.Length > 0)
                            {
                                using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                                {
                                    con.Open();
                                    System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("SELECT COUNT(Drawings.SerialNumber) As DrawCount FROM Drawings WHERE RevisionID = " + e.CommandArgument.ToString(), con);
                                    System.Data.SqlClient.SqlDataReader reader = cmd.ExecuteReader();
                                    while (reader.Read())
                                    {
                                        count = Convert.ToInt32(reader["DrawCount"].ToString());
                                    }

                                    con.Close();

                                    if (count > 0)
                                    {
                                        cmd = new System.Data.SqlClient.SqlCommand("DELETE FROM Drawings WHERE RevisionID = " + e.CommandArgument.ToString(), con);

                                        con.Open();
                                        cmd.ExecuteNonQuery();
                                        con.Close();
                                    }
                                    con.Open();
                                    cmd = new System.Data.SqlClient.SqlCommand("FileAdd", con);
                                    cmd.CommandType = CommandType.StoredProcedure;

                                    cmd.Parameters.AddWithValue("@revisionID", Convert.ToInt32(e.CommandArgument.ToString()));
                                    cmd.Parameters.AddWithValue("@fileType", myFileTest.PostedFile.ContentType);
                                    cmd.Parameters.AddWithValue("@ID", fileId);
                                    cmd.Parameters.AddWithValue("@Drawing", buffer);


                                    cmd.ExecuteNonQuery();


                                    con.Close();
                                }
                            }
                        }
                        break;
                    case "GetFile":
                        String PartNumber;
                        String RevNumber;
                        GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                        PartNumber = clickedRow.Cells[2].Text;
                        RevNumber = clickedRow.Cells[3].Text;
                        Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber + "&RevNumber=" + RevNumber);
                        break;


                    default:
                        break;
                }

            }
        }

        protected void FixtureInventoryGrid_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //GridView cells are 0 based
            GridViewRow gvRow;

            TextBox Location;
            string FixtureMapID;
            int index;


            string command_name = e.CommandName;

            switch (e.CommandName)
            {
                case "Remove":
                    index = Convert.ToInt32(e.CommandArgument);


                    gvRow = FixtureInventoryGrid.Rows[index];
                    FixtureMapID = gvRow.Cells[0].Text;


                    SqlConnection connection = new SqlConnection(MonseesConnectionString);
                    connection.Open();
                    if ((connection.State & ConnectionState.Open) > 0)
                    {
                        connection.Close();
                        try
                        {

                            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("RemoveFixtInvFromPart", con);
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FixtureMapID", Convert.ToInt32(FixtureMapID));

                            con.Open();
                            cmd.ExecuteNonQuery();
                            FixtureInventoryGrid.DataBind();
                        }
                        catch
                        {
                            Response.Write("Didn't Work.");
                        }
                    }
                    else
                    {
                        Response.Write("No network connection!");
                    }
                    break;
                case "Delete":
                    index = Convert.ToInt32(e.CommandArgument);


                    gvRow = FixtureInventoryGrid.Rows[index];
                    FixtureMapID = gvRow.Cells[0].Text;
                    System.Data.SqlClient.SqlConnection con1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                    System.Data.SqlClient.SqlCommand cmd1 = new System.Data.SqlClient.SqlCommand("DeleteFixture", con1);
                    cmd1.Parameters.Clear();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.Parameters.AddWithValue("@FixtureMapID", Convert.ToInt32(FixtureMapID));

                    con1.Open();
                    cmd1.ExecuteNonQuery();
                    FixtureInventoryGrid.DataBind();
                    break;
                case "AttachFile":
                    int index2 = Convert.ToInt32(e.CommandArgument);


                    gvRow = FixtureInventoryGrid.Rows[index2];
                    FileUpload myFileTest = (FileUpload)gvRow.FindControl("filMyFileTest");
                    Guid fileId = Guid.NewGuid();
                    if (myFileTest.HasFile)
                    {
                        byte[] buffer = new byte[(int)myFileTest.FileContent.Length];
                        myFileTest.FileContent.Read(buffer, 0, buffer.Length);
                        if (myFileTest.FileContent.Length > 0)
                        {
                            using (System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                            {
                                con.Open();

                                System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand("FileAdd", con);
                                cmd.CommandType = CommandType.StoredProcedure;

                                cmd.Parameters.AddWithValue("@revisionID", 17756);
                                cmd.Parameters.AddWithValue("@fileType", "pdf");

                               
                                cmd.ExecuteNonQuery();
                                SqlTransaction tran = con.BeginTransaction();
                               
                                SqlCommand getTransaction = new SqlCommand("SELECT Drawing.PathName(), GET_FILESTREAM_TRANSACTION_CONTEXT() FROM Drawings WHERE FileId = @FileID", con);
                                getTransaction.Transaction = tran;
                                getTransaction.Parameters.Add("@FileId", SqlDbType.UniqueIdentifier).Value = fileId;
                                SqlDataReader contextReader = getTransaction.ExecuteReader(CommandBehavior.SingleRow);
                                contextReader.Read();
                                string filePath = contextReader.GetString(0);
                                byte[] transactionId = (byte[])contextReader[1];
                                contextReader.Close();
                                con.Close();
                            }
                        }
                    }
                    break;

                case "GetFile":
                    String PartNumber;

                    GridViewRow clickedRow = ((LinkButton)e.CommandSource).NamingContainer as GridViewRow;
                    PartNumber = clickedRow.Cells[3].Text;

                    Response.Redirect("pdfhandler.ashx?FileID=" + e.CommandArgument + "&PartNumber=" + PartNumber);
                    break;

                default:
                    break;
            }





        }

        //private void LoadServices()
        //{
           // string sqlstring = "Select [ServiceID],Service from [Services] where Active = 1 ORDER BY Service ASC";

            // create a connection with sqldatabase 
           // System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            // create a sql command which will user connection string and your select statement string
          //  System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
          //  System.Data.SqlClient.SqlDataReader reader;
            // open a connection with sqldatabase
          //  con.Open();

            // execute sql command and store a return values in reade
         //   reader = comm.ExecuteReader();
         //   while (reader.Read())
         //   {
         //       ServiceDropDownList.Items.Add(reader["Service"].ToString());
                
        //    }
        //    con.Close();
       // }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string sqlstring = "Select [OperationID],OperationName from [OperationsAvailalbe] where JobItemID = " + SourceLot + " ORDER BY ProcessOrder ASC";

                

                
            
        }

       
    }
}

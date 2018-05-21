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
    public partial class QuickFixture : DataPage
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
            con1.Close(); 



            string sqlstring = "Select [POID] FROM [Purchase Order] WHERE [SourceLot] = " + SourceLot + ";";
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

                    POID = Convert.ToInt32(reader["POID"].ToString());

                }
                con.Close();

                GetData();

                if (!IsPostBack)
                {

                //OwnerList = new List<ContactModel>();
                

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
                   
            FileUpload myFileTest;
            
            DropDownList FirstOpReq;
            DropDownList Plating;
            DropDownList HeatTreat;
            DropDownList Owner;
            DropDownList Material;
            TextBox Location;
            TextBox Notes;

            int ret;

            byte[] buffer;

           


                PartNumber1 = (TextBox)FormView1.FindControl("PartNumber1");
                Description1 = (TextBox)FormView1.FindControl("Description1");
                    
                myFileTest = (FileUpload)FormView1.FindControl("filMyFileTest");
               
                FirstOpReq = (DropDownList)FormView1.FindControl("OperationDropDownList");
                Plating = (DropDownList)FormView1.FindControl("Plating");
                HeatTreat = (DropDownList)FormView1.FindControl("HeatTreat");
                Owner = (DropDownList)FormView1.FindControl("Owner");
                Material = (DropDownList)FormView1.FindControl("Material");
                Location = (TextBox)FormView1.FindControl("Location");
                Notes = (TextBox)FormView1.FindControl("Notes");


            


                con1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
                cmd1 = new System.Data.SqlClient.SqlCommand("QuickFixture", con1);
                cmd1.Parameters.Clear();
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.AddWithValue("@PartNumber", PartNumber1.Text);
                cmd1.Parameters.AddWithValue("@DrawingNumber", Description1.Text);
                cmd1.Parameters.AddWithValue("@POID", Convert.ToInt32(POID));
               
                cmd1.Parameters.AddWithValue("@Notes", Notes.Text);
                cmd1.Parameters.AddWithValue("@SourceLot", Convert.ToInt32(SourceLot));
                cmd1.Parameters.AddWithValue("@FirstOpReq", Convert.ToInt32(FirstOpReq.SelectedValue));
                cmd1.Parameters.AddWithValue("@Owner", Convert.ToInt32(Owner.SelectedValue));
                cmd1.Parameters.AddWithValue("@Material", Convert.ToInt32(Material.SelectedValue));               
                cmd1.Parameters.AddWithValue("@SourceSetup", Convert.ToInt32(FirstOpReq.SelectedValue));
                cmd1.Parameters.AddWithValue("@Location", Location.Text);   

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
      

                //Response.Redirect("Receipt.aspx?Session=" + sessionval);

            
        }


       
        

       // protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    string sqlstring = "Select [OperationID],OperationName from [OperationsAvailalbe] where JobItemID = " + SourceLot + " ORDER BY ProcessOrder ASC";

                

                
            
      //  }

       
    }
}

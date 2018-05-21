using System;
using System.IO;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Monsees.Database;
using Monsees.DataModel;
using Monsees.Pages;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;


namespace Monsees
{
    public partial class _Fixturing : DataPage
    {
        private string MonseesConnectionString;
        private string MatPriceID;
        public string JobItemID;
        public JobDetailModel JobDetailModel { get; set; }
              

        protected void Page_Load(object sender, EventArgs e)
        {
                JobItemID = Request.QueryString["JobItemID"];
                

                MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                MonseesSqlDataSourceFixtureOrders.ConnectionString = MonseesConnectionString;
                MonseesSqlDataSourceFixtureInventory.ConnectionString = MonseesConnectionString;

                MonseesSqlDataSourceFixtureOrders.EnableCaching = false;
                MonseesSqlDataSourceFixtureOrders.SelectCommand = "Select * From FixtureOrders WHERE SourceLot = " + JobItemID;
                MonseesSqlDataSourceFixtureInventory.EnableCaching = false;
                MonseesSqlDataSourceFixtureInventory.SelectCommand = "Select * From FixtureInvSummary WHERE SourceLot = " + JobItemID;

                GetData();    

                if (!Page.IsPostBack)
                {
                   
                    FixtureOrderGrid.DataBind();
                    FixtureInventoryGrid.DataBind();
                    
                }

               
           
        }


        protected void GetData()
        {
            this.UnitOfWork.Begin();

            ActiveJobsRepository ActiveJobsRepository = new ActiveJobsRepository(UnitOfWork);
            JobDetailModel = ActiveJobsRepository.GetJobDetailModelByJobItemId(Int32.Parse(JobItemID));
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
                                    
                                    if (count>0)
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
                                
                                //objSqlParamOutput.Direction = ParameterDirection.Output;
                                //cmd.Parameters.Add(objSqlParamOutput);
                                cmd.ExecuteNonQuery();
                                SqlTransaction tran = con.BeginTransaction();   
                                //string Path = cmd.Parameters["@filepath"].Value.ToString();

                                //cmd = new SqlCommand("SELECT GET_FILESTREAM_TRANSACTION_CONTEXT()", con, tran);
                                //byte[] objContext = (byte[])cmd.ExecuteScalar();
                                //SqlFileStream objSqlFileStream = new SqlFileStream(Path, objContext, FileAccess.Write);
                                //objSqlFileStream.Write(buffer, 0, buffer.Length);
                                //objSqlFileStream.Close();

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

        protected bool UpdateMaterialRecord()
        {
            MonseesDB objMonseesDB;
            
            

            

            objMonseesDB = new MonseesDB();
            
            try
            {
                string sqlstring = @"--Use monsees2 
									declare @True bit,@False bit; select @True = 1, @False = 0;UPDATE Material_Price2 SET Received = 1 WHERE MatPriceID=" + MatPriceID.Trim();
                int result;

                result = objMonseesDB.ExecuteNonQuery(sqlstring);

                if (result == 1)
                {
                    FixtureOrderGrid.DataSourceID = MonseesSqlDataSourceFixtureOrders.ID;
                    FixtureOrderGrid.DataBind();


                }
                
            }
            catch (System.Exception ex)
            {
                
            }
            finally
            {
                objMonseesDB.Close();
            }

            return true;
        }


       
        private void MessageBox(string msg)
        {
            Page.Controls.Add(new LiteralControl("<script language='javascript'> window.alert('" + msg.Replace("'", "\\'") + "')</script>"));
        }
      
        
       

        
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BasicFrame.WebControls;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using Monsees.Database;
using Monsees.DataModel;
using Monsees.Data;
using Monsees.Pages;
using Dapper;

namespace Monsees
{
    public partial class SetupSheet : DataPage
    {
        string MonseesConnectionString;
        private string EmployeeID;
        private string[] EmployeeLoginName;
        private string JobItemIDStr;
        private string JobSetupIDStr;
        private int SetupID;
        private List<string> JobSetups = new List<string>();        
        public List<SetupListModel> SetupList { get; set; }
        protected List<EmployeeModel> EmployeeList;
        protected List<MachineModel> MachineList;
        protected JobDetailModel JobItemDetails;
        protected List<FixtureSetupSheetModel> Fixtures;
        protected List<ToolingDetailModel> SpecialTools;
        protected SetupDetailModel SetupDetails;
        protected ProcessDetailModel ProcessDetails;
        protected int JobItemID;
        protected int JobSetupID;
        protected int ProcessID;
        public List<SetupWorksheetView> WorksheetData { get; set; }
        //public ProcessView HeaderData { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            
            
            if (Request.QueryString["JobItemID"] != null)
            {
                JobItemIDStr = Request.QueryString["JobItemID"];
                JobItemID = Convert.ToInt32(JobItemIDStr);
            }

            if (Request.QueryString["EmpID"] != null)
                EmployeeID = Request.QueryString["EmpID"];

            if (Request.QueryString["JobSetupID"] != null)
            {
                JobSetupIDStr = Request.QueryString["JobSetupID"];
                JobSetupID = Convert.ToInt32(JobSetupIDStr);
            }
            
            GetData();
            if (!IsPostBack)
            {
                EmployeeLoginName = User.Identity.Name.Split('\\');
                string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
                string sqlstring3 = "Select [EmployeeID], [Name] FROM [Employees] WHERE [WindowsAuthLogin] = 'jspurling';";
                System.Data.SqlClient.SqlConnection con2 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(sqlstring3, con2);
                System.Data.SqlClient.SqlDataReader reader2;
                con2.Open();

                reader2 = comm2.ExecuteReader();

                while (reader2.Read())
                {
                    EmployeeID = reader2["EmployeeID"].ToString();
                }
                con2.Close();

                string sqlstring2 = "Select JobItemID FROM JobSetup WHERE JobSetupID = " + JobSetupIDStr + ";";

                System.Data.SqlClient.SqlConnection con1 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
                System.Data.SqlClient.SqlCommand comm1 = new System.Data.SqlClient.SqlCommand(sqlstring2, con1);
                System.Data.SqlClient.SqlDataReader reader1;
                con1.Open();

                reader1 = comm1.ExecuteReader();

                while (reader1.Read())
                {
                    JobItemIDStr = reader1["JobItemID"].ToString();
                    JobItemID = Convert.ToInt32(JobItemIDStr);
                }
                con1.Close(); 

                string sqlstring = "Select ProcessID FROM Process WHERE [SetupID] = " + JobSetupID + ";";

                System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);

                System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);

                System.Data.SqlClient.SqlDataReader reader;

                con.Open();


                reader = comm.ExecuteReader();
                ProcessID = 0;
                while (reader.Read())
                {
                    ProcessID = Convert.ToInt32(reader["ProcessID"].ToString());                   
                }

                con.Close();
                SetupsDropDownList.DataSource = SetupList;
                SetupsDropDownList.DataBind();
                EmployeeCommentDrop.DataSource = EmployeeList;
                EmployeeDropDown.DataSource = EmployeeList;
                EmployeeDropList.DataSource = EmployeeList;
                EmployeeCommentDrop.DataBind();
                EmployeeDropDown.DataBind();
                EmployeeDropList.DataBind();
                EmployeeCommentDrop.SelectedValue = EmployeeID.ToString();
                EmployeeDropDown.SelectedValue = EmployeeID.ToString();
                EmployeeDropList.SelectedValue = EmployeeID.ToString();
                MachineDropDown.DataSource = MachineList;
                MachineDropDown.DataBind();
                GridView1.DataSource = Fixtures;
                GridView2.DataSource = SpecialTools;
                GridView1.DataBind();
                GridView2.DataBind();
                SetupEntrySource.SelectCommand = "SELECT SetupEntryID, SetupID, Name, Entry, Timestamp FROM SetupEntries LEFT OUTER JOIN Employees ON SetupEntries.EmployeeID = Employees.EmployeeID WHERE SetupID = " + SetupID;
                SetupEntries.DataSource = SetupEntrySource;
                SetupEntries.DataBind();
            }

            UnitOfWork uw = new UnitOfWork();
            uw.Context.Open();

            if (ProcessID != 0)
            {
                ProcessMultiView.SetActiveView(ProcessView);
                InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
                ProcessDetails = inspectionRepository.GetProcessDetailsbyProcessID(ProcessID);
                WorksheetData = uw.Context.Query<SetupWorksheetView>(@"DECLARE @true bit; 
												DECLARE @false bit;
												SET @true = 1 SET @false = 0;
												SELECT * From SetupWorksheetItems WHERE ProcessID=@ProcessID ORDER BY ToolNumber",
                                                new { ProcessID = ProcessID }).ToList();
            }
            else
            {
                ProcessMultiView.SetActiveView(StartProcessView);
                WorksheetData = uw.Context.Query<SetupWorksheetView>(@"DECLARE @true bit; 
												DECLARE @false bit;
												SET @true = 1 SET @false = 0;
												SELECT * From SetupWorksheetItems WHERE ProcessID=0 ORDER BY ToolNumber",
                                               new { ProcessID = ProcessID }).ToList();
            }
            
        }

        protected void GetData()
        {
            
            this.UnitOfWork.Begin();
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string sqlstring = "Select [JobItemID], [SetupID] FROM [JobSetup] WHERE [JobSetupID] = " + JobSetupID + ";";
            
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
           
            System.Data.SqlClient.SqlCommand comm = new System.Data.SqlClient.SqlCommand(sqlstring, con);
            
            System.Data.SqlClient.SqlDataReader reader;
           
            con.Open();

            
            reader = comm.ExecuteReader();

            while (reader.Read())
            {
                  JobItemID = Convert.ToInt32(reader["JobItemID"].ToString());
                  SetupID = Convert.ToInt32(reader["SetupID"].ToString());
            }

            con.Close();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            SetupDetails = inspectionRepository.GetSetupDetailsbyJobSetupID(JobSetupID);
            
            JobItemDetails = inspectionRepository.GetJobDetailModelByJobItemId(JobItemID);
            EmployeeList = inspectionRepository.GetActiveEmployees();
            MachineList = inspectionRepository.GetMachines();
            Fixtures = inspectionRepository.GetFixtureDetailModelBySetupID(SetupDetails.SetupID);
            //SpecialTools = inspectionRepository.GetSpecialToolDetailBySetupID(SetupDetails.SetupID);
            SetupList = inspectionRepository.GetSetupList(Convert.ToInt32(JobItemID));
            
            this.UnitOfWork.End();
        }

        /*protected void Save_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection con1;
            System.Data.SqlClient.SqlCommand cmd1;


            int programmer = Convert.ToInt32(EmployeeDropDown.SelectedValue);
            //string programs = ProgramNums.Text;
            string comments = Comments.Text;

            con1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            cmd1 = new System.Data.SqlClient.SqlCommand("UpdateSetupSheet", con1);
            cmd1.Parameters.Clear();
            cmd1.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue("@SetupID", SetupDetails.SetupID);
            cmd1.Parameters.AddWithValue("@programmer", programmer);
            //cmd1.Parameters.AddWithValue("@programnums", programs);
            cmd1.Parameters.AddWithValue("@comments", comments);


            con1.Open();
            cmd1.ExecuteNonQuery();
            con1.Close();
            con1.Dispose();
        }*/

        protected bool CreateProcessRecord()
        {
            string MonseesConnectionString;
            MonseesDBStaticTables objEmployeeDictionary = MonseesDBStaticTables.Instance;
            double HoursValue;
            int checkvalue;
            if (CheckMoveOn.Checked)
            {
                checkvalue = 1;
            }
            else
            {
                checkvalue = 0;
            }
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
            comm2.Parameters.AddWithValue("@QuantityIn", Convert.ToInt32(QuanityIn.Text));
            comm2.Parameters.AddWithValue("@QuantityOut", Convert.ToInt32(QuanityOut.Text));
            comm2.Parameters.AddWithValue("@Hours", Convert.ToInt32(Hours.Text));
            comm2.Parameters.AddWithValue("@Logout", DateTime.Now);
            comm2.Parameters.AddWithValue("@JobItemID", Convert.ToInt32(JobItemID));
            comm2.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(EmployeeDropList.SelectedValue));
            comm2.Parameters.AddWithValue("@JobSetupID", SetupsDropDownList.SelectedValue);
            comm2.Parameters.AddWithValue("@ProgramNum", ProgramNum.Text);
            comm2.Parameters.AddWithValue("@CheckMoveOn", checkvalue);
            try
            {

                result = comm2.ExecuteNonQuery();

                if (result == 2 || result == 1)
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.SaveButton.GetType(), "EventPop", "window.opener.location.reload();window.opener==null;window.close()", true);

                }
                else
                {
                    ResultMsg.Text = result.ToString();
                }
                ;
            }
            catch (System.Exception ex)
            {
                ResultMsg.Text = "Save Failed. (2)";
            }
            finally
            {
                con.Close();
            }



            return true;




        }

        protected void SeeFixtures_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.open('QuickFixture.aspx?SourceLot=" + JobItemID + "&SourceSetup=" + SetupID + "','_blank');</script>");
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            CreateProcessRecord();
        }

        protected void CommentButton_Click(object sender, EventArgs e)
        {
            using (System.Data.SqlClient.SqlConnection con7 = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                con7.Open();

                System.Data.SqlClient.SqlCommand cmd7 = new System.Data.SqlClient.SqlCommand("SetupCommentAdd", con7);
                cmd7.CommandType = CommandType.StoredProcedure;

                cmd7.Parameters.AddWithValue("@SetupID", SetupID);
                cmd7.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(EmployeeCommentDrop.SelectedValue));
                cmd7.Parameters.AddWithValue("@EntryTxt", EntryText.Text);
                cmd7.ExecuteNonQuery();

                con7.Close();
                SetupEntrySource.SelectCommand = "SELECT SetupEntryID, SetupID, Name, Entry, Timestamp FROM SetupEntries LEFT OUTER JOIN Employees ON SetupEntries.EmployeeID = Employees.EmployeeID WHERE SetupID = " + SetupID;
                SetupEntries.DataSource = SetupEntrySource;
                SetupEntries.DataBind();
            }
        }

        protected void StartProcButton_Command(object sender, CommandEventArgs e)
        {
            
            using (System.Data.SqlClient.SqlConnection con8 = new System.Data.SqlClient.SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                
                con8.Open();
                
                System.Data.SqlClient.SqlCommand cmd8 = new System.Data.SqlClient.SqlCommand("ProcessWorksheetAdd", con8);
                cmd8.CommandType = CommandType.StoredProcedure;

                cmd8.Parameters.AddWithValue("@JobSetupID", JobSetupID);
                cmd8.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(EmployeeDropDown.SelectedValue));
                cmd8.Parameters.AddWithValue("@MachineID", Convert.ToInt32(MachineDropDown.SelectedValue));
                cmd8.Parameters.AddWithValue("@NumTools", Convert.ToInt32(NumToolSlots.Text));                
                cmd8.ExecuteNonQuery();

                con8.Close();
                
            }

            UnitOfWork uw = new UnitOfWork();
            uw.Context.Open();
            WorksheetData = uw.Context.Query<SetupWorksheetView>(@"DECLARE @true bit; 
												DECLARE @false bit;
												SET @true = 1 SET @false = 0;
												SELECT * From SetupWorksheetItems WHERE ProcessID=@ProcessID ORDER BY ToolNumber",
                                               new { ProcessID = ProcessID }).ToList();
            Response.Redirect(Request.RawUrl);
        }
    }
}
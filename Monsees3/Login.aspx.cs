using System;
using System.Collections;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Monsees.Database;
using Monsees.DataModel;
using Monsees.Pages;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;


// Login
// D.S.Harmor
// Description - Allows a user to Login to a specific Job
// 08/19/2011 - Initial Version
// 
// 

namespace Monsees
{
    public partial class Login : DataPage
    {
        string MonseesConnectionString;
        private string EmployeeID;
        private string JobItemID;
        private string JobSetupID;
        private List<string> JobSetups = new List<string>();
        public List<EmployeeModel> Employees { get; set; }
        public List<SetupListModel> SetupList { get; set; }
        


        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is already logged in or not
            
            ResultMsg.Text = "";
            MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();



            if (Request.QueryString["JobItemID"] != null)
                JobItemID = Request.QueryString["JobItemID"];

            if (!IsPostBack)
            {

                if (Request.QueryString["EmpID"] != null)
                    EmployeeID = Request.QueryString["EmpID"];

                if (Request.QueryString["JobSetupID"] != null)
                    JobSetupID = Request.QueryString["JobSetupID"];

                LoadSetups();
            }
        }

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
            comm2.Parameters.AddWithValue("@EmployeeID", Convert.ToInt32(EmployeeList.SelectedValue));
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

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            CreateProcessRecord();
        }

        protected void SeeFixtures_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.open('Fixturing.aspx?JobItemID=" + JobItemID + "');</script>");
        }

 

        private void LoadSetups()
        {
            this.UnitOfWork.Begin();
            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);

            SetupList = inspectionRepository.GetSetupList(Convert.ToInt32(JobItemID));  
            SetupsDropDownList.DataSource = SetupList;
            SetupsDropDownList.DataBind();
           
            Employees = inspectionRepository.GetActiveEmployees();
            EmployeeList.DataSource = Employees;
            //EmployeeList.SelectedValue = EmployeeID;
            EmployeeList.DataBind();
            this.UnitOfWork.End();
        }

        
        
    }
}

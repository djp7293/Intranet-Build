using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Management;
using Monsees.Database;
using Monsees.DataModel;
using Monsees.Pages;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using System.Collections.Generic;

// Logout
// D.S.Harmor
// Description - Allows a user to Logout to a specific Job
// 08/19/2011 - Initial Version
// 
// 

namespace Monsees
{
    public partial class Logout : DataPage

    {
        private string EmployeeID;
        private string JobItemID;
        private string JobSetupID;
        public List<EmployeeModel> Employees { get; set; }
        

        protected void Page_Load(object sender, EventArgs e)
        {
           
                // Check if the user is already logged in or not
                ResultMsg.Text = "";
                if (Request.QueryString["EmpID"] != null)
                    EmployeeID = Request.QueryString["EmpID"];

                if (Request.QueryString["JobItemID"] != null)
                    JobItemID = Request.QueryString["JobItemID"];

                if (Request.QueryString["JobSetupID"] != null)
                    JobSetupID = Request.QueryString["JobSetupID"];
                if (!IsPostBack)
                {
                LoadEmployees();
            }
        }

        protected void SaveButton_Click(object sender, EventArgs e)
        {
            
                UpdateProcessRecord();
            
        }

        protected void SeeFixtures_Click(object sender, EventArgs e)
        {
            Response.Write("<script type='text/javascript'>window.open('Fixturing.aspx?JobItemID=" + JobItemID + "');</script>");
        }

        protected bool UpdateProcessRecord()
        {
            string MonseesConnectionString;
            
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
            comm2.Parameters.AddWithValue("@JobSetupID", Convert.ToInt32(JobSetupID)); 
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

        private void LoadEmployees()
        {
            

            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            Employees = inspectionRepository.GetActiveEmployees();
            EmployeeList.DataSource = Employees;
            EmployeeList.SelectedValue = EmployeeID;
            EmployeeList.DataBind();


            

        }
        
    }
}

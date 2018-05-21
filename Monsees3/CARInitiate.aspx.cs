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
using Monsees.Data;
using Monsees.Pages;

namespace Monsees
{
    public partial class WebForm4 : DataPage
    {
        protected List<EmployeeModel> EmployeeList;
        protected JobDetailModel JobItemDetails;
        protected int JobItemID;

        protected void Page_Load(object sender, EventArgs e)
        {
            JobItemID = Int32.Parse(Request["id"]);
            GetData();

            if (!IsPostBack)
            {
                
                ImplEmplCtrl.DataSource = EmployeeList;
                ImplEmplCtrl.DataBind();

                InitEmplCtrl.DataSource = EmployeeList;
                InitEmplCtrl.DataBind();
            }

            
        }

        protected void GetData()
        {
            this.UnitOfWork.Begin();

            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            EmployeeList = inspectionRepository.GetActiveEmployees();
            JobItemDetails = inspectionRepository.GetJobDetailModelByJobItemId(JobItemID);

            this.UnitOfWork.End();
        }

        protected void Initiate_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection con1;
            System.Data.SqlClient.SqlCommand cmd1;

            string InitEmp = InitEmplCtrl.SelectedValue.ToString();
            string ImpEmp = ImplEmplCtrl.SelectedValue.ToString();
            int CustCar = Convert.ToInt32(CustCarCtrl.Checked);
            string CustCarNum = CustNumCtrl.Text;
            string InitDate = InitDateCtrl.SelectedDateFormatted.ToString();
            string DueDate = DueDateCtrl.SelectedDateFormatted.ToString();
            string Def = ProblemCtrl.Text;

            con1 = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
            cmd1 = new System.Data.SqlClient.SqlCommand("InitCorrectiveAction", con1);
            cmd1.Parameters.Clear();
            cmd1.CommandType = CommandType.StoredProcedure;

            cmd1.Parameters.AddWithValue("@JobItemID", JobItemID);
            cmd1.Parameters.AddWithValue("@InitEmployee", InitEmp);
            cmd1.Parameters.AddWithValue("@ImpEmployee", ImpEmp);
            cmd1.Parameters.AddWithValue("@CustomerCAR", CustCar);
            cmd1.Parameters.AddWithValue("@CustomerCARNum", CustCarNum);
            cmd1.Parameters.AddWithValue("@InitiationDate", InitDate);
            cmd1.Parameters.AddWithValue("@DueDate", DueDate);
            cmd1.Parameters.AddWithValue("@Definition", Def);

            con1.Open();
            cmd1.ExecuteNonQuery();
            con1.Close();
            con1.Dispose();
        }
    }
}
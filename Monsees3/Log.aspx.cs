using Monsees.Database;
using Monsees.DataModel;
using Monsees.Pages;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Monsees
{
	public partial class Log : DataPage
	{
		public JobDetailModel JobDetailModel { get; set; }
		public List<DeliveryModel> DeliveryList { get; set; }
		public List<OperationModel> OperationList { get; set; }
		public int JobItemID { get; set; }
        public string EmployeeID;
        public List<EmployeeModel> Employees { get; set; }
       


		protected void Page_Load(object sender, EventArgs e)
		{
			JobItemID = Int32.Parse(Request["id"]);
			GetData();
        
            if (!IsPostBack)
            {

                if (Request.QueryString["EmpID"] != null)
                    EmployeeID = Request.QueryString["EmpID"];

               
                LoadEmployees();
            }
			
			OperationsGridView.DataSource = OperationList;
			OperationsGridView.DataBind();
		}

		

		protected void GetData()
		{
			this.UnitOfWork.Begin();

			InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
			JobDetailModel = inspectionRepository.GetJobDetailModelByJobItemId(JobItemID);
			DeliveryList = inspectionRepository.GetDelivery(JobItemID);
			OperationList = inspectionRepository.GetOperationsByJobItemId(JobItemID);

			this.UnitOfWork.End();
		}

        private void LoadEmployees()
        {
            this.UnitOfWork.Begin();
            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
           
            Employees = inspectionRepository.GetActiveEmployees();
            EmployeeList.DataSource = Employees;
            EmployeeList.SelectedValue = EmployeeID;
            EmployeeList.DataBind();
            this.UnitOfWork.End();
        }

        private void onclick_MarkComplete(int jobsetupid)
        {
            this.UnitOfWork.Begin();
            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            //inspectionRepository.SetCompletionStatus(jobsetupid, status);
            inspectionRepository.CreateProcessRecord(jobsetupid, JobItemID, Convert.ToInt32(EmployeeList.SelectedValue), Convert.ToInt32(hours.Text), Convert.ToInt32(qtyin.Text), Convert.ToInt32(qtyout.Text));
            
        }

	}
}
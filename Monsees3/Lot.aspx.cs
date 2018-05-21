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
	public partial class Lot : DataPage
	{
		public JobDetailModel JobDetailModel { get; set; }
		public List<DeliveryModel> DeliveryList { get; set; }
		public List<OperationModel> OperationList { get; set; }
        public List<InspectionReportView> ReportData { get; set; }
        public List<CertificationSummary> CertSummary { get; set; }
		public int JobItemID { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			JobItemID = Int32.Parse(Request["id"]);
			GetData();
            string DetailID="0";
            string RevisionID="0";
            string sqlstring = "Select DetailID, [Active Version] FROM [Job Item] WHERE [JobItemID] = " + JobItemID + ";";
            string MonseesConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            // create a connection with sqldatabase 
            System.Data.SqlClient.SqlConnection con2 = new System.Data.SqlClient.SqlConnection(MonseesConnectionString);
            // create a sql command which will user connection string and your select statement string
            System.Data.SqlClient.SqlCommand comm2 = new System.Data.SqlClient.SqlCommand(sqlstring, con2);
            // create a sqldatabase reader which will execute the above command to get the values from sqldatabase
            System.Data.SqlClient.SqlDataReader reader2;
            // open a connection with sqldatabase
            con2.Open();

            // execute sql command and store a return values in reade
            reader2 = comm2.ExecuteReader();

            while (reader2.Read())
            {

                DetailID = reader2["DetailID"].ToString();
                RevisionID = reader2["Active Version"].ToString();

            }

            con2.Close();


            DeliveryDataGrid.DataSource = DeliveryList;
			DeliveryDataGrid.DataBind();

			OperationsGridView.DataSource = OperationList;
			OperationsGridView.DataBind();

            ListView1.DataSource = CertSummary;
            ListView1.DataBind();

            SqlDataSource12.SelectCommand = "SELECT * FROM CorrectiveActionView WHERE [DetailID] = " + DetailID;

            CARView.DataSource = SqlDataSource12;
            CARView.DataBind();
        }

		

		protected void GetData()
		{
			this.UnitOfWork.Begin();
            UnitOfWork uw = new UnitOfWork();
            uw.Context.Open();


			InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
			JobDetailModel = inspectionRepository.GetJobDetailModelByJobItemId(JobItemID);
			DeliveryList = inspectionRepository.GetDelivery(JobItemID);
			OperationList = inspectionRepository.GetOperationsByJobItemId(JobItemID);
            ReportData = inspectionRepository.GetInspectionReportData(JobItemID);
            CertSummary = inspectionRepository.GetCertificationSummary(JobItemID);

            uw.Context.Close();
			this.UnitOfWork.End();
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
                    string pageName = "CARComplete.aspx";
                    Response.Write("<script type='text/javascript'>window.open('" + pageName + "?id=" + CARID + "');</script>");
                    break;
                default:
                    break;
            }
            
        }

        protected void button_click(object sender, EventArgs e)
        {
           
            

            string pageNameprt = "CARInitiate.aspx";
            Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?id=" + JobItemID + "');</script>");
        }

        }
}
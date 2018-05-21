using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Drawing;
using Monsees.Controls;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;

namespace Monsees
{
    public partial class NestedActiveJobsCtrl : DataCtrl
    {
        private string MonseesConnectionString;
        public List<OperationDetailedModel> Setups { get; set; }
        public List<SetupFixturesModel> SetupFixtures { get; set; }
        public List<SetupLogHistoryModel> SetupHistory { get; set; }
        public List<SetupLogModel> SetupLogs { get; set; }
        public List<SetupEntriesModel> SetupEntries { get; set; }
        public List<EmployeeModel> Employees { get; set; }
        public List<OperationListModel> OperationList { get; set; }
        public List<MachineModel> MachineList { get; set; }
       

        
        public List<DeliveryModel> Deliveries { get; set; }
        public List<CorrectiveActionModel> CorrectiveActions { get; set; }
        public ViewJobItem Summary { get; set; }
        public List<CertificationSummary> Certifications { get; set; }
        public List<MatlQuoteModel> MatlQuotes { get; set; }
        public List<MatlOrderModel> MatlOrders { get; set; }
        public List<FixtureJobItemModel> Fixtures { get; set; }
        public List<AssyMachinedCompModel> MachinedComponenets { get; set; }
        public List<AssyPurchasedCompModel> PurchasedComponents { get; set; }
        public List<SubcontractLineModel> Subcontracting { get; set; }
        public Int32 EmployeeID = 0;
        public string JobItemID {
            get; set; }
        public int DetailID = 0;
        private Int32 index;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            GetData();
            
                //MainOperationsGrid.DataSource = Setups;
                //MainOperationsGrid.DataBind();
            
        }

        public void SetJobItem(string theText)
        {
            JobItemID = theText;
        }

        public void GetData()
        {
            this.UnitOfWork.Begin();

            ActiveJobsRepository JobItemRepository = new ActiveJobsRepository(UnitOfWork);
            Deliveries = JobItemRepository.GetDelivery(Convert.ToInt32(JobItemID));
            CorrectiveActions = JobItemRepository.GetCorrectiveActionsByJobItemID(Convert.ToInt32(JobItemID));
            Summary = JobItemRepository.GetJobItemSummaryByJobItemID(Convert.ToInt32(JobItemID));
            Certifications = JobItemRepository.GetCertificationSummaryByJobItemID(Convert.ToInt32(JobItemID));
            MatlQuotes = JobItemRepository.GetMatlQuotesByJobItemID(Convert.ToInt32(JobItemID));
            MatlOrders = JobItemRepository.GetMatlOrdersByJobItemID(Convert.ToInt32(JobItemID));
            Fixtures = JobItemRepository.GetFixturesByJobItemID(Convert.ToInt32(JobItemID));
            MachinedComponenets = JobItemRepository.GetMachinedComponentsByJobItemID(Convert.ToInt32(JobItemID));
            PurchasedComponents = JobItemRepository.GetPurchasedComponentsByJObItemID(Convert.ToInt32(JobItemID));
            Subcontracting = JobItemRepository.GetSubcontractLinesByJobItemID(Convert.ToInt32(JobItemID));
            InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
            
            MachineList = inspectionRepository.GetMachines();
            Employees = inspectionRepository.GetActiveEmployees();
            OperationList = inspectionRepository.GetOperations();
            Setups = JobItemRepository.GetDetailedOperationsByJobItemId(Convert.ToInt32(JobItemID));
            this.UnitOfWork.End();
        }

        public void GetRowData1(int jobsetupid, int setupid)
        {
            GetRowData(jobsetupid, setupid);
        }

        private void GetRowData(int jobsetupid, int setupid)
        {
            this.UnitOfWork.Begin();

            ActiveJobsRepository JobItemRepository = new ActiveJobsRepository(UnitOfWork);
            SetupLogs = JobItemRepository.GetSetupLogBySetupID(jobsetupid);
            SetupFixtures = JobItemRepository.GetSetupFixturesBySetupID(setupid);
            SetupHistory = JobItemRepository.GetSetupLogHistoryBySetupID(setupid);
            SetupEntries = JobItemRepository.GetSetupEntriesBySetupID(setupid);
            this.UnitOfWork.End();
        }

        protected void LoadThumbnail(HttpContext context)
        {
            string imageid = context.Request.QueryString["ImID"];
            if (imageid == null || imageid == "")
            {
                //Set a default imageID
                imageid = "1";
            }
            SqlConnection connection = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString);
            connection.Open();
            SqlCommand command = new SqlCommand("select SetupImage from SetupImage where ImageID=" + imageid, connection);
            SqlDataReader dr = command.ExecuteReader();
            dr.Read();

            Stream str = new MemoryStream((Byte[])dr[0]);

            Bitmap loBMP = new Bitmap(str);
            Bitmap bmpOut = new Bitmap(100, 100);

            Graphics g = Graphics.FromImage(bmpOut);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            g.FillRectangle(Brushes.White, 0, 0, 100, 100);
            g.DrawImage(loBMP, 0, 0, 100, 100);

            MemoryStream ms = new MemoryStream();
            bmpOut.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            byte[] bmpBytes = ms.GetBuffer();
            bmpOut.Dispose();
            ms.Close();
            context.Response.BinaryWrite(bmpBytes);
            connection.Close();
            context.Response.End();

        }




    }
}
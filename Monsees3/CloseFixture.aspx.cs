using Monsees.Pages;
using Montsees.Data;
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
	public partial class CloseFixture : DataPage
	{
		public JobDetailModel JobDetailModel { get; set; }
		public int JobItemID { get; set; }
		public List<InventoryStatusModel> InventoryStatusList { get; set; }
		public List<InvStatus> InventoryStatusTypeList { get; set; }
		public LotPartTotals LotPartTotals { get; set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			JobItemID = Int32.Parse(Request["id"]);
			GetData();
		}

		public void GetData()
		{
			this.UnitOfWork.Begin();

			InspectionRepository InspectionRepository = new InspectionRepository(UnitOfWork);
            JobDetailModel = InspectionRepository.GetJobDetailModelByJobItemId(JobItemID);
            InventoryStatusList = InspectionRepository.GetInventoryStatusByRevisionID(JobDetailModel.RevisionID);
            InventoryStatusTypeList = InspectionRepository.GetAllInventoryStatusItems();
            LotPartTotals = InspectionRepository.GetLotPartTotals(JobItemID);

			this.UnitOfWork.End();
		}
	}
}
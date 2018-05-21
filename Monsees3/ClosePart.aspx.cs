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
	public partial class ClosePart : DataPage
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

			InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
			JobDetailModel = inspectionRepository.GetJobDetailModelByJobItemId(JobItemID);
			InventoryStatusList = inspectionRepository.GetInventoryStatusByRevisionID(JobDetailModel.RevisionID);
			InventoryStatusTypeList = inspectionRepository.GetAllInventoryStatusItems();
			LotPartTotals = inspectionRepository.GetLotPartTotals(JobItemID);

			this.UnitOfWork.End();
		}
	}
}
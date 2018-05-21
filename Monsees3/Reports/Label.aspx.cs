using Monsees.Pages;
using Montsees.Data.DataModel;
using Montsees.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Monsees.Reports
{
	public partial class Label : DataPage
	{
		public List<LabelModel> LabelModelList { get; set; }
		public List<LabelDeliveryItem> DeliveryItems { get; set; }
		public int JobItemID { get; set; }
		protected void Page_Load(object sender, EventArgs e)
		{
			JobItemID = Int32.Parse(Request["id"]);
			GetData();
            if (LabelModelList[0].ITAR == false)
            {
                ITARInvTag.Visible = false;
                ITARShipTag.Visible = false;
                ITARLabel.Visible = false;

            }
		}

		protected void GetData()
		{
			this.UnitOfWork.Begin();

			InspectionRepository inspectionRepository = new InspectionRepository(UnitOfWork);
			LabelModelList = inspectionRepository.GetLabelByJobItemId(JobItemID);
			DeliveryItems = inspectionRepository.GetDeliveryItemForLabel(JobItemID);

			this.UnitOfWork.End();
		}
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dapper;
using Monsees.DataModel;
using Monsees.Database;

namespace Monsees
{
	public partial class InspectionReportInProcess : System.Web.UI.Page
	{
		public List<InspectionReportView> ReportData { get; set; }
		public InspectionReportView HeaderData { get;set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			int jobItemId = 0;
			Int32.TryParse( Request["JobItemID"], out jobItemId);

			UnitOfWork uw = new UnitOfWork();
			uw.Context.Open();

			ReportData= uw.Context.Query<InspectionReportView>(@"DECLARE @true bit; 
												DECLARE @false bit;
												SET @true = 1 SET @false = 0;
												SELECT *,[Revision Number] as Revision_Number From InspectionReport WHERE JobItemID=@JobItemID And Critical=1 ORDER BY DimensionNumber",
												new{ JobItemID = jobItemId }).ToList();


			HeaderData = ReportData.Count > 0 ? ReportData.First() : new InspectionReportView();

			//sqlstring = "Select [JobItemID], [PartNumber], [Revision Number], [DrawingNumber], [CompanyName], [Quantity], [JobNumber], [Block Address] FROM InspectionReport WHERE JobItemID=@JobItemId GROUP BY [JobItemID], [PartNumber], [Revision Number], [DrawingNumber], [CompanyName], [Quantity], [JobNumber], [Block Address]";
			uw.Context.Close();
		}
	}
}
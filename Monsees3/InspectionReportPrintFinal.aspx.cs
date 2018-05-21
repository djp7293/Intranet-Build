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
	public partial class InspectionReportPrintFinal : System.Web.UI.Page
	{
		public List<InspectionReportView> ReportData { get; set; }
		public InspectionReportView HeaderData { get;set; }

		protected void Page_Load(object sender, EventArgs e)
		{
			int jobItemId = 0;
            int serialNumId = 0;
			Int32.TryParse( Request["JobItemID"], out jobItemId);
            Int32.TryParse(Request["SerialNumID"], out serialNumId);
			UnitOfWork uw = new UnitOfWork();
			uw.Context.Open();

            if (jobItemId != 0)
            {
                ReportData = uw.Context.Query<InspectionReportView>(@"DECLARE @true bit; 
												DECLARE @false bit;
												SET @true = 1 SET @false = 0;
												SELECT *,[Revision Number] as Revision_Number From InspectionReport WHERE JobItemID=@JobItemID ORDER BY DimensionNumber",
                                                new { JobItemID = jobItemId }).ToList();
            }
            else
            {
                ReportData = uw.Context.Query<InspectionReportView>(@"DECLARE @true bit; 
												DECLARE @false bit;
												SET @true = 1 SET @false = 0;
												SELECT *,[Revision Number] as Revision_Number From InspectionReportSerial WHERE SerialNumID=@SerialNumID ORDER BY DimensionNumber",
                                                new { SerialNumID = serialNumId }).ToList();
            }


            HeaderData = ReportData.Count > 0 ? ReportData.First() : new InspectionReportView();

			//sqlstring = "Select [JobItemID], [PartNumber], [Revision Number], [DrawingNumber], [CompanyName], [Quantity], [JobNumber], [Block Address] FROM InspectionReport WHERE JobItemID=@JobItemId GROUP BY [JobItemID], [PartNumber], [Revision Number], [DrawingNumber], [CompanyName], [Quantity], [JobNumber], [Block Address]";
			uw.Context.Close();
		}
	}
}
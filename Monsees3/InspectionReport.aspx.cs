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
	public partial class InspectionReport : System.Web.UI.Page
	{
		public List<InspectionReportView> ReportData { get; set; }
		public InspectionReportView HeaderData { get;set; }
        private int jobItemId = 0;
        public int serialId = 0;

		protected void Page_Load(object sender, EventArgs e)
		{
			
			Int32.TryParse( Request["JobItemID"], out jobItemId);
            Int32.TryParse( Request["SerialNumID"], out serialId);

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
                                                new { SerialNumID = serialId }).ToList();
            }


            HeaderData = ReportData.Count > 0 ? ReportData.First() : new InspectionReportView();

			//sqlstring = "Select [JobItemID], [PartNumber], [Revision Number], [DrawingNumber], [CompanyName], [Quantity], [JobNumber], [Block Address] FROM InspectionReport WHERE JobItemID=@JobItemId GROUP BY [JobItemID], [PartNumber], [Revision Number], [DrawingNumber], [CompanyName], [Quantity], [JobNumber], [Block Address]";
			uw.Context.Close();
		}

        protected void Button2_Click(object sender, EventArgs e)
        {
            string pageNameprt = "InspectionReportPrint.aspx";
            Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?JobItemID=" + jobItemId + "');</script>");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string pageNameprt = "InspectionReportPrintFinal.aspx";
            Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?JobItemID=" + jobItemId + "');</script>");
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            string pageNameprt = "InspectionReportInProcess.aspx";
            Response.Write("<script type='text/javascript'>window.open('" + pageNameprt + "?JobItemID=" + jobItemId + "');</script>");
        }
	}
}
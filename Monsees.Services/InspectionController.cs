using Montsees.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monsees.Services
{
    public class UpdateMeasurementRequest
    {
        public int JobItemId { get; set; }
        public int SerialNumId { get; set; }
        public int DimensionId { get; set; }
        public string Measurment1 { get; set; }
        public string Measurment2 { get; set; }
        public string Measurment3 { get; set; }
        public string Final { get; set; }
        public string Remarks { get; set; }
    }

    

    public class InspectionController : ControllerBase
    {
        static UserControlRestService handler;


        static InspectionController()
		{
			handler = new UserControlRestService(typeof(InspectionController));
		}

        protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (Request["a"] != null)
			{
				handler.HandleRequest(this);
			}
		}
 

		protected InspectionRepository _inspectionRepository;

		protected InspectionRepository inspectionRepository
		{
			get
			{
				if (_inspectionRepository == null) 
					_inspectionRepository = new InspectionRepository(UnitOfWork);

				return _inspectionRepository;
			}
		}
        

		[ServiceMethod]
        public void UpdateMeasurement(int jobItemId, int serialNumId, int dimensionId, string column, string measurement1, string measurement2, string measurement3, string final, string remarks, string critical)
		{
            string[] EmployeeLoginName = User.Identity.Name.Split('\\');           
            inspectionRepository.UpdateInspectionMeasurement(jobItemId, serialNumId, dimensionId, column, measurement1, measurement2, measurement3, final, remarks, critical, EmployeeLoginName[1]);
		}


        [ServiceMethod]
        public void UpdateToolDetails(int ProcessID, int SetupWorksheetItemID, string column, int ToolNumber, string ToolName, string ToolDetails)
        {            
            inspectionRepository.UpdateToolDetails(ProcessID, SetupWorksheetItemID, column, ToolNumber, ToolName, ToolDetails);
        }

        [ServiceMethod]
        public void UpdateJobSetupInfo(int JobSetupID, string Comments)
        {
            inspectionRepository.UpdateJobSetupInfo(JobSetupID, Comments);
        }

    }
}

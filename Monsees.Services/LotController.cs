using Montsees.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Monsees.Services
{
 
	public partial class LotController : ControllerBase
	{
		
		static UserControlRestService handler;

		#region Init

		static LotController()
		{
			handler = new UserControlRestService(typeof(LotController));
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (Request["a"] != null)
			{
				handler.HandleRequest(this);
			}
		}

		#endregion

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
		public void SetCompletionStatus(int jobSetupId, bool status)
		{
			inspectionRepository.SetCompletionStatus(jobSetupId, status);
		}

        [ServiceMethod]
        public void SetCompletionStatusLog(int jobSetupId, int jobItemId, int employeeId, int hours, int qtyin, int qtyout, bool status)
        {
            inspectionRepository.SetCompletionStatus(jobSetupId, status);
            inspectionRepository.CreateProcessRecord(jobSetupId, jobItemId, employeeId, hours, qtyin, qtyout);
        }

		[ServiceMethod]
		public void SetShipStatus(int deliveryItemId, bool readyToShip, bool suspended)
		{
			inspectionRepository.SetShipStatus(deliveryItemId, readyToShip, suspended);
		}

	}
}

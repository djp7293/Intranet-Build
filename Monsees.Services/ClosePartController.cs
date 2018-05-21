using Montsees.Data;
using Montsees.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Monsees.Services
{
 
	public partial class ClosePartController : ControllerBase
	{
		
		static UserControlRestService handler;

		#region Init

		static ClosePartController()
		{
			handler = new UserControlRestService(typeof(ClosePartController));
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
		public LotPartTotals MoveToInventory(int jobItemId, int qty, int status, string location, string notes)
		{
			inspectionRepository.MoveToInventory(jobItemId, qty, status, location, notes);
			return inspectionRepository.GetLotPartTotals(jobItemId);
		}

        [ServiceMethod]
        public void MoveToFixtureInventory(int jobItemId, int qty, string location, string notes)
        {
            inspectionRepository.MoveToFixtureInventory(jobItemId, qty, location, notes);

        }

		[ServiceMethod]
		public void ClosePart(int jobItemId, int produced, int allocated)
		{
			inspectionRepository.CloseLot(jobItemId);
		}

	}
}

using Montsees.Data;
using Montsees.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Monsees.Services
{
 
	public partial class CloseFixtureController : ControllerBase
	{
		
		static UserControlRestService handler;

		#region Init

		static CloseFixtureController()
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

		protected ActiveJobsRepository _ActiveJobsRepository;

		protected ActiveJobsRepository ActiveJobsRepository
		{
			get
			{
				if (_ActiveJobsRepository == null) 
					_ActiveJobsRepository = new ActiveJobsRepository(UnitOfWork);

				return _ActiveJobsRepository;
			}
		}

		[ServiceMethod]
		public void MoveToFixtureInventory(int jobItemId, int qty, string location, string notes)
		{
			ActiveJobsRepository.MoveToFixtureInventory(jobItemId, qty, location, notes);
			
		}

		[ServiceMethod]
		public void ClosePart(int jobItemId)
		{
			ActiveJobsRepository.CloseLot(jobItemId);
		}

	}
}

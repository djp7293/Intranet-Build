using Monsees.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;


namespace Monsees.Services
{
	public class ControllerBase : Page
	{
		private IUnitOfWork _unitOfWork = null;
		public IUnitOfWork UnitOfWork
		{
			get
			{
				if (_unitOfWork == null)
				{
					_unitOfWork = new UnitOfWork();
					_unitOfWork.Begin();
				}

				return _unitOfWork;
			}
		}

		protected override void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
			
			if (_unitOfWork!=null)
			{
				_unitOfWork.End();
			}
		}


	}
}

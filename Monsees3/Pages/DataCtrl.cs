using Monsees.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Monsees.Controls
{
	public class DataCtrl : System.Web.UI.UserControl
    {
		private IUnitOfWork _unitOfWork = null;
		public IUnitOfWork UnitOfWork
		{
			get
			{
				if (_unitOfWork == null)
				{
					_unitOfWork = new UnitOfWork();
				}

				return _unitOfWork;
			}
		}


	}
}
using Monsees.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HardingeTaiwan.Repository
{
	public abstract class DapperRepositoryBase
	{

		internal readonly IUnitOfWork _unitOfWork;

		public DapperRepositoryBase() { }

		public DapperRepositoryBase(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		protected UnitOfWork Uw
		{
			get
			{
				return _unitOfWork as UnitOfWork;
			}
		}
	}
}

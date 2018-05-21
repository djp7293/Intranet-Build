using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Monsees.Database
{
	public interface IUnitOfWork
	{
		void Save();
		void BeginTransaction();
		void Rollback();
		void Begin();
		void End();
	}

}

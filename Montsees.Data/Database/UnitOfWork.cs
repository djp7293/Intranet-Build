 using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
 

namespace Monsees.Database
{
	public class UnitOfWork : IUnitOfWork
	{
		IDbConnection _context;
		IDbTransaction _tx;
		private string connectionStringName;	

		public bool InTransaction { get; private set; }

		public UnitOfWork(string connectionStringName = null)
		{
			_context = new SqlConnection();
			if (connectionStringName == null)
			{
				this.connectionStringName = "ConnectionString";
			}
		}


		public void Save()
		{
			if (_tx != null)
			{
				_tx.Commit();
				_tx = null;
			}

		}

		public IDbTransaction Transaction
		{
			get
			{
				return _tx;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public IDbConnection Connection
		{
			get
			{
				return Context;
			}
		}

		public IDbConnection Context
		{
			get
			{
				if (_context.State == ConnectionState.Broken || _context.State == ConnectionState.Closed)
				{
					_context.ConnectionString = ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
				}

				return _context;
			}
		}


		public void BeginTransaction()
		{
			InTransaction = true;
			_tx = _context.BeginTransaction();
		}

		public void Rollback()
		{
			throw new NotImplementedException();
		}


		public void Begin()
		{
			Connection.Open();
		}

		public void End()
		{
			Connection.Close();
		}
	}
}

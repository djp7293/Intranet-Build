using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 

namespace HardingeTaiwan.Repository
{
	public interface IRepository<T>
	{
		T New();
		T Get(object key);
		IEnumerable<T> All();
	}

    public interface IEdtiableRepository<T>
    {
		void Insert(T entity);
		void InsertOrUpdate(T entity);
		void Delete(T entity);
    }

}

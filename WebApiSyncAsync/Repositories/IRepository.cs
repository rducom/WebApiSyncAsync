using System.Collections.Generic;

namespace WebApiSyncAsync.Repositories
{
	public interface IRepository<T,TKey>
		where T : IEntity<TKey>
		where TKey : struct
	{
		IEnumerable<T> Get();
		T GetById(TKey id);
		T Create(T product);
		T Update(TKey id, T product);
		void Delete(TKey id);
	}
}
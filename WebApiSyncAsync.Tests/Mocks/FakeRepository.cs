using System.Collections.Concurrent;
using System.Collections.Generic;
using WebApiSyncAsync.Repositories;

namespace WebApiSyncAsync.Tests.Mocks
{
	public class FakeRepository<T, TKey> : IRepository<T, TKey>
		where T : IEntity<TKey>
		where TKey : struct
	{
		private readonly ConcurrentDictionary<TKey,T> _concurrentDictionary = new ConcurrentDictionary<TKey, T>();
		public void Dispose()
		{
			_concurrentDictionary.Clear();
		}

		public IEnumerable<T> Get()
		{
			return _concurrentDictionary.Values;
		}

		public T GetById(TKey id)
		{
			return _concurrentDictionary.TryGetValue(id, out T value) ? value : default(T);
		}

		public T Create(T product)
		{
			_concurrentDictionary[product.Id] = product;
			return product;
		}

		public T Update(TKey id, T product)
		{
			_concurrentDictionary[id] = product;
			return product;
		}

		public void Delete(TKey id)
		{
			_concurrentDictionary.TryRemove(id, out T t);
		}
	}
}

namespace WebApiSyncAsync.Repositories
{
	public interface IEntity<TKey> where TKey : struct
	{
		TKey Id { get; }
	}
}
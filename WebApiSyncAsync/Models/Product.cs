using WebApiSyncAsync.Repositories;

namespace WebApiSyncAsync.Models
{
	public class Product : IEntity<int>
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Category { get; set; }
		public decimal Price { get; set; }
	}
}
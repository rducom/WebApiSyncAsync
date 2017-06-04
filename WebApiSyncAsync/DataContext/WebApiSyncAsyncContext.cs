using System.Data.Entity;
using WebApiSyncAsync.Models;

namespace WebApiSyncAsync.DataContext
{
    public class WebApiSyncAsyncContext : DbContext
    {
        public WebApiSyncAsyncContext() 
			: base("name=WebApiSyncAsyncContext")
        {
        }

	    public WebApiSyncAsyncContext(string connectionString) 
			: base(connectionString)
	    {
	    }

		public DbSet<Product> Products { get; set; } 
	}
}

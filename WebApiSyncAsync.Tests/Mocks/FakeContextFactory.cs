using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ploeh.AutoFixture;
using WebApiSyncAsync.DataContext;
using WebApiSyncAsync.Models;

namespace WebApiSyncAsync.Tests.Mocks
{
	public class FakeContextFactory : IWebApiSyncAsyncContextFactory
	{
		private readonly string _connection =
			@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=TestProduct;Integrated Security=true";

		public FakeContextFactory()
		{
			Database.SetInitializer(new ProductTestData());
		}

		public WebApiSyncAsyncContext Create()
		{
			return new WebApiSyncAsyncContext(_connection);
		}

		public class ProductTestData : CreateDatabaseIfNotExists<WebApiSyncAsyncContext>
		{
			protected override void Seed(WebApiSyncAsyncContext context)
			{
				Fixture fixture = new Fixture();
				var customers = Enumerable.Range(0, 100)
					.Select(i => fixture.Create<Product>()).ToList();
				context.Products.AddRange(customers);

				context.SaveChanges();
				base.Seed(context);
			}
		}
	}
}

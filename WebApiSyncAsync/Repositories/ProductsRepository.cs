using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WebApiSyncAsync.DataContext;
using WebApiSyncAsync.Models;

namespace WebApiSyncAsync.Repositories
{
	public class ProductsRepository : IRepository<Product,int>
	{
		private readonly IWebApiSyncAsyncContextFactory _contextFactory;

		public ProductsRepository(IWebApiSyncAsyncContextFactory contextFactory)
		{
			_contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
		}

		public IEnumerable<Product> Get()
		{
			using (var context = _contextFactory.Create())
			{
				return context.Products.ToList();
			}
		}

		public Product GetById(int id)
		{
			using (var context = _contextFactory.Create())
			{
				return context.Products.FirstOrDefault(p => p.Id == id);
			}
		}

		public Product Create(Product product)
		{
			using (var context = _contextFactory.Create())
			{
				context.Products.Add(product);
				context.SaveChanges();
				return product;
			}
		}

		public Product Update(int id, Product product)
		{
			if (id != product.Id || ProductExists(id) == false)
			{
				throw new ProductNotFoundException();
			}
			using (var context = _contextFactory.Create())
			{
				context.Entry(product).State = EntityState.Modified;
				context.SaveChanges();
				return product;
			}
		}
		 
		public void Delete(int id)
		{
			using (var context = _contextFactory.Create())
			{
				Product product = context.Products.Find(id);
				if (product == null)
				{
					throw new ProductNotFoundException();
				}
				context.Products.Remove(product);
				context.SaveChanges();
			}
		}

		private bool ProductExists(int id)
		{
			using (var context = _contextFactory.Create())
			{
				return context.Products.Count(e => e.Id == id) > 0;
			}
		} 
	}
}
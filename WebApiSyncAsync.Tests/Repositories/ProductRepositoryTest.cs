using System;
using System.Linq;
using Ploeh.AutoFixture;
using WebApiSyncAsync.Models;
using WebApiSyncAsync.Repositories;
using WebApiSyncAsync.Tests.Mocks;
using Xunit;

namespace WebApiSyncAsync.Tests.Repositories
{
	public class ProductRepositoryTest
	{
		[Fact]
		public void ProductsRepositoryTest()
		{
			Assert.Throws<ArgumentNullException>(() => new ProductsRepository(null)); 
		}

		[Fact]
		public void GetTest()
		{
			var productRepository = new ProductsRepository(new FakeContextFactory());

			var products = productRepository.Get();

			Assert.NotNull(products);
			Assert.NotEmpty(products);
		}

		[Fact]
		public void GetByIdTest()
		{
			var productRepository = new ProductsRepository(new FakeContextFactory());
			var product = productRepository.Get().FirstOrDefault();
			Assert.NotNull(product);

			var found = productRepository.GetById(product.Id);

			Assert.NotNull(found);
			Assert.Equal(product.Id, found.Id);
		}

		[Fact]
		public void CreateTest()
		{
			var productRepository = new ProductsRepository(new FakeContextFactory());
			Fixture fixture = new Fixture();
			var product = fixture.Create<Product>();

			var created = productRepository.Create(product);

			var found = productRepository.GetById(created.Id);
			Assert.NotNull(found);
			Assert.Equal(created.Id, found.Id);
		}

		[Fact]
		public void UpdateTest()
		{
			var productRepository = new ProductsRepository(new FakeContextFactory());
			var product = productRepository.Get().FirstOrDefault();
			Assert.NotNull(product);
			var guid = Guid.NewGuid().ToString("N");
			product.Name = guid;
			var updated = productRepository.Update(product.Id, product);

			var found = productRepository.GetById(product.Id);
			Assert.NotNull(found);
			Assert.Equal(guid,updated.Name);
		}

		[Fact]
		public void DeleteTest()
		{
			var productRepository = new ProductsRepository(new FakeContextFactory());
			var product = productRepository.Get().FirstOrDefault();
			Assert.NotNull(product);

			productRepository.Delete(product.Id);

			var found = productRepository.GetById(product.Id);
			Assert.Null(found); 
		}
	}
}

using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using Ploeh.AutoFixture.Xunit2;
using WebApiSyncAsync.Controllers;
using WebApiSyncAsync.Models;
using WebApiSyncAsync.Tests.Mocks;
using Xunit;

namespace WebApiSyncAsync.Tests.Controllers
{
	public class ProductsControllerTests
	{
		[Theory, AutoData]
		public void GetProductTest(Product product, FakeRepository<Product, int> repository)
		{
			var controller = new ProductsController(repository);
			repository.Create(product);

			var getActionResult = controller.GetProduct(product.Id);
			var getResult = getActionResult as OkNegotiatedContentResult<Product>;

			Assert.NotNull(getResult);
			Assert.Equal(product, getResult.Content);
		}

		[Theory, AutoData]
		public void GetProductsTest(Product product, FakeRepository<Product, int> repository)
		{
			var controller = new ProductsController(repository);
			repository.Create(product);

			IEnumerable<Product> found = controller.GetProducts();

			Assert.NotNull(found);
			Assert.NotEmpty(found);
		}

		[Theory, AutoData]
		public void PutProductTest(Product product, FakeRepository<Product, int> repository)
		{
			var controller = new ProductsController(repository);

			var putActionResult = controller.PutProduct(product.Id, product);
			var putResult = putActionResult as StatusCodeResult;

			Assert.NotNull(putResult);
			Assert.Equal(HttpStatusCode.NoContent, putResult.StatusCode);
			Assert.NotNull(repository.GetById(product.Id));
		}

		[Theory, AutoData]
		public void PostProductTest(Product product, FakeRepository<Product, int> repository)
		{
			var controller = new ProductsController(repository);

			var postActionResult = controller.PostProduct(product);
			var postResult = postActionResult as CreatedAtRouteNegotiatedContentResult<Product>;

			Assert.NotNull(postResult);
			Assert.Equal(product, postResult.Content);
			Assert.NotNull(repository.GetById(product.Id));
		}

		[Theory, AutoData]
		public void DeleteProductTest(Product product, FakeRepository<Product, int> repository)
		{
			var controller = new ProductsController(repository);
			repository.Create(product);

			var deleteActionResult = controller.DeleteProduct(product.Id);
			var deleteResult = deleteActionResult as OkNegotiatedContentResult<Product>;

			Assert.NotNull(deleteResult);
			Assert.Equal(product, deleteResult.Content);
			Assert.Null(repository.GetById(product.Id));
		}
	}
}
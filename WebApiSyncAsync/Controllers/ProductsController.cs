using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiSyncAsync.Models;
using WebApiSyncAsync.Repositories;

namespace WebApiSyncAsync.Controllers
{
	public class ProductsController : ApiController
	{
		private readonly IRepository<Product,int> _productsRepository;

		public ProductsController(IRepository<Product, int> productsRepository)
		{
			_productsRepository = productsRepository;
		}

		// GET: api/Products
		public IEnumerable<Product> GetProducts()
		{
			return _productsRepository.Get();
		}

		// GET: api/Products/5
		[ResponseType(typeof(Product))]
		public IHttpActionResult GetProduct(int id)
		{
			Product product = _productsRepository.GetById(id);
			if (product == null)
			{
				return NotFound();
			}

			return Ok(product);
		}

		// PUT: api/Products/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutProduct(int id, Product product)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			try
			{
				_productsRepository.Update(id, product);
			}
			catch (Exception)
			{
				return BadRequest();
			}

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Products
		[ResponseType(typeof(Product))]
		public IHttpActionResult PostProduct(Product product)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			product = _productsRepository.Create(product);

			return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
		}

		// DELETE: api/Products/5
		[ResponseType(typeof(Product))]
		public IHttpActionResult DeleteProduct(int id)
		{
			Product product = _productsRepository.GetById(id);
			if (product == null)
			{
				return NotFound();
			}
			_productsRepository.Delete(id);
			return Ok(product);
		}
	}
}
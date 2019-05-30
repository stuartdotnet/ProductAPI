using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductAPI.Data;
using ProductAPI.Model;

namespace ProductAPI.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		IProductService _service;
		public ProductsController(IProductService service)
		{
			_service = service;
		}

		// GET: Products or odata/Products
		[HttpGet]
		[EnableQuery]
		public IEnumerable<Product> Get()
		{
			return _service.Products;
		}

		// GET: Products/5
		[HttpGet("{id}", Name = "Get")]
		public Product Get(string id)
		{
			return _service.Products.SingleOrDefault(p => p.Id == id);
		}

		// POST: Products
		[HttpPost]
		public ActionResult Post([FromBody] Product product)
		{
			if (!_service.Products.Any(p => p.Id == product.Id))
			{
				int lastProductId = _service.Products.Max(p => int.Parse(p.Id));
				string newProductId = (lastProductId + 1).ToString();
				product.Id = newProductId;
				_service.Products.Add(product);
				return Ok();
			}
			return new BadRequestObjectResult("Product already exists");
		}

		// PUT: Products/5
		[HttpPut]
		public ActionResult Put([FromBody] Product product)
		{
			var productToUpdate = _service.Products.FirstOrDefault(p => p.Id == product.Id);
			if (productToUpdate != null)
			{
				_service.Products[_service.Products.IndexOf(productToUpdate)] = product;
				return Ok();
			}
			return new BadRequestObjectResult("Product doesn't exist");
		}

		// DELETE: Products/5
		[Authorize]
		[HttpDelete("{id}")]
		public void Delete(string id)
		{
			Product product = _service.Products.FirstOrDefault(p => p.Id == id);
			if (product != null)
			{
				_service.Products.Remove(product);
			}
		}
	}
}

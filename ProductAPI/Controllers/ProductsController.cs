using System.Collections.Generic;
using System.Linq;
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

		// GET: Products
		[HttpGet]
        public IEnumerable<Product> Get()
        {
			return _service.Products.Select(p => p);
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
				int lastProduct = _service.Products.Max(p => int.Parse(p.Id));
				string newProductId = (lastProduct + 1).ToString();
				product.Id = newProductId;
				_service.Products.Add(product);
				return Ok();
			}
			return new BadRequestObjectResult("Product already exists");
        }

        // PUT: Products/5
        [HttpPut]
        public void Put([FromBody] Product product)
        {
			var productToUpdate = _service.Products.FirstOrDefault(p => p.Id == product.Id);
			if (productToUpdate != null)
			{
				int index;
				int.TryParse(product.Id, out index);

				_service.Products[_service.Products.IndexOf(productToUpdate)] = product;
			}
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

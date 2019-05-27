using System.Collections.Generic;
using System.Linq;
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
			return _service.Products.Select(p => p.Value);
        }

        // GET: Products/5
        [HttpGet("{id}", Name = "Get")]
        public Product Get(string id)
        {
			return _service.Products.SingleOrDefault(p => p.Key == id).Value;
        }

		// POST: Products
		[HttpPost]
        public void Post([FromBody] Product product)
        {
			if (!_service.Products.Any(p => p.Value.Id == product.Id))
			{
				int lastProduct = _service.Products.Max(p => int.Parse(p.Key));
				string newProductId = (lastProduct + 1).ToString();
				product.Id = newProductId;
				_service.Products.GetOrAdd(newProductId, product);
			}
        }

        // PUT: Products/5
        [HttpPut]
        public void Put([FromBody] Product product)
        {
			var productToUpdate = _service.Products.FirstOrDefault(p => p.Value.Id == product.Id).Value;
			if (productToUpdate != null)
			{
				_service.Products[product.Id] = product;
			}
		}

        // DELETE: Products/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
			Product product;
			_service.Products.Remove(id, out product);
		}
    }
}

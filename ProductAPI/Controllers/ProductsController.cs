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
			return _service.Cache.Select(p => p.Value);
        }

        // GET: Products/5
        [HttpGet("{id}", Name = "Get")]
        public Product Get(string id)
        {
			return _service.Cache.SingleOrDefault(p => p.Key == id).Value;
        }

        // POST: Products
        [HttpPost]
        public void Post([FromBody] Product product)
        {
			if (!_service.Cache.Any(p => p.Value.Id == product.Id))
			{
				_service.Cache.GetOrAdd(product.Id, product);
			}
        }

        // PUT: Products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product product)
        {
			if (!_service.Cache.Any(p => p.Value.Id == product.Id))
			{
				_service.Cache.GetOrAdd(product.Id, product);
			}
		}

        // DELETE: Products/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
			Product product;
			_service.Cache.Remove(id, out product);
		}
    }
}

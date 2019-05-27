using ProductAPI.Data;
using ProductAPI.Model;
using System.Collections.Generic;

namespace ProductAPI.Tests
{
	public class FakeProductService : IProductService
	{
		private List<Product> _cache;
		public FakeProductService(int itemCount)
		{
			_cache = new List<Product>();

			for (int i = 1; i <= itemCount; i++)
			{
				_cache.Add(new Product { Id = i.ToString(), Description = $"Test Item {i}", Model = $"Model{i}", Brand = $"Brand{i}" });
			}
		}

		public List<Product> Products
		{
			get
			{
				return _cache;
			}
		}
	}
}

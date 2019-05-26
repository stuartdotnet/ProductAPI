using ProductAPI.Data;
using ProductAPI.Model;
using System.Collections.Concurrent;

namespace ProductAPI.Tests
{
	public class FakeProductService : IProductService
	{
		private ConcurrentDictionary<string, Product> _cache;
		public FakeProductService(int itemCount)
		{
			_cache = new ConcurrentDictionary<string, Product>();

			for (int i = 1; i <= itemCount; i++)
			{
				_cache.GetOrAdd(i.ToString(), new Product { Id = i.ToString(), Description = $"Test Item {i}", Model = $"Model{i}", Brand = $"Brand{i}" });
			}
		}

		public ConcurrentDictionary<string, Product> Cache
		{
			get
			{
				return _cache;
			}
		}
	}
}

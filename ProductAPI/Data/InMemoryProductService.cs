using ProductAPI.Model;
using System.Collections.Concurrent;

namespace ProductAPI.Data
{
	public class InMemoryProductService : IProductService
	{
		private static ConcurrentDictionary<string, Product> cache;

		private static object cacheLock = new object();

		public InMemoryProductService()
		{
			Product[] products = new Product[]
			{
			new Product { Id = "1", Description = "98 inch smart oven", Model = "J2123", Brand = "Sony" },
			new Product { Id = "2", Description = "Mahogany Wooden Drone", Model = "DX-900", Brand = "NSA" },
			new Product { Id = "3", Description = "Pipe Organ Keyboard", Model = "KW-XML", Brand = "Kawasaki" }
			};

			foreach (Product product in products)
			{
				this.Cache.GetOrAdd(product.Id, product);
			}
		}

		public ConcurrentDictionary<string, Product> Cache
		{
			get
			{
				lock (cacheLock)
				{
					if (cache == null)
					{
						cache = new ConcurrentDictionary<string, Product>();
					}
					return cache;
				}
			}
		}


	}
}

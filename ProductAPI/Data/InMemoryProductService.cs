using ProductAPI.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace ProductAPI.Data
{
	public class InMemoryProductService : IProductService
	{
		private static List<Product> cache;

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
				this.Products.Add(product);
			}
		}

		public List<Product> Products
		{
			get
			{
				lock (cacheLock)
				{
					if (cache == null)
					{
						cache = new List<Product>();
					}
					return cache;
				}
			}
		}


	}
}

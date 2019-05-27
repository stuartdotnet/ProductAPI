using ProductAPI.Model;
using System.Collections.Generic;

namespace ProductAPI.Data
{
	public interface IProductService
	{
		List<Product> Products { get; }
	}
}

using ProductAPI.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductAPI.Data
{
	public interface IProductService
	{
		ConcurrentDictionary<string, Product> Products { get; }
	}
}

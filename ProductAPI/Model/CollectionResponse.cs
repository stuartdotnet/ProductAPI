using System;
using System.Collections.Generic;

namespace ProductAPI.Model
{
	public class CollectionResponse<T> where T : class
	{
		public IEnumerable<T> Items { get; set; }
	}
}

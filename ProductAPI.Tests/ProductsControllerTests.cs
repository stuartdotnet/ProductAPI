using ProductAPI.Controllers;
using ProductAPI.Data;
using ProductAPI.Model;
using System.Linq;
using Xunit;

namespace ProductAPI.Tests
{
	public class ProductsControllerTests
	{
		IProductService _service;
		public ProductsControllerTests()
		{
			// Default
			_service = new FakeProductService(itemCount: 3);
		}

		 [Fact]
		 public void ProductGet_WhenProducts_ReturnsAllProducts()
		{
			const int ITEMCOUNT = 6;
			_service = new FakeProductService(ITEMCOUNT);
			var controller = new ProductsController(_service);

			// Act
			var result = controller.Get();

			// Assert
			Assert.Equal(ITEMCOUNT, result.Count());
		}

		[Fact]
		public void ProductGet_WhenProductRequestedById_ReturnsCorrectProduct()
		{
			var controller = new ProductsController(_service);

			// Act
			var result = controller.Get("2");

			// Assert
			Assert.Equal("2", result.Id);
			Assert.Equal("Test Item 2", result.Description);
			Assert.Equal("Brand2", result.Brand);
			Assert.Equal("Model2", result.Model);
		}

		[Fact]
		public void ProductPost_WhenProductComplete_AddedToCache()
		{
			var controller = new ProductsController(_service);
			var product = new Product { Id = "4", Description = "Test Item 4!!!", Brand = "The Great Guys", Model = "Test" };

			// Act
			controller.Post(product);

			// Assert
			var result = _service.Cache.Single(p => p.Key == "4").Value;
			Assert.Equal("4", result.Id);
			Assert.Equal("Test Item 4!!!", result.Description);
			Assert.Equal("The Great Guys", result.Brand);
			Assert.Equal("Test", result.Model);
		}
	}
}

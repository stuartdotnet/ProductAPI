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
		 public void ProductGet_WhenProductsGot_ReturnsAllProducts()
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
		public void ProductPost_WhenProductComplete_AddedToCacheIncludingId()
		{
			var controller = new ProductsController(_service);
			var product = new Product { Description = "Test Item 4!!!", Brand = "The Great Guys", Model = "Test" };

			// Act
			controller.Post(product);

			// Assert
			var result = _service.Products.Single(p => p.Key == "4").Value;
			Assert.Equal("4", result.Id);
			Assert.Equal("Test Item 4!!!", result.Description);
			Assert.Equal("The Great Guys", result.Brand);
			Assert.Equal("Test", result.Model);
		}

		[Fact]
		public void ProductPut_WhenUpdaingProduct_ProductUpdated()
		{
			var controller = new ProductsController(_service);

			Product product = new Product
			{
				Id = "2",
				Description = "Something new",
				Brand = "Updated Brand",
				Model = "Test"
			};

			controller.Put(product);

			var updatedProduct = _service.Products.Single(p => p.Key == "2").Value;

			Assert.Equal("Something new", updatedProduct.Description);
			Assert.Equal("Updated Brand", updatedProduct.Brand);
			Assert.Equal("Test", updatedProduct.Model);
		}

		[Fact]
		public void ProductDelete_WhenProductDeleted_ProductIsDeleted()
		{
			const string IDTODELETE = "2";
			int startingCount = _service.Products.Count;
			var controller = new ProductsController(_service);
			controller.Delete(IDTODELETE);

			var result = _service.Products.FirstOrDefault(p => p.Key == IDTODELETE);

			Assert.True(result.Value == null);
			Assert.Equal(startingCount - 1, _service.Products.Count);
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using Moq;

namespace SportsStore.Tests
{
	public class HomeControllerTests
	{
		[Fact]
		public void CanUseRepository()
		{
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductId = 1, Name = "P1"},
				new Product {ProductId = 2, Name = "P2"}
			}).AsQueryable());

			HomeController controller = new HomeController(mock.Object);

			ProductsListViewModels result = controller.Index(null).ViewData.Model as ProductsListViewModels ?? new();

			//IEnumerable<Product>? result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

			Product[] prodArray = result.Products.ToArray();
			//Product[] prodArray = result?.ToArray() ?? Array.Empty<Product>();
			Assert.True(prodArray.Length == 2);
			Assert.Equal("P1", prodArray[0].Name);
			Assert.Equal("P2", prodArray[1].Name);
		}

		[Fact]
		public void CanPaginate()
		{
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductId = 1, Name = "P1"},
				new Product {ProductId = 2, Name = "P2"},
				new Product {ProductId = 3, Name = "P3"},
				new Product {ProductId = 4, Name = "P4"},
				new Product {ProductId = 5, Name = "P5"}
			}).AsQueryable());

			HomeController controller = new HomeController(mock.Object);
			controller.pageSize = 3;

			ProductsListViewModels result = controller.Index(null, 2).ViewData.Model as ProductsListViewModels ?? new();

			//IEnumerable<Product>? result = (controller.Index(2) as ViewResult)?.ViewData.Model as IEnumerable<Product> ?? Enumerable.Empty<Product>();

			Product[] prodArray = result.Products.ToArray();
			Assert.True(prodArray.Length == 2);
			Assert.Equal("P4", prodArray[0].Name);
			Assert.Equal("P5", prodArray[1].Name);
		}

		[Fact]
		public void CanSendPaginationViewModel()
		{
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductId = 1, Name = "P1"},
				new Product {ProductId = 2, Name = "P2"},
				new Product {ProductId = 3, Name = "P3"},
				new Product {ProductId = 4, Name = "P4"},
				new Product {ProductId = 5, Name = "P5"}
			}).AsQueryable());

			HomeController controller = new HomeController(mock.Object) { pageSize = 3};

			ProductsListViewModels result = controller.Index(null, 2).ViewData.Model as ProductsListViewModels ?? new();

			PagingInfo pageInfo = result.PagingInfo;
			Assert.Equal(2, pageInfo.CurrentPage);
			Assert.Equal(3, pageInfo.ItemsPerPage);
			Assert.Equal(5, pageInfo.TotalItems);
			Assert.Equal(2, pageInfo.TotalPages);
		}

		[Fact]
		public void CanFilterProducts()
		{
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
				new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
				new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
				new Product {ProductId = 4, Name = "P4", Category = "Cat2"},
				new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
			}).AsQueryable());

			HomeController controller = new HomeController(mock.Object);
			controller.pageSize = 3;

			Product[] result = (controller.Index("Cat2", 1).ViewData.Model as ProductsListViewModels ?? new ProductsListViewModels()).Products.ToArray();

			Assert.Equal(2, result.Length);
			Assert.True(result[0].Name == "P2" && result[0].Category == "Cat2");
			Assert.True(result[1].Name == "P4" && result[1].Category == "Cat2");
		}
	}
}

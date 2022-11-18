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

			ProductsListViewModels result = controller.Index().ViewData.Model as ProductsListViewModels ?? new();

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

			ProductsListViewModels result = controller.Index(2).ViewData.Model as ProductsListViewModels ?? new();

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

			ProductsListViewModels result = controller.Index(2).ViewData.Model as ProductsListViewModels ?? new();

			PagingInfo pageInfo = result.PagingInfo;
			Assert.Equal(2, pageInfo.CurrentPage);
			Assert.Equal(3, pageInfo.ItemsPerPage);
			Assert.Equal(5, pageInfo.TotalItems);
			Assert.Equal(2, pageInfo.TotalPages);
		}
	}
}

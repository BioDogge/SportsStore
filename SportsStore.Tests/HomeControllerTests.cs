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

			IEnumerable<Product>? result = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<Product>;

			Product[] prodArray = result?.ToArray() ?? Array.Empty<Product>();
			Assert.True(prodArray.Length == 2);
			Assert.Equal("P1", prodArray[0].Name);
			Assert.Equal("P2", prodArray[1].Name);
		}
	}
}

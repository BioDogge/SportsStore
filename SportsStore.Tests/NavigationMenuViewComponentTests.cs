using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

namespace SportsStore.Tests
{
	public class NavigationMenuViewComponentTests
	{
		[Fact]
		public void CanSelectCategories()
		{
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductId = 1, Name = "P1", Category = "Apples"},
				new Product {ProductId = 2, Name = "P2", Category = "Apples"},
				new Product {ProductId = 3, Name = "P3", Category = "Plums"},
				new Product {ProductId = 4, Name = "P4", Category = "Oranges"}
			}).AsQueryable());

			NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);

			string[] results = ((IEnumerable<string>?)(target.Invoke() as ViewViewComponentResult)?
				.ViewData?.Model ?? Enumerable.Empty<string>()).ToArray();

			Assert.True(Enumerable.SequenceEqual(new string[] { "Apples", "Oranges", "Plums" }, results));
		}

		[Fact]
		public void IndicatesSelectedCategory()
		{
			string categoryToSelect = "Apples";
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[]
			{
				new Product {ProductId = 1, Name = "P1", Category = "Apples"},
				new Product {ProductId = 4, Name = "P2", Category = "Oranges"}
			}).AsQueryable());

			NavigationMenuViewComponent target = new NavigationMenuViewComponent(mock.Object);
			target.ViewComponentContext = new ViewComponentContext { ViewContext = new ViewContext { RouteData = new RouteData() } };
			target.RouteData.Values["category"] = categoryToSelect;

			string? result = (string?)(target.Invoke() as ViewViewComponentResult)?.ViewData?["SelectedCategory"];

			Assert.Equal(categoryToSelect, result);
		}
	}
}

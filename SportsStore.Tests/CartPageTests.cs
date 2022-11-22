using SportsStore.Pages;

namespace SportsStore.Tests
{
	public class CartPageTests
	{
		[Fact]
		public void CanLoadCart()
		{
			Product p1 = new Product { ProductId = 1, Name = "P1" };
			Product p2 = new Product { ProductId = 2, Name = "P2" };
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[] { p1, p2 }).AsQueryable<Product>());

			Cart testCart = new Cart();
			testCart.AddItem(p1, 2);
			testCart.AddItem(p2, 1);

			CartModel cartModel = new CartModel(mock.Object, testCart);
			cartModel.OnGet("myUrl");

			Assert.Equal(2, cartModel.Cart.Lines.Count());
			Assert.Equal("myUrl", cartModel.ReturnUrl);
		}

		[Fact]
		public void CanUpdateCart()
		{
			Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
			mock.Setup(m => m.Products).Returns((new Product[] { new Product { ProductId = 1, Name = "P1" } }).AsQueryable());

			Cart testCart = new Cart();

			CartModel cartModel = new CartModel(mock.Object, testCart);
			cartModel.OnPost(1, cartModel.ReturnUrl);

			Assert.Single(testCart.Lines);
			Assert.Equal("P1", testCart.Lines.First().Product.Name);
			Assert.Equal(1, testCart.Lines.First().Quantity);
		}
	}
}
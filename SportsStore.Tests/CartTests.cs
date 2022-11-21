
namespace SportsStore.Tests
{
	public class CartTests
	{
		[Fact]
		public void CanAddNewLines()
		{
			Product p1 = new Product { ProductId = 1, Name = "P1" };
			Product p2 = new Product { ProductId = 2, Name = "P2" };

			Cart target = new Cart();

			target.AddItem(p1, 1);
			target.AddItem(p2, 1);
			CartLine[] result = target.Lines.ToArray();

			Assert.Equal(2, result.Length);
			Assert.Equal(p1, result[0].Product);
			Assert.Equal(p2, result[1].Product);
		}

		[Fact]
		public void CanAddQuantityForExistingLines()
		{
			Product p1 = new Product { ProductId = 1, Name = "P1" };
			Product p2 = new Product { ProductId = 2, Name = "P2" };

			Cart target = new Cart();

			target.AddItem(p1, 1);
			target.AddItem(p2, 1);
			target.AddItem(p1, 10);

			CartLine[] result = target.Lines.ToArray();

			Assert.Equal(2, result.Length);
			Assert.Equal(11, result[0].Quantity);
			Assert.Equal(1, result[1].Quantity);
		}

		[Fact]
		public void CanRemoveLine()
		{
			Product p1 = new Product { ProductId = 1, Name = "P1" };
			Product p2 = new Product { ProductId = 2, Name = "P2" };
			Product p3 = new Product { ProductId = 3, Name = "P3" };

			Cart target = new Cart();

			target.AddItem(p1, 1);
			target.AddItem(p2, 3);
			target.AddItem(p3, 1);
			target.AddItem(p2, 2);

			target.RemoveLine(p2);

			Assert.Empty(target.Lines.Where(p => p.Product == p2));
			Assert.Equal(2, target.Lines.Count());
		}

		[Fact]
		public void CalculateCartTotal()
		{
			Product p1 = new Product { ProductId = 1, Name = "P1", Price = 100M };
			Product p2 = new Product { ProductId = 2, Name = "P2", Price = 50M };

			Cart target = new Cart();

			target.AddItem(p1, 1);
			target.AddItem(p2, 1);

			decimal result = target.ComputeTotalValue();

			Assert.Equal(150, result);
		}

		[Fact]
		public void CanClearLines()
		{
			Product p1 = new Product { ProductId = 1, Name = "P1" };
			Product p2 = new Product { ProductId = 2, Name = "P2" };

			Cart target = new Cart();

			target.AddItem(p1, 1);
			target.AddItem(p2, 1);

			target.Clear();

			Assert.Empty(target.Lines);
		}
	}
}

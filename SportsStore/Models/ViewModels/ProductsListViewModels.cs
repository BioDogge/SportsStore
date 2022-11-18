namespace SportsStore.Models.ViewModels
{
	public class ProductsListViewModels
	{
		public IEnumerable<Product> Products { get; set; } = Enumerable.Empty<Product>();

		public PagingInfo PagingInfo { get; set; } = new();
	}
}

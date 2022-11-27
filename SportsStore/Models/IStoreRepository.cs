namespace SportsStore.Models
{
	public interface IStoreRepository
	{
		IQueryable<Product> Products { get; }

		void CreateProduct(Product product);
		void SaveProduct(Product product);
		void DeleteProduct(Product product);
	}
}
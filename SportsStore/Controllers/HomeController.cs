using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers
{
	public class HomeController : Controller
	{
		private IStoreRepository repository;
		public int pageSize = 4;

		public HomeController(IStoreRepository repo)
		{
			repository = repo;
		}

		public ViewResult Index(int productPage = 1)
		{
			return View(new ProductsListViewModels
			{
				Products = repository.Products
					.OrderBy(p => p.ProductId)
					.Skip((productPage - 1) * pageSize)
					.Take(pageSize),
				PagingInfo = new PagingInfo
				{
					CurrentPage = productPage,
					ItemsPerPage = pageSize,
					TotalItems = repository.Products.Count()
				}
			});
		}
	}
}

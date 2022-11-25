using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
	public class Order
	{
		[BindNever]
		public int OrderId { get; set; }

		[BindNever]
		public ICollection<CartLine> Lines { get; set; } = new List<CartLine>();

		[Required(ErrorMessage = "Please enter the name")]
		public string? Name { get; set; }

		[Required(ErrorMessage = "Please enter the first address")]
		public string? LineOne { get; set; }
		public string? LineTwo { get; set; }
		public string? LineThree { get; set; }

		[Required(ErrorMessage = "Please enter a city name")]
		public string? City { get; set; }

		[Required(ErrorMessage = "Please enter a state name")]
		public string? State { get; set; }
		
		public string? Zip { get; set; }

		[Required(ErrorMessage = "Please enter a country name")]
		public string? Country { get; set; }

		public bool GiftWrap { get; set; }

		[BindNever]
		public bool Shipped { get; set; }
	}
}
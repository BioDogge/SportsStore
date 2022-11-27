using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
	public static class IdentitySeedData
	{
		private const string ADMIN_USER = "Admin";
		private const string ADMIN_PASSWORD = "Secret123$";

		public static async void EnsurePopulated(IApplicationBuilder application)
		{
			AppIdentityDbContext context = application.ApplicationServices
				.CreateScope().ServiceProvider
				.GetRequiredService<AppIdentityDbContext>();

			if (context.Database.GetPendingMigrations().Any())
				context.Database.Migrate();

			UserManager<IdentityUser> userManager = application.ApplicationServices
				.CreateScope().ServiceProvider
				.GetRequiredService<UserManager<IdentityUser>>();

			IdentityUser user = await userManager.FindByNameAsync(ADMIN_USER);

			if (user == null)
			{
				user = new IdentityUser(ADMIN_USER);
				user.Email = "admin@examile.com";
				user.PhoneNumber = "555-1234";
				await userManager.CreateAsync(user, ADMIN_PASSWORD);
			}
		}
	}
}
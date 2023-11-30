using System.Reflection.Metadata;
using URLShortener.Models;

namespace URLShortener.Services
{
	public class DataSeeder
	{
		public static void SeedUsers()
		{
			using (var context = new ShortenerDBContext())
			{

				var testUser = context.Users.FirstOrDefault(b => b.Username == "testuser");
				if (testUser == null)
				{
					context.Users.Add(new User { Username = "testuser", PasswordHash=Authenticator.HashPassword("12345"), IsAdmin=false });
				}

				context.SaveChanges();
			}
		}
	}
}

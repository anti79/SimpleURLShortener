using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace URLShortener.Models
{
	public class User
	{
		public int UserId { get; set; }

		[Index(IsUnique = true)]
		[MaxLength(255)]
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public bool IsAdmin { get; set; }
		public string Token { get; set; }
	}
}

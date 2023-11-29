using System.Data.Entity;
using System.Reflection.Metadata;
using URLShortener.Models;

namespace URLShortener
{
	public class ShortenerDBContext : DbContext
	{
		public DbSet<URLRecord> Records { get; set; }
	}
}

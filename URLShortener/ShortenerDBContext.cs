using System.Data.Entity;
using System.Data.Entity.Hierarchy;
using System.Reflection.Metadata;
using URLShortener.Models;

namespace URLShortener
{
	public class ShortenerDBContext : DbContext
	{
		public DbSet<URLRecord> Records { get; set; }
		public DbSet<User> Users { get; set; }
		public object User { get; internal set; }
	}
}

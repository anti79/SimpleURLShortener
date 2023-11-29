namespace URLShortener.Models
{
	public class URLRecord
	{
		public int URLRecordId { get; set; }
		public string OriginalURL { get; set; }
		public string ShortenedURL { get; set; }
	}
}

namespace URLShortener.Models
{
	public class GetRecordsResponse:RequestResponse
	{
		public int Total { get; set; }
		public IEnumerable<URLRecord> Records { get; set; }
	}
}

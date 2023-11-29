namespace URLShortener.Models
{
	public class GetRecordsResponse:RequestResponse
	{
		public IEnumerable<URLRecord> Records { get; set; }
	}
}

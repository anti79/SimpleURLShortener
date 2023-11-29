using System.Text.Json.Serialization;

namespace URLShortener.Models
{
	
	public class ShortenResponse:RequestResponse
	{
		
		
		public URLRecord Result { get; set; }
		public bool AlreadyExists { get; set; }
	}
}

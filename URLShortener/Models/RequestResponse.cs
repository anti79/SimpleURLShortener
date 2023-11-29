namespace URLShortener.Models
{
	public enum Status
	{
		success,
		error
	}
	public class RequestResponse
	{
		public Status Status { get; set; }
		public string Error { get; set; }
	}
}

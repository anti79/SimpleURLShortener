using Microsoft.AspNetCore.Mvc;
using URLShortener.Models;
using URLShortener.Services;


namespace URLShortener.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ShortenController : ControllerBase
	{
		[HttpPost]
		public ShortenResponse Post([FromQuery] string url)
		{
			try
			{
				url = URLNormalizer.Normalize(url);
			}
			catch (UriFormatException ufex)
			{
				return new ShortenResponse() { Status = Status.error, Error = "Invalid URL" };
			}

			try
			{
				using (var db = new ShortenerDBContext())
				{
					
					var existing = db.Records.Where(x => x.OriginalURL == url).FirstOrDefault();
					if (existing != null)
					{
						return new ShortenResponse() { Status = Status.success, Result = existing, AlreadyExists=true };
					}
					URLRecord newRecord = new URLRecord() { OriginalURL = url, ShortenedURL = "" };
					newRecord = db.Records.Add(newRecord);
					db.SaveChanges();
					newRecord.ShortenedURL = URLGenerator.IdToURL(newRecord.URLRecordId);
					db.SaveChanges();
					return new ShortenResponse() { Status = Status.success, Result = newRecord, AlreadyExists=false };
				}
			}
			catch(Exception ex)
			{
				Response.StatusCode = 400;
				return new ShortenResponse() { Status = Status.error, Error=ex.Message };
			}
		}
	}
}

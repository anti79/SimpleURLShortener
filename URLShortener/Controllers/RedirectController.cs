using Microsoft.AspNetCore.Mvc;

namespace URLShortener.Controllers
{
	public class RedirectController : Controller
	{
		String[] angularRoutes = new String[] { "login", "about" };

		[HttpGet]
		public IActionResult Get()
		{
			string currentUrl = Request.Path.ToString().Substring(1);
			using (var db = new ShortenerDBContext())
			{
				var urlRecord = db.Records.Where(x => x.ShortenedURL == currentUrl).FirstOrDefault();
				if(urlRecord==null)
				{
					if(angularRoutes.Contains(currentUrl))
					{
						return Redirect($"/?redir={currentUrl}");
					}
					return Redirect("/?redir=not_found");
				}
				else
				{
					return Redirect(urlRecord.OriginalURL);
				}
			}

			
		}
	}
}

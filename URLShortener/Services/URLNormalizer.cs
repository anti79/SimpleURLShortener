using System.Text.RegularExpressions;

namespace URLShortener.Services
{
	public class URLNormalizer
	{
		public static string Normalize(string url)
		{
		
			url = url.ToLower();
			Regex urlPattern = new Regex("^https?:\\/\\/(?:www\\.)?[-a-zA-Z0-9@:%._\\+~#=]{1,256}\\.[a-zA-Z0-9()]{1,6}\\b(?:[-a-zA-Z0-9()@:%_\\+.~#?&\\/=]*)$");
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
				{
					url = "https://" + url;
				}

			url = Uri.UnescapeDataString(url);
			url = url.Replace(":80/", "/").Replace(":443/", "/");
			url = new Uri(url).AbsoluteUri;


			if(!urlPattern.IsMatch(url))
			{
				throw new UriFormatException("Invalid URL");
			}

			return url;
			
		}

		static bool IsValid(string url)
		{
			return Uri.TryCreate(url, UriKind.Absolute, out Uri result)
				   && (result.Scheme == Uri.UriSchemeHttp || result.Scheme == Uri.UriSchemeHttps);
		}
	}
}

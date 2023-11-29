using System.Text.RegularExpressions;

namespace URLShortener.Services
{
	public class URLNormalizer
	{
		public static string Normalize(string url)
		{
		
			url = url.ToLower();
			string urlPattern = @"^(https?|ftp):\/\/[^\s/$.?#].[^\s]*$";
			if (!url.StartsWith("http://") && !url.StartsWith("https://"))
				{
					url = "https://" + url;
				}

			url = Uri.UnescapeDataString(url);
			url = url.Replace(":80/", "/").Replace(":443/", "/");
			url = new Uri(url).AbsoluteUri;


			if(!Regex.IsMatch(url, urlPattern, RegexOptions.IgnoreCase))
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

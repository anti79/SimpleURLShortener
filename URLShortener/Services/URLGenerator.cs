using System.Text;

namespace URLShortener.Services
{
	public class URLGenerator
	{
		const string characters = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public static string IdToURL(int id)
		{
			if (id == 0)
			{
				return characters[0].ToString();
			}

			StringBuilder result = new StringBuilder();

			while (id > 0)
			{
				int remainder = (int)(id % 62);
				result.Insert(0, characters[remainder]);
				id /= 62;
			}

			return result.ToString();
		}

		public static int URLToId(string url)
		{
			int result = 0;

			for (int i = 0; i < url.Length; i++)
			{
				char currentChar = url[i];
				int charIndex = characters.IndexOf(currentChar);

				if (charIndex == -1)
				{
					throw new ArgumentException($"Invalid character '{currentChar}' in base62Number");
				}

				result = result * 62 + charIndex;
			}

			return result;
		}
	}
}

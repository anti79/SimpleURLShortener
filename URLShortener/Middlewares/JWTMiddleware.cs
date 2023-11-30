using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace URLShortener.Middlewares
{
	public class JwtMiddleware
	{
		private readonly RequestDelegate _next;

		public JwtMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			var token = context.Request.Cookies["token"];

			if (!string.IsNullOrEmpty(token))
			{
				try
				{
					// Validate the JWT token
					var handler = new JwtSecurityTokenHandler();
					var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
					bool valid = false;
					using(var db = new ShortenerDBContext())
					{
						valid = db.Users.Where(u => u.Token == token).Any();
					}
					if(!valid)
					{
						throw new Exception("Token doesn't exist");
					}
					context.Items["User"] = jsonToken?.Subject;
				}
				catch (Exception ex)
				{
					// Token validation failed
					context.Response.StatusCode = 401; // Unauthorized
					await context.Response.WriteAsync("Invalid token");
					return;
				}
			}
			else
			{
				// Token is missing
				context.Response.StatusCode = 401; // Unauthorized
				await context.Response.WriteAsync("Token is missing");
				return;
			}

			await _next(context);
		}
	}

	public static class JwtMiddlewareExtensions
	{
		public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<JwtMiddleware>();
		}
	}
}

using Microsoft.AspNetCore.Mvc;
using URLShortener.Models;
using URLShortener.Services;

namespace URLShortener.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost("login")]
		public LoginResponse Login([FromBody] LoginRequest request)
		{
			//var response = Authenticator.generateJWTToken(username);
			User? user = Authenticator.Login(request);
			if (user!=null)
			{
				string jwt = Authenticator.generateJWTToken(user);
				using(var db = new ShortenerDBContext())
				{
					var u = db.Users.Where(u => u.UserId == user.UserId).FirstOrDefault();
					u.Token = jwt;
					db.SaveChanges();
				}
				

				return new LoginResponse() { Token = jwt };
			}
			Response.StatusCode = 400;
			return new LoginResponse() { Error = "Username or password is incorrect", Status=Status.error };

		}
	}
}

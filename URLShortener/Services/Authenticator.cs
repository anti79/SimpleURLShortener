using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using URLShortener.Models;

namespace URLShortener.Services
{
	public class Authenticator
	{

		public static string Key { get; set; }

		private const int SaltSize = 16; // Size of the salt in bytes
		private const int HashSize = 20; // Size of the hash in bytes
		private const int Iterations = 100; // Number of iterations for the PBKDF2 algorithm
		
		public static string generateJWTToken(User user)
		{
			// generate token that is valid for 7 days
			var tokenHandler = new JwtSecurityTokenHandler();
			
			var key = Encoding.ASCII.GetBytes(Key);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] { new Claim("id", user.UserId.ToString()) }),
				Expires = DateTime.UtcNow.AddDays(7),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		public static User? Login(LoginRequest request)
		{
			using (var db = new ShortenerDBContext())
			{
				var userInDb = db.Users.Where(u => u.Username == request.Username).FirstOrDefault();
				if (VerifyPassword(request.Password, userInDb.PasswordHash)) return userInDb;
				return null;
			}
		}

		public static string HashPassword(string password)
		{
			// Generate a random salt
			byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltSize]);

			// Create the hash with the password and salt using PBKDF2
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
			byte[] hash = pbkdf2.GetBytes(HashSize);

			// Combine the salt and hash into a single byte array
			byte[] hashBytes = new byte[SaltSize + HashSize];
			Array.Copy(salt, 0, hashBytes, 0, SaltSize);
			Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

			// Convert the byte array to a base64-encoded string and return
			return Convert.ToBase64String(hashBytes);
		}

		public static bool VerifyPassword(string password, string hashedPassword)
		{
			// Convert the base64-encoded string back to a byte array
			byte[] hashBytes = Convert.FromBase64String(hashedPassword);

			// Extract the salt from the beginning of the byte array
			byte[] salt = new byte[SaltSize];
			Array.Copy(hashBytes, 0, salt, 0, SaltSize);

			// Create the hash with the provided password and extracted salt using PBKDF2
			var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations);
			byte[] hash = pbkdf2.GetBytes(HashSize);

			// Compare the computed hash with the stored hash
			for (int i = 0; i < HashSize; i++)
			{
				if (hashBytes[i + SaltSize] != hash[i])
				{
					return false; // Passwords don't match
				}
			}

			return true; // Passwords match
		}
	}
}

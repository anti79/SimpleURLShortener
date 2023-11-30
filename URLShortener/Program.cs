
using System.Text.Json.Serialization;
using URLShortener.Middlewares;
using URLShortener.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddControllers().AddJsonOptions(options =>
{
	options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	options.JsonSerializerOptions.DefaultIgnoreCondition =
		JsonIgnoreCondition.WhenWritingNull;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
	

	app.MapControllerRoute(
	name: "default",
	pattern: "{controller}/{action=Index}/{id?}");

	// Catch-all route for shortened links

	endpoints.MapControllerRoute(
		name: "CatchAll",
		pattern: "{*url}",
		defaults: new { controller = "Redirect", action = "Get" }
	);

	// Info pages for shortened links
	/*
	endpoints.MapControllerRoute(
		name: "CatchAll",
		pattern: "{*url}/info",
		defaults: new { controller = "Records", action = "Get" }
	);
	*/

	app.Map("/api/shorten", securedApp =>
	{
		securedApp.UseJwtMiddleware();

		// Additional middleware or endpoint configurations for the "/secured" route
		securedApp.UseEndpoints(endpoints =>
		{
			endpoints.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");
		});
	});


	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}"
	);
});


app.MapFallbackToFile("index.html"); ;
DataSeeder.SeedUsers();
Authenticator.Key = "OfED+KgbZxtu4e4+JSQWdtSgTnuNixKy1nMVAEww8QL3IN3idcNgbNDSSaV4491Fo3sq2aGSCtYvekzs7JwXJnNAyvDSJjfK/7M8MpxSMnm1vMscBXyiYFXhGC4wqWlYBE828/5DNyw3QZW5EjD7hvDrY5OlYd4smCTa53helNnJz5NT9HQaDbE2sMwIDAQABAoIBAEs63TvT94njrPDP3A/sfCEXg1F2y0D/PjzUhM1aJGcRiOUXnGlYdViGhLnnJoNZTZm9qI1LT0NWcDA5NmBN6gcrk2EApyTt1D1i4AQ66rYoTF9iEC4Wye28v245BYESA6IIelgIxXGsVyllERsbTkaphzibbYfHmvwMxkn135Zfzd/NOXl/O32vYIomzrNEP+tN2WXhhG8c8+iZ8PErBV3CqrYogYy97d2CeQbXcpd5unPiU4TK0nnzeBAXdgeYuJHFC45YHl9UvShRoe6CHR47ceIGp6WMc5BTyyTkZpctuYJTwaChdj/QuRSkTYmn6jFL+MRfYQJ8VVwSVo5DbkECgYEA4/YIMKcwObYcSuHzgkMwH645CRDoy9M98eptAoNLdJBHYz23U5IbGL1+qHDDCPXxKs9ZG7EEqyWezq42eoFoebLA5O6/xrYXoaeIb094dbCF4D932hAkgAaAZkZVsSiWDCjYSV+JoWX4NVBcIL9yyHRhaaPVULTRbPsZQWq9+hMCgYEA48j4RGO7CaVpgUVobYasJnkGSdhkSCd1VwgvHH3vtuk7/JGUBRaZc0WZGcXkAJXnLh7QnDHOzWASdaxVgnuviaDi4CIkmTCfRqPesgDR2Iu35iQsH7P2/o1pzhpXQS/Ct6J7/GwJTqcXCvp4tfZDbFxS8oewzp4RstILj+pDyWECgYByQAbOy5xB8GGxrhjrOl1OI3V2c8EZFqA/NKy5y6/vlbgRpwbQnbNy7NYj+Y/mV80tFYqldEzQsiQrlei78Uu5YruGgZogL3ccj+izUPMgmP4f6+9XnSuN9rQ3jhy4k4zQP1BXRcim2YJSxhnGV+1hReLknTX2IwmrQxXfUW4xfQKBgAHZW8qSVK5bXWPjQFnDQhp92QM4cnfzegxe0KMWkp+VfRsrw1vXNx";
app.Run();


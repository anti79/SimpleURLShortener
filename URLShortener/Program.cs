
using System.Text.Json.Serialization;

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

	// Catch-all route

	endpoints.MapControllerRoute(
		name: "CatchAll",
		pattern: "{*url}",
		defaults: new { controller = "Redirect", action = "Get" }
	);

	

	// Default route (if needed)
	endpoints.MapControllerRoute(
		name: "default",
		pattern: "{controller=Home}/{action=Index}/{id?}"
	);
});


app.MapFallbackToFile("index.html"); ;

app.Run();

using backend.Data;

// Class for calling the function DbInitializer for better code clarity
public static class Extensions
{
	public static void CreateDbIfNotExists(this IHost host)
	{
		using (var scope = host.Services.CreateScope())
		{
			var services = scope.ServiceProvider;
			var context = services.GetRequiredService<CountryContext>();
			context.Database.EnsureCreated();
			DbInitializer.Initialize(context);
		}
	}
}

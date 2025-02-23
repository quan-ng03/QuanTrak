using frontend;
using MudBlazor.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
var backendUri = new Uri("http://localhost:5181");

var httpClient = new HttpClient
{
    BaseAddress = backendUri
};
// Inject API key
httpClient.DefaultRequestHeaders.Add("X-Api-Key", "H\"G(c}{W-Y5?@#[K");
builder.Services.AddScoped(sp => httpClient);
builder.Services.AddScoped<CountryService>();
builder.Services.AddMudServices();


await builder.Build().RunAsync();

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Joana.Web;
using Joana.Web.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

const string ApiBaseAddress = "http://localhost:5295/";


builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(ApiBaseAddress) });
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient<ApiService>(client => client.BaseAddress = new Uri(ApiBaseAddress));
builder.Services.AddScoped<CartService>();
await builder.Build().RunAsync();

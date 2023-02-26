using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using VV.Common.HttpClients;
using VV.Common.Services;
using VV.User.UI;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpClient<VVHttpClient>(client => client.BaseAddress = new Uri("https://localhost:6001/api/"));

await builder.Build().RunAsync();

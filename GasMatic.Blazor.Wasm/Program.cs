using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using GasMatic.Blazor.Localization;
using GasMatic.Blazor.Wasm.Components;
using GasMatic.Blazor.Wasm.IndexedDb.Services;
using GasMatic.Blazor.Wasm.Services;
using GasMatic.Core.Interfaces;
using GasMatic.Core.Services;
using IndexedDB.Blazor;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddScoped<IIndexedDbFactory, IndexedDbFactory>();
builder.Services.AddScoped<IGasVolumeDataSource, GasVolumeIndexedDbDataSource>();

builder.Services.AddScoped<IGasVolumeService, GasVolumeService>();
builder.Services.AddScoped<ICultureService, CultureService>();

builder.Services.AddLocalization();

var host = builder.Build();
var jsRuntime = host.Services.GetRequiredService<IJSRuntime>();
await jsRuntime.SetDefaultCultureAsync();
await host.RunAsync();
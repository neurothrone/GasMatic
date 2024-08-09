using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.EntityFrameworkCore;
using GasMatic.Blazor.Wasm.Components;
using GasMatic.Blazor.Wasm.Data;
using GasMatic.Blazor.Wasm.Extensions;
using GasMatic.Blazor.Wasm.Services;
using GasMatic.Core.Interfaces;
using GasMatic.Core.Services;
using SqliteWasmHelper;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddSqliteWasmDbContextFactory<GasMaticDbContext>(options =>
    options.UseSqlite("Data Source=GasMaticDB.sqlite3"));
builder.Services.AddScoped<IGasVolumeDataSource, GasVolumeLocalDataSource>();
builder.Services.AddScoped<IGasVolumeService, GasVolumeService>();

builder.Services.AddLocalization();

var host = builder.Build();
await host.SetDefaultCulture();
await host.RunAsync();
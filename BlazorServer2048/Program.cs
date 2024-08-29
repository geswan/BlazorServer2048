
using Blazored.Modal;
using BlazorServer2048.Data;
using BlazorServer2048.Services;
using Fluxor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly));
builder.Services.AddSingleton<WeatherForecastService>();
builder.Services.AddScoped<IGameService, GameService>();
builder.Services.AddBlazoredModal();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();

using cwzpgpract.Data;
using cwzpgpract.DB;
using cwzpgpract.Hubs;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<MyDbContext>();

builder.Services.AddServerSideBlazor();

builder.Services.AddSingleton<MainMenuDataService>();

builder.Services.AddScoped<RHub>();

builder.Services.AddScoped<FHub>();

builder.Services.AddScoped<CHub>();

builder.Services.AddSignalR();

//builder.Services.AddHostedService<BGCreateService>();

builder.Services.AddHostedService<BGCreateService>();

builder.Services.AddLocalization();

builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("Postgres"));
});

var app = builder.Build();

app.UseRequestLocalization(new RequestLocalizationOptions()
    .AddSupportedCultures(new[] { "en-US", "es-RU" })
    .AddSupportedUICultures(new[] { "en-US", "es-RU" }));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseWebSockets();

app.UseRouting();

app.UseAuthorization();

app.MapBlazorHub();

app.MapHub<RHub>("/RHub");

app.MapHub<FHub>("/FHub");

app.MapHub<CHub>("/CHub");

app.MapFallbackToPage("/_Host");

using (var scope = app.Services.CreateScope())
{
    var serviceProvider  = scope.ServiceProvider;
    var db = serviceProvider.GetRequiredService<MyDbContext>();

    await db.Database.EnsureCreatedAsync();

    await InitializingDb.Initialize(db);
}

app.Run();

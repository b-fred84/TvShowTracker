using DataAccess.Data;
using DataAccess.DataAccess;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Syncfusion.Blazor;
using ImdbScraper;
using ShowTrackerApp.Services;
using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using ShowTrackerApp.Components;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddSyncfusionBlazor();
builder.Services.AddScoped<ShowData>();
builder.Services.AddScoped<IImdbRatingScraper, ImdbRatingScraper>();

builder.Services.AddHostedService<BackgroundScraperService>();

builder.Services.AddTransient<ILoginStatus, LoginStatus>();

var SyncfusionLicenceKey = builder.Configuration["SyncfusionLicenceKey"];
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(SyncfusionLicenceKey);

builder.Services
    .AddAuth0WebAppAuthentication(options =>
    {
        options.Domain = builder.Configuration["Auth0:Authority"] ?? ""; ;
        options.ClientId = builder.Configuration["Auth0:ClientId"] ?? ""; ;
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


app.Run();

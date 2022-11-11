using CoffeShopServices.Emailer;
using ÑoffeShop;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using FluentValidation;
using ÑoffeShop.Models;
using ÑoffeShop.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization().AddViewLocalization();

builder.Services.AddScoped<IValidator<Person>, PersonValidator>();

builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Logging.AddFile(Path.Combine(Directory.GetCurrentDirectory(), "Logs/logger.txt"));

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
      new CultureInfo("en"),
      new CultureInfo("uk")
    };

    options.DefaultRequestCulture = new RequestCulture("en");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files")),

    RequestPath = new PathString("/pages")
});

var LocOptions = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(LocOptions.Value);

app.UseStaticFiles();

app.UseStaticFiles(new StaticFileOptions() // îáðàáàòûâàåò çàïðîñû ê êàòàëîãó wwwroot/html
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Files")),
    RequestPath = new PathString("/pages")
});
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    var contextAccessor = context.RequestServices.GetService<IHttpContextAccessor>();
    app.Logger.LogInformation($"Ip: {contextAccessor?.HttpContext?.Connection.RemoteIpAddress} Path: {context.Request.Host}{context.Request.Path}{context.Request.QueryString} Time:{DateTime.Now.ToLongTimeString()}");
    await next(context);
});
app.Run();
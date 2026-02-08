using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<BlazingTrails.Client.App>();
builder.Services.AddControllers();
builder.Services.AddDbContext<BlazingTrailsContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext"));
    }
);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetEntryAssembly()!));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddHttpLogging(cfg =>
{
   cfg.LoggingFields = Microsoft.AspNetCore.HttpLogging.HttpLoggingFields.All; 
});
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"images")),
    RequestPath = new Microsoft.AspNetCore.Http.PathString("/images")
});
app.UseRouting();
app.UseHttpLogging();
app.MapBlazorHub();
app.MapControllers();
app.MapFallbackToFile("index.html");
app.Run();
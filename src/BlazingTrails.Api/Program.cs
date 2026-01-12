using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
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
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(Assembly.GetEntryAssembly()!));
builder.Services.AddFluentValidationAutoValidation();
var app = builder.Build();
if(!app.Environment.IsDevelopment()){
    app.UseExceptionHandler();
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
Console.WriteLine(Path.Combine(Directory.GetCurrentDirectory()));
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),@"images")),
    RequestPath = new Microsoft.AspNetCore.Http.PathString("/images")
});
app.UseRouting();
app.MapBlazorHub();
app.MapControllers();
app.MapFallbackToFile("index.html");

Console.WriteLine("Server was started");

app.Run();
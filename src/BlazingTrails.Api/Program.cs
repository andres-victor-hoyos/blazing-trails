using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<BlazingTrails.Client.App>();
builder.Services.AddDbContext<BlazingTrailsContext>(
    options =>
    {
        options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext"));
    }
);
var app = builder.Build();
if(!app.Environment.IsDevelopment()){
    app.UseExceptionHandler();
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseRouting();
app.MapBlazorHub();
app.MapFallbackToFile("index.html");
app.Run();
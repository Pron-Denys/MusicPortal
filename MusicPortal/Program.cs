using Microsoft.EntityFrameworkCore;
using MusicPortal.Models;
using MusicPortal.Repository;
using MusicPortal.Services;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
string connection = builder.Configuration.GetConnectionString("DefaultConnection")!;
builder.Services.AddDbContext<MusicPortalContext>(options => options.UseSqlServer(connection));
builder.Services.AddUserRepository();
builder.Services.AddGenreRepository();
builder.Services.AddMusicRepository();
builder.Services.AddReadLanguage();
builder.Services.AddSession();
var app = builder.Build();

app.UseSession();
app.UseStaticFiles();

app.MapGet("/Download/{fileName}", (string fileName) =>
{
    var pathAudio = Path.Combine(builder.Environment.WebRootPath, "Music", fileName);
    return Results.File(pathAudio, "audio/ogg", fileName);
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Music}/{action=Index}/{id?}");


app.Run();

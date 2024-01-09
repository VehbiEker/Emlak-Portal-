using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using WebApplication1.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UygulamaDbContext>(options=>
                 options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<UygulamaDbContext>().AddDefaultTokenProviders();
builder.Services.AddRazorPages(); //identity kismi icin

//_emlakturuRepostory nesnesinin olusutulmasini sagladim.
builder.Services.AddScoped<IemlakturuRepostory, emlakturuRepostory>();

//
builder.Services.AddScoped<IEmlakRepostory, EmlakRepostory>();

builder.Services.AddScoped<IEmailSender, EmailSender>();//emailsender serveri olmadigindan dolayi hata vermemesi icin yaptim


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

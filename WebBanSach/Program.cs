using Microsoft.EntityFrameworkCore;
using WebBanSach.DataAccess;
using WebBanSach.DataAccess.Repository;
using WebBanSach.DataAccess.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();//cau hinh thay dbcontext bang repository
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
// 1) Route chung cho tất cả Areas
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// 2) Đặt Area mặc định là Customer cho root "/"
app.MapAreaControllerRoute(
    name: "customer_default",
    areaName: "Customer",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// (tuỳ chọn) Route MVC không‑area nếu bạn còn controller ngoài Areas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
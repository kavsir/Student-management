using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Student_Management.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Đăng ký dịch vụ MVC
builder.Services.AddControllersWithViews();

// 2. Đăng ký DbContext
builder.Services.AddDbContext<QuanLyHocSinhContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("QuanLyHocSinh")));

// 3. Đăng ký Session
builder.Services.AddSession();

// 4. Đăng ký Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });

// 5. Đăng ký Authorization (phân quyền)
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdmin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("RequireGiaoVien", policy => policy.RequireRole("GiaoVien"));
    options.AddPolicy("RequireHocSinh", policy => policy.RequireRole("HocSinh"));
});

// ✅ Build sau khi Add xong mọi dịch vụ
var app = builder.Build();

// 6. Cấu hình middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
builder.Services.AddSession();
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // middleware session
app.UseAuthentication(); // middleware xác thực
app.UseAuthorization();  // middleware phân quyền

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

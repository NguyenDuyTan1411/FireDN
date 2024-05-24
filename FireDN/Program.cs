using FireDN.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(serverOptions =>
{
    serverOptions.Listen(System.Net.IPAddress.Loopback, 5000); // Kết nối đến localhost qua cổng 5000
});

// Cấu hình DbContext
builder.Services.AddDbContext<FireKLContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Defaultconnect")));

// Cấu hình xác thực cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Trang đăng nhập
        options.AccessDeniedPath = "/Account/AccessDenied"; // Trang truy cập bị từ chối (nếu có)
    });

// Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

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

// Sử dụng xác thực và ủy quyền
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.Run();

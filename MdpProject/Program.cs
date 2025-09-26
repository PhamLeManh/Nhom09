using Microsoft.EntityFrameworkCore;
using MdpProject.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Lấy connection string từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Đăng ký DbContext
builder.Services.AddDbContext<Project2Context>(options =>
    options.UseSqlServer(connectionString));

// Nếu cần session (giỏ hàng, đăng nhập)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Đăng ký IHttpContextAccessor để có thể dùng HttpContext trong Razor
builder.Services.AddHttpContextAccessor();

// Nếu có dùng SignalR thì để, không thì bỏ
// builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/MDPHome/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Kích hoạt session
app.UseSession();

// Nếu có dùng Authentication thì bật ở đây
// app.UseAuthentication();

app.UseAuthorization();

// Route mặc định
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MDPHome}/{action=Index}/{id?}");

// Nếu có dùng SignalR thì map hub ở đây
// app.MapHub<ChatHub>("/chathub");

app.Run();

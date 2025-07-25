using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.WebUI.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddCookie(JwtBearerDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Login/Index";
    options.LogoutPath = "/Login/Logout";
    options.AccessDeniedPath = "/Login/AccessDenied";
    options.Cookie.HttpOnly = true; // HttpOnly �zelli�i, �erezlerin JavaScript taraf�ndan eri�ilmesini engeller
    options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; // �erezlerin g�venli ba�lant�larda kullan�lmas�n� sa�lar
    options.Cookie.SameSite = SameSiteMode.Strict; // �erezlerin yaln�zca ayn� site i�indeki isteklerde kullan�lmas�n� sa�lar
    options.Cookie.Name = "MultiShopJwtCookie";
});

builder.Services.AddHttpContextAccessor(); // HttpContext eri�imini sa�lar
builder.Services.AddScoped<ILoginService, LoginService>();

// Add services to the container.
builder.Services.AddHttpClient();
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();

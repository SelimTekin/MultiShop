using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Comment.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceComment"; // bu key'e sahip kullanýcý config dosyasýndaki bu key'e karþýlýk gelen iþlemleri yapabilecek
    opt.RequireHttpsMetadata = false; // http yaptýðýmýz için false yaptýk (appsetting.json dosyasýnda)
});

// Add services to the container.
builder.Services.AddDbContext<CommentContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // Authentication iþlemlerini yapabilmesi için bu middleware'i kullanmasý gerekiyor
app.UseAuthorization();

app.MapControllers();

app.Run();

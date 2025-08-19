using Microsoft.AspNetCore.Authentication.JwtBearer;
using MultiShop.Comment.Context;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.Authority = builder.Configuration["IdentityServerUrl"];
    opt.Audience = "ResourceComment"; // bu key'e sahip kullan�c� config dosyas�ndaki bu key'e kar��l�k gelen i�lemleri yapabilecek
    opt.RequireHttpsMetadata = false; // http yapt���m�z i�in false yapt�k (appsetting.json dosyas�nda)
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

app.UseAuthentication(); // Authentication i�lemlerini yapabilmesi i�in bu middleware'i kullanmas� gerekiyor
app.UseAuthorization();

app.MapControllers();

app.Run();

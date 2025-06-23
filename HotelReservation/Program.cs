using HotelReservation.Core.Entities;
using HotelReservation.Core.IServices;
using HotelReservation.Core.Services;
using HotelReservation.Infrastructure;
using HotelReservation.Infrastructure.Data;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.InfrastureConfiguration(builder.Configuration);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
               .AddEntityFrameworkStores<AppDbContext>()
               .AddDefaultTokenProviders();

// JWT Configuration
// Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, //????? ?? ?????? ??? ?? ???? ????? (???? ??? ?????? ???).
        ValidateAudience = true,//????? ?? ?????? ???? ??????? ????? (?? ??? ????).
        ValidAudience = builder.Configuration["JWT:ValidAudience"], //?? ?????? ???? ?????? ????? ????? ???? ?? ?????? ????? ????? ??????? ??
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],//?? ??? ?????? ???? ???? ?????? (?? ????? ?? ??????? ?????).
        ClockSkew = TimeSpan.Zero,//???? ?????? ?? ??? ?? ????? ???? ????? (expiry ???? ????).
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

builder.Services.AddScoped<ITokenService, TokenService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
await DbSeeder.SeedDataAsync(app);
app.UseAuthorization();

app.MapControllers();

app.Run();

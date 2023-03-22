using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ReactNetProyect.BackEnd.API.ApiBehavior;
using ReactNetProyect.BackEnd.API.Filters;
using ReactNetProyect.BackEnd.Data;
using ReactNetProyect.BackEnd.Data.Repositories;
using ReactNetProyect.BackEnd.Service;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<ReactNetProyectContext>(builder.Configuration.GetConnectionString("ReactNetProyectContextDB"),
    b => b.MigrationsAssembly("ReactNetProyect.BackEnd.Data"));

var frontendURL = builder.Configuration.GetValue<string>("frontend_url");
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "Cores", builder =>
    {
        
        builder.AllowAnyHeader();
        builder.WithOrigins(frontendURL);
        builder.AllowAnyMethod();
        builder.WithExposedHeaders(new string[] { "cantidadTotalRegistros" });
    });
});
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ReactNetProyectContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opciones =>
                opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWTKey"))),
                    ClockSkew = TimeSpan.Zero
                });
builder.Services.AddAuthorization(opciones =>
{
    opciones.AddPolicy("EsAdmin", policy => policy.RequireClaim("role", "admin"));
});

builder.Services.AddScoped<ReceiptRepository>();
builder.Services.AddScoped<CurrencyRepository>();
builder.Services.AddScoped<IReceiptService, ReceiptService>(); 
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ExceptionFilter));
    options.Filters.Add(typeof(ParseBadRequests));
}).ConfigureApiBehaviorOptions(BehaviorBadRequests.Parse);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("Cores");
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

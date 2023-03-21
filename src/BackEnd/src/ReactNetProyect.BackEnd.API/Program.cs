using Microsoft.Extensions.Configuration;
using ReactNetProyect.BackEnd.API.ApiBehavior;
using ReactNetProyect.BackEnd.API.Filters;
using ReactNetProyect.BackEnd.Data;
using ReactNetProyect.BackEnd.Data.Repositories;
using ReactNetProyect.BackEnd.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSqlServer<ReactNetProyectContext>(builder.Configuration.GetConnectionString("ReactNetProyectContextDB"),
    b => b.MigrationsAssembly("ReactNetProyect.BackEnd.Data"));

var frontendURL = builder.Configuration.GetConnectionString("frontend_url");
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


builder.Services.AddScoped<ReceiptRepository>();
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

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();

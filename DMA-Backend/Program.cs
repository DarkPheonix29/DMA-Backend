using DMA_BLL.Interfaces;
using DMA_DAL;
using DMA_DAL.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add MySQL connection string
var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");

// Configure the DbContext to use MySQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add scoped services for your repositories
builder.Services.AddScoped<IDishRepos, DishRepos>();

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

app.UseAuthorization();

app.MapControllers();

app.Run();

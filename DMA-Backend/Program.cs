using DMA_BLL;
using DMA_BLL.Interfaces;
using DMA_DAL;
using DMA_DAL.Repos;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add MySQL connection string
var connectionString = builder.Configuration.GetConnectionString("MySQLConnection");

// Configure the DbContext to use MySQL with Pomelo
builder.Services.AddDbContext<ApplicationDbContext>(options =>
	options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Register repository interfaces and implementations
builder.Services.AddScoped<IDishRepos, DishRepos>();
builder.Services.AddScoped<ITableRepos, TableRepos>();

// Register BLL services
builder.Services.AddScoped<TableServices>();
builder.Services.AddSingleton<QrCodeService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

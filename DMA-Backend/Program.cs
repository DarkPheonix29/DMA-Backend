using DMA_BLL;
using DMA_BLL.Interfaces;
using DMA_DAL;
using DMA_DAL.Repos;
using DMA_Backend.Hubs;
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
builder.Services.AddScoped<IOrderRepos, OrderRepos>();
builder.Services.AddScoped<IAllergenRepos, AllergenRepos>();
builder.Services.AddScoped<ICategoryRepos, CategoryRepos>();

builder.Services.AddSignalR();

// Register BLL services
builder.Services.AddScoped<TableServices>();
builder.Services.AddSingleton<QrCodeService>();

// Add controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("https://localhost:50623")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.MapHub<OrderHub>("/orderhub");
app.Run();
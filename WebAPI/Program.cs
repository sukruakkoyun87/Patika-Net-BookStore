using WebAPI.DBOperations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WebAPI.Middlewares;
using WebAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BookStoreDbContext>(options =>
    options.UseInMemoryDatabase( databaseName: "BookStoreDbContext"));

builder.Services.AddSingleton<ILoggerService, DatabaseLogger>();
builder.Services.AddScoped<IBookStoreDbContext>(provider => provider.GetService<BookStoreDbContext>());


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());



var app = builder.Build();
using(var scope=app.Services.CreateScope()){
    var dbContext = scope.ServiceProvider;
    DataGenarator.Initialize(dbContext);
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCustomExceptionMiddleware();

app.MapControllers();

app.Run();

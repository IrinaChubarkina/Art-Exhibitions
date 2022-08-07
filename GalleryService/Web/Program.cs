using RabbitMq;
using Storage.Sql;
using Storage.Sql.EntityFramework;
using Web;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration.Get<AppSettings>();
builder.Services.AddDbServices(config.SqlServer);
builder.Services.AddRabbitMq(config.RabbitMq);
builder.Services.AddServices(config);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using var serviceScope = ((IApplicationBuilder)app).ApplicationServices.CreateScope();
DbInitializer.SeedData(serviceScope.ServiceProvider.GetService<AppDbContext>());

app.Run();

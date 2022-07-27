using Storage.Sql;
using Web;
using Web.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbServices();
builder.Services.AddServices(builder.Configuration);

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

PrepDb.PrepareExerciseData(app);

app.Run();

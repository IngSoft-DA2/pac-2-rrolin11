using APIServiceFactory;
using BackApi.Interfaces;
using BackApi.Services;
using WebService.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IReflectionService, ReflectionService>();

builder.Services.AddControllers(o => o.Filters.Add<ExceptionFilter>());
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

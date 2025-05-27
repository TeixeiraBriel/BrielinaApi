using InjecaoDependencia;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
_ = new Bootstrapper(services, configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (app.Environment.IsDevelopment() && app.Configuration["ASPNETCORE_HTTPS_PORT"] != null)
{
    app.UseHttpsRedirection();
}

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "API Brielina rodando!");
app.Run();

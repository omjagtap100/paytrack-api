using Microsoft.OpenApi.Models;
using Microsoft.Data.SqlClient; 
using Dapper;

var builder = WebApplication.CreateBuilder(args);

// Register DapperConnection
builder.Services.AddSingleton<DapperConnection>();

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayTrack API", Version = "v1" });
});

var app = builder.Build();

// Always enable Swagger (even in production)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayTrack API V1");
    c.RoutePrefix = string.Empty; // Loads Swagger at http://localhost:5000/
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Dapper test route
app.MapGet("/test-dapper", async (DapperConnection context) =>
{
    using var connection = context.CreateConnection();
    var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
    return Results.Ok(new { DapperTest = result });
});

app.Run();

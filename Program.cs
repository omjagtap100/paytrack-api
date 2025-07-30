using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register DapperConnection
builder.Services.AddSingleton<DapperConnection>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        System.Diagnostics.Debug.WriteLine("API Request Receied");
//        options.Authority = "http://localhost:8080/realms/payroll-realm";
//        options.Audience = "paytrack-client";
//        options.RequireHttpsMetadata = false;

//        options.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateAudience = false, // optional
//        };

//        options.Events = new JwtBearerEvents
//        {
//            OnAuthenticationFailed = context =>
//            {
//                System.Diagnostics.Debug.WriteLine("Authentication failed:");
//                System.Diagnostics.Debug.WriteLine(context.Exception.ToString());
//                return Task.CompletedTask;
//            },
//            OnTokenValidated = context =>
//            {
//                System.Diagnostics.Debug.WriteLine("Token validated successfully");
//                return Task.CompletedTask;
//            },
//            OnChallenge = context =>
//            {
//                System.Diagnostics.Debug.WriteLine("Challenge triggered");
//                System.Diagnostics.Debug.WriteLine(context.Request.Headers);
//                System.Diagnostics.Debug.WriteLine($"Error: {context.Error}");
//                System.Diagnostics.Debug.WriteLine($"Description: {context.ErrorDescription}");

//                // Prevent default behavior
//                context.HandleResponse();

//                context.Response.StatusCode = 401;
//                context.Response.ContentType = "application/json";

//                var errorResponse = new
//                {
//                    error = context.Error ?? "invalid_token",
//                    error_description = context.ErrorDescription ?? "Token validation failed or missing"
//                };

//                return context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResponse));
//            }
//        };
//    });

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // Authority: Keycloak Realm issuer URL
        options.Authority = "http://localhost:8080/realms/payroll-realm";
        // Audience: our API's client ID in Keycloak
        options.Audience = "account";
        // Disable HTTPS metadata requirement for dev (use true in production)
        options.RequireHttpsMetadata = false;
        // Token validation parameters
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
            // (Optional) If needed, set RoleClaimType to "roles" or other to map roles correctly
            // RoleClaimType = "roles"
        };
    });
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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

//app.MapGet("/secure", [Authorize] () => "This is a secure endpoint!");
//app.MapGet("/unsecure", () => "This is a unsecure endpoint!");

// Dapper test route
app.MapGet("/test-dapper", async (DapperConnection context) =>
{
    using var connection = context.CreateConnection();
    var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
    return Results.Ok(new { DapperTest = result });
});

app.Run();

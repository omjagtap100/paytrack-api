using Dapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using paytrack_api.Middlewares;
using paytrack_api.Repository;
using paytrack_api.Repository.Interfaces;
using paytrack_api.Services;
using paytrack_api.Services.Interfaces;
//using paytrack_api.Middlewares;
using System.Globalization;





var builder = WebApplication.CreateBuilder(args);

// Register DapperConnection
builder.Services.AddScoped<DapperConnection>();

builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<ISalariesRepository, SalariesRepository>();

builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<ISalariesService, SalariesService>();




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


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "http://localhost:8080/realms/payroll-realm";
        options.Audience = "account";
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = false,
       
        };
    });
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "PayTrack API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "PayTrack API V1");
    c.RoutePrefix = string.Empty;
});



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseRequestLocale();
builder.Logging.AddConsole(); 
builder.Logging.AddDebug();


app.MapGet("/test-dapper", async (DapperConnection context) =>
{
    using var connection = context.CreateConnection();
    var result = await connection.ExecuteScalarAsync<int>("SELECT 1");
    return Results.Ok(new { DapperTest = result });
});

app.Run();

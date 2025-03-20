using AspNetCoreRateLimit;
using MediatR;
using Study_Project.Application;
using Study_Project.Core.Interfaces;
using Study_Project.Extensions;
using Study_Project.Infrastructure.Persistence;
using Study_Project.Infrastructure.Services;
using Study_Project.Services;
using static System.Net.Mime.MediaTypeNames;

var builder = WebApplication.CreateBuilder(args);

// ✅ Serilog Logging
builder.AddSerilogLogging();

// ✅ Identity + JWT Configuration
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

// ✅ MediatR Registration (Points to Application Layer for Handlers)
builder.Services.AddMediatR(typeof(Application.AssemblyReference).Assembly);

// ✅ Replace old EmployeeService DI
// No longer needed: builder.Services.AddTransient<IEmployeeService, EmployeeService>();

// ✅ Register AuthService if still needed
builder.Services.AddScoped<IAuthService, AuthService>();

// ✅ Custom Policies
builder.Services.AddCustomAuthorization();

// ✅ Swagger, CORS, RateLimiting, Controllers
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddRateLimiting(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// ✅ Pipeline Setup
app.UseHttpsRedirection();
app.UseGlobalExceptionMiddleware();
app.UseCors("AllowSpecificOrigins");
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerDocumentation();

app.MapControllers();
app.Run();

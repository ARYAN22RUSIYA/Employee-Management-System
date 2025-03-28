using Application.DependencyInjection;
using AspNetCoreRateLimit;
using Study_Project.Extensions;

var builder = WebApplication.CreateBuilder(args);

// ✅ Serilog Logging
builder.AddSerilogLogging();

// ✅ Identity + JWT Configuration
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

// ✅ Register Application Layer Services (CQRS - Commands & Queries)
builder.Services.AddApplicationServices();


// ✅ Custom Policies
builder.Services.AddCustomAuthorization();

// ✅ Swagger, CORS, RateLimiting, Controllers
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddRateLimiting(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// ✅ Correct Middleware Order
app.UseHttpsRedirection();
app.UseGlobalExceptionMiddleware();
app.UseCors("AllowSpecificOrigins");
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerDocumentation();

// ✅ Correct Mapping for Minimal API
app.MapControllers();

app.Run();

using AspNetCoreRateLimit;
using Study_Project.Extensions;
using Study_Project.Interfaces;
using Study_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Logging
builder.AddSerilogLogging();

// Add Identity & JWT
builder.Services.AddIdentityConfiguration(builder.Configuration)
                .AddJwtAuthentication(builder.Configuration);

// Register Services
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAuthService, AuthService>();


// Add core services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Authorization Policies
builder.Services.AddCustomAuthorization();

// Swagger, CORS, Rate Limiting
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddRateLimiting(builder.Configuration);

var app = builder.Build();

// Middleware Pipeline
app.UseHttpsRedirection();
app.UseGlobalExceptionMiddleware();
app.UseCors("AllowSpecificOrigins");
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerDocumentation();

app.MapControllers();
app.Run();

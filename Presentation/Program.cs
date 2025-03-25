using Application.Features.Employee.Queries.GetEmployeeList;
using AspNetCoreRateLimit;
using Study_Project.Extensions;
using Study_Project.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Serilog Logging
builder.AddSerilogLogging();

// ✅ Identity + JWT Configuration
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);

// ✅ MediatR Registration - Register Application Layer (Handles all Queries/Commands)
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(GetEmployeeListHandler).Assembly));

// ✅ AuthService - Only if still used for Registration/Login/Role
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

// ✅ Correct Middleware Order
app.UseHttpsRedirection();

app.UseRouting();  // ❗ REQUIRED to enable routing

app.UseGlobalExceptionMiddleware();
app.UseCors("AllowSpecificOrigins");
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();

app.UseSwaggerDocumentation();

// ✅ Ensure controller mapping is inside UseEndpoints or MapControllers
app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();

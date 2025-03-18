using Study_Project.Context;
using Study_Project.Interfaces;
using Study_Project.Services;
using Study_Project.Extensions;
using Microsoft.EntityFrameworkCore;
using JWT_Authentication_Authorization.Services;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

builder.AddSerilogLogging();

builder.Services.AddDbContext<JwtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCorsPolicy(builder.Configuration); 
builder.Services.AddRateLimiting(builder.Configuration); 

var app = builder.Build();

app.UseHttpsRedirection();

app.UseGlobalExceptionMiddleware();

app.UseCors("AllowSpecificOrigins"); 
app.UseIpRateLimiting(); 

app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerDocumentation();

app.MapControllers();
app.Run();

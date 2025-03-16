using Study_Project.Context;
using Study_Project.Interfaces;
using Study_Project.Services;
using Study_Project.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using JWT_Authentication_Authorization.Services;

var builder = WebApplication.CreateBuilder(args);

// ✅ Use Extension to Configure Logging
builder.AddSerilogLogging();

// ✅ Add Services to the Container
builder.Services.AddDbContext<JwtContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// ✅ Use Extension for Authentication & Swagger
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

var app = builder.Build();

// ✅ Use Middleware for Authentication & Swagger
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerDocumentation();

app.MapControllers();
app.Run();

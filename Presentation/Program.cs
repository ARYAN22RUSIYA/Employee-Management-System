using Application.DependencyInjection;
using AspNetCoreRateLimit;
using Core.Interface;
using Hangfire;
using Infrastructure.Extensions;
using Infrastructure.DependencyInjection;
using Study_Project.Extensions;

var builder = WebApplication.CreateBuilder(args);


builder.AddSerilogLogging();
builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddFluentValidationPipeline();
builder.Services.AddCustomAuthorization();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddCorsPolicy(builder.Configuration);
builder.Services.AddRateLimiting(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapperServices();
builder.Services.AddHangfire(x => x.UseInMemoryStorage());
builder.Services.AddHangfireServer();
// Register infrastructure services (repositories, email, background jobs)
builder.Services.AddInfrastructureServices();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseGlobalExceptionMiddleware();
app.UseCors("AllowSpecificOrigins");
app.UseIpRateLimiting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard();


app.UseSwaggerDocumentation();

app.MapControllers();

app.Run();

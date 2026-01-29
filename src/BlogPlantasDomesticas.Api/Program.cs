using BlogPlantasDomesticas.Api.Configurations;
using BlogPlantasDomesticas.Api.Middleware;
using BlogPlantasDomesticas.Api.Services;
using BlogPlantasDomesticas.Api.Shared;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host.AddSerilogConfig();
builder.Services.AddDependencyInjectionConfig();
builder.Services.AddDbContextConfig(builder.Configuration);
builder.Services.AddJwtConfig(builder.Configuration);
builder.Services.AddSeedConfig(builder.Configuration);

builder.Services.AddHealthChecks();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();
    await seedService.SeedAdminAsync();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsProduction())
{
    app.UseHttpsRedirection();
}

app.UseMiddleware<ErrorHandleMiddleware>();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.MapHealthChecks("/health");

app.Run();
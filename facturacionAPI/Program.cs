using System.Reflection;
using AspNetCoreRateLimit;
using facturacionAPI.Extensions;
using Microsoft.EntityFrameworkCore;
using Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



//-Add Authentication
builder.Services.AddAuthentication();


builder.Services.AddDbContext<FacturacionContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("ConexHome");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Configurations
builder.Services.AddAplicacionServices();
builder.Services.ConfigureCors();
builder.Services.ConfigurationRatelimiting();
builder.Services.ConfigureApiVersioning();
builder.Services.AddAutoMapper(Assembly.GetEntryAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/* using(var scope = app.Services.CreateScope()){
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try{
        
        var context = services.GetRequiredService<FacturacionContext>();
        await context.Database.MigrateAsync();

    }catch (Exception ex){
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex,"Ocurrio un error durante la migracion");
    }
} */

app.UseCors("CorsPolicy");
app.UseApiVersioning();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseIpRateLimiting();

app.MapControllers();

app.Run();
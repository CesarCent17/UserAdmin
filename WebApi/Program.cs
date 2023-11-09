using WebApi.Dependencies;
using dotenv.net;
using WebApi.Utils;

var builder = WebApplication.CreateBuilder(args);

ServicesDependency.AddServices(builder.Services, builder.Configuration);

MigrateDatabaseUtil.MigrateDatabase(builder.Services);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        //builder.WithOrigins("http://localhost:4200")
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors("AllowOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();

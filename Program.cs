var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddSingleton<ICarService, CarService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

app.UseCors("AllowAll");
app.UseAuthorization();

app.MapControllers();

app.Run();

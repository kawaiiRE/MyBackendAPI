var builder = WebApplication.CreateBuilder(args);

// Register the ICarService with its implementation
// builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddSingleton<ICarService, CarService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // Replace with your frontend URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

// Apply CORS policy
app.UseCors("AllowAll");


// Configure the HTTP request pipeline.
app.UseAuthorization();

app.MapControllers();  // Ensure routing is set up

app.Run();

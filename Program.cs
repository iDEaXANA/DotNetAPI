var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors((options) =>
    {
        // Development
        options.AddPolicy("DevCors", (corsBuilder) =>
        {
            corsBuilder.WithOrigins("http://localhost:4200", "http://localhost:3000", "http://localhost:8000") //Angular, React, Vue 
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
        // Deployment
        options.AddPolicy("ProdCors", (corsBuilder) =>
        {
            corsBuilder.WithOrigins("https://myProductionSite.com") //Front-End.
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("DevCors");
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseCors("ProdCors");
    app.UseHttpsRedirection(); // More secure method but no need whilst dev
}
app.MapControllers();

// app.MapGet("/weatherforecast", () =>
// {
// })
// .WithName("GetWeatherForecast")
// .WithOpenApi();

app.Run();


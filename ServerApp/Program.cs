using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Options;
using ServerApp.Repository;
using ServerApp;


MongoDBmappings.Register();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

var dbSettingsSection = builder.Configuration.GetSection("DatabaseSettings");
builder.Services.Configure<DatabaseSettings>(options =>
{
    options.ConnectionString = Environment.GetEnvironmentVariable("ConnectionString")
                               ?? dbSettingsSection.GetValue<string>("ConnectionString");
    options.DatabaseName = dbSettingsSection.GetValue<string>("DatabaseName");
});

// Register singleton using the configured settings
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<DatabaseSettings>>().Value);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IBookingRepository, BookingMongoDB>();
builder.Services.AddSingleton<IWindowRepository, WindowMongoDB>();
builder.Services.AddSingleton<IUserRepository, UserMongoDB>();
builder.Services.AddSingleton<IWorkTaskRepository, WorkTaskMongoDB>();
builder.Services.AddSingleton<IWindowLocationRepository, WindowLocationMongoDB>();
builder.Services.AddSingleton<IWindowTypeRepository, WindowTypeMongoDB>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy => policy.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowBlazor");

app.MapControllers();
Console.WriteLine($"ENVIRONMENT: {app.Environment.EnvironmentName}");

app.Run();
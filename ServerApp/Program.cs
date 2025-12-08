using Microsoft.AspNetCore.Identity.Data;
using ServerApp.Repository;
using ServerApp;


MongoDBmappings.Register();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton<IBookingRepository, BookingMongoDB>();
builder.Services.AddSingleton<ICustomerRepository, CustomerMongoDB>();
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


app.Run();
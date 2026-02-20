using Microsoft.EntityFrameworkCore;
using PublisherData;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PubContext>();

// Lägger till controllers och ignorerar cykliska referenser i JSON
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = 
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Aktiverar Swagger för att testa API:et i webbläsaren
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
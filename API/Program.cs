using MediatR;
using Microsoft.EntityFrameworkCore;
using PagosCQRSDDDEventSourcing.Application.Commands;
using PagosCQRSDDDEventSourcing.Infrastructure.Mongo;
using PagosCQRSDDDEventSourcing.Infrastructure.Kafka;
using PagosCQRSDDDEventSourcing.Infrastructure.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Servicios
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Pagos API CQRS-DDD-EventSourcing", Version = "v1" });
});


builder.Services.AddDbContext<PagosDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IPagoRepository, PagoRepository>();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));
builder.Services.AddSingleton<IMongoPagoRepository, MongoPagoRepository>();

builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection("KafkaSettings"));
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();
builder.Services.AddScoped<KafkaProducer>();

builder.Services.AddMediatR(typeof(CreatePagoCommandHandler).Assembly);

builder.Services.AddScoped<CreatePagoCommandHandler>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pagos API CQRS-DDD-EventSourcing v1"));
}

app.MapControllers();
app.Run();

using Application.Common.Interfaces;
using Application.Features.Events.Consumers;
using Domain.Events;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IEventRepository, EventRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Application.Features.Events.Commands.CreateEvent.CreateEventCommand).Assembly));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(x =>
{
    x.UsingInMemory((context, cfg) => cfg.ConfigureEndpoints(context));

    x.AddRider(rider =>
    {
        rider.AddConsumer<TicketPurchaseConsumer>();
        
        rider.AddProducer<TicketPurchaseRequest>("ticket-sales");

        rider.UsingKafka((context, k) =>
        {
            k.Host("localhost:9092");
            
            k.TopicEndpoint<TicketPurchaseRequest>("ticket-sales", "ticket-group", e =>
            {
                e.ConfigureConsumer<TicketPurchaseConsumer>(context);
            });
        });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
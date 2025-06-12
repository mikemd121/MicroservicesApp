using MediatR;
using Microsoft.EntityFrameworkCore;
using PositionsService;
using PositionsService.Application.Commands;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PositionDbContext>(options =>
    options.UseInMemoryDatabase("PositionDb"));
// MediatR
builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CreatePositionCommandHandler>());
// Event Dispatcher
builder.Services.AddSingleton<IEventDispatcher, InMemoryEventDispatcher>();

// Register Notification Handler
builder.Services.AddScoped<INotificationHandler<RateChangedIntegrationEvent>, RateChangedEventHandler>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

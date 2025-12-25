using OfficeOpenXml;
using Template.Application;
using Template.Application;
using Template.Application.Interfaces;
using Template.Infrastructure.Messaging.Rabbit;
using Template.Infrastructure.Persistence;
using Template.SignalR;
using Template.Worker.Outbox;
using Template.Worker.Workers;

var builder = Host.CreateApplicationBuilder(args);

//// 🧠 Application (Use cases)
//builder.Services.AddApplication();

// 🏗 Infrastructure (reuse – not rebuild)
builder.Services.AddInfrastructurePersistence(builder.Configuration);
builder.Services.AddInfrastructureRabbitMq(builder.Configuration);
builder.Services.AddSignalRModule(builder.Configuration);

// 🧵 Workers
builder.Services.AddHostedService<FileProcessorWorker>();
builder.Services.AddHostedService<OutboxDispatcherWorker>();

// handler های واقعی
builder.Services.AddScoped<IOutboxEventHandler, SignalROutboxEventHandler>();

// فقط یک composite
builder.Services.AddScoped<CompositeOutboxEventHandler>();

// Dispatcher از composite استفاده می‌کند
builder.Services.AddScoped<IOutboxEventDispatcher>(sp => new OutboxEventDispatcher(sp.GetRequiredService<CompositeOutboxEventHandler>()));

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var host = builder.Build();
host.Run();

using Template.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<OutboxProcessorWorker>();

var host = builder.Build();
host.Run();

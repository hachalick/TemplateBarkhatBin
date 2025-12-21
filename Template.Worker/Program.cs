using OfficeOpenXml;
using Template.Worker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();
builder.Services.AddHostedService<FileProcessorWorker>();
builder.Services.AddHostedService<OutboxDispatcherWorker>();

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

var host = builder.Build();
host.Run();

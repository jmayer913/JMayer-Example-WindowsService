using JMayer.Example.WindowsService;
using JMayer.Example.WindowsService.BSM;
using JMayer.Net.TcpIp;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Example BSM Service";
});

builder.Services.AddSingleton<BSMParser>();
builder.Services.AddSingleton<TcpIpServer>();
builder.Services.AddHostedService<BSMServerConnectionWorker>();
builder.Services.AddHostedService<BSMServerOutputWorker>();
builder.Services.AddHostedService<BSMClientWorker>();


var host = builder.Build();
host.Run();

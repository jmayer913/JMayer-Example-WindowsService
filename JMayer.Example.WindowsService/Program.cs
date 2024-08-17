using JMayer.Example.WindowsService;
using JMayer.Example.WindowsService.BSM;
using JMayer.Net;
using JMayer.Net.TcpIp;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddWindowsService(options =>
{
    options.ServiceName = "Example BSM Service";
});

builder.Services.AddSingleton<BSMGenerator>();
builder.Services.AddSingleton<IClient>(new TcpIpClient(new BSMParser()));
builder.Services.AddSingleton<IServer>(new TcpIpServer(new BSMParser()) { ConnectionStaleMode = ConnectionStaleMode.LastSent, ConnectionTimeout = 60 });
builder.Services.AddHostedService<BSMServerConnectionWorker>();
builder.Services.AddHostedService<BSMServerOutputWorker>();
builder.Services.AddHostedService<BSMClientWorker>();


var host = builder.Build();
host.Run();

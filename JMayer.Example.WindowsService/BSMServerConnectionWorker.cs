using JMayer.Net;

namespace JMayer.Example.WindowsService;

/// <summary>
/// The class manages starting/stopping the server & receiving remote connections from the clients.
/// </summary>
/// <remarks>
/// The server code is split between two workers because you want to accept client connections
/// as frequently as possible; on a real server, multiple client connections may be queued to be
/// accepted but if the worker accepts a connection, does other stuff and then sleeps for a second
/// or more, the queued clients are missing BSMs.
/// </remarks>
internal class BSMServerConnectionWorker : BackgroundService
{
    /// <summary>
    /// Used to log activity for the service.
    /// </summary>
    private readonly ILogger<BSMServerConnectionWorker> _logger;

    /// <summary>
    /// Used to manage TCP/IP communication.
    /// </summary>
    private readonly IServer _server;

    /// <summary>
    /// The port to monitor.
    /// </summary>
    public const int Port = 55555;

    /// <summary>
    /// The dependency injection constructor.
    /// </summary>
    /// <param name="logger">Used to log activity for the service.</param>
    /// <param name="server">Used to manage TCP/IP communication.</param>
    public BSMServerConnectionWorker(ILogger<BSMServerConnectionWorker> logger, IServer server)
    {
        _logger = logger;
        _server = server;
    }

    /// <summary>
    /// The method handles starting the server & receiving remote connections from the clients.
    /// </summary>
    /// <param name="stoppingToken">Used to cancel the task when the service stops.</param>
    /// <returns>A Task object for the async.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            //Start the server if not ready.
            if (!_server.IsReady)
            {
                try
                {
                    _server.Start(Port);
                    _logger.LogInformation("The BSM server is listen on {Port} port.", Port);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "The BSM server failed to listen to the {Port} port", Port);
                }
            }
            //Accept the client connections.
            else
            {
                try
                {
                    Guid id = await _server.AcceptIncomingConnectionAsync(stoppingToken);

                    if (id != Guid.Empty)
                    {
                        _logger.LogInformation("The BSM server accepted a remote client connection.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "The BSM server failed to accept a remote client connection.");
                }
            }

            await Task.Delay(10, stoppingToken);
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This is overriden so the server can stop listening on the port.
    /// </remarks>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_server.IsReady)
        {
            try
            {
                _server.Stop();
                _logger.LogInformation("The BSM server has stopped.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The BSM server failed to stop; the service will still stop.");
            }
        }

        await base.StopAsync(cancellationToken);
    }
}

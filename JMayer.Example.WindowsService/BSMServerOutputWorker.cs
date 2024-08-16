using JMayer.Example.WindowsService.BSM;
using JMayer.Net;

namespace JMayer.Example.WindowsService;

/// <summary>
/// The class manages stale connections and sending the BSMs to the clients.
/// </summary>
internal class BSMServerOutputWorker : BackgroundService
{
    /// <summary>
    /// Used to log activity for the service.
    /// </summary>
    private readonly ILogger<BSMServerOutputWorker> _logger;

    /// <summary>
    /// Used to manage TCP/IP communication.
    /// </summary>
    private readonly IServer _server;

    /// <summary>
    /// The dependency injection constructor.
    /// </summary>
    /// <param name="logger">Used to log activity for the service.</param>
    /// <param name="server">Used to manage TCP/IP communication.</param>
    public BSMServerOutputWorker(ILogger<BSMServerOutputWorker> logger, IServer server)
    {
        _logger = logger;
        _server = server;
    }

    /// <summary>
    /// The class manages client connections and sending BSMs to the clients.
    /// </summary>
    /// <param name="stoppingToken">Used to cancel the task when the service stops.</param>
    /// <returns>A Task object for the async.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_server.IsReady)
            { 
                //Manage stale remote clients.
                List<Guid> connectionIds = _server.GetStaleRemoteConnections();

                if (connectionIds.Count > 0)
                {
                    _logger.LogInformation("The BSM server detected stale remote clients; will attempt to disconnect.");

                    foreach (Guid connectionId in connectionIds)
                    {
                        try
                        {
                            _server.Disconnect(connectionId);
                        }
                        catch { }
                    }
                }

                //Generate a BSM & sends it to the remote clients.
                try
                {
                    BSM.BSM bsm = new(); //Need to generate; should make a class to manage the generation.
                    BSMPDU pdu = new()
                    {
                        BSM = bsm,
                    };

                    await _server.SendToAllAsync(pdu, stoppingToken);
                    _logger.LogInformation("The BSM server sent a BSM to the remote clients. {BSM}", pdu.BSM.ToTypeB());
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "The BSM server failed to send the BSM to the remote clients.");
                }
            }

            await Task.Delay(10_000, stoppingToken);
        }
    }
}

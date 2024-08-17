using JMayer.Example.WindowsService.BSM;
using JMayer.Net;
using JMayer.Net.ProtocolDataUnit;

namespace JMayer.Example.WindowsService;

/// <summary>
/// The class manages connecting to the server and processes BSMs received from the server.
/// </summary>
internal class BSMClientWorker : BackgroundService
{
    /// <summary>
    /// Used to log activity for the service.
    /// </summary>
    private readonly ILogger<BSMServerConnectionWorker> _logger;

    /// <summary>
    /// Used to manage TCP/IP communication.
    /// </summary>
    private readonly IClient _client;

    /// <summary>
    /// The dependency injection constructor.
    /// </summary>
    /// <param name="logger">Used to log activity for the service.</param>
    /// <param name="client">Used to manage TCP/IP communication.</param>
    public BSMClientWorker(ILogger<BSMServerConnectionWorker> logger, IClient client)
    {
        _logger = logger;
        _client = client;
    }

    /// <summary>
    /// The method manages connecting to the server and processes BSMs received from the server.
    /// </summary>
    /// <param name="stoppingToken">Used to cancel the task when the service stops.</param>
    /// <returns>A Task object for the async.</returns>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (!_client.IsConnected)
            {
                try
                {
                    await _client.ConnectAsync("127.0.0.1", BSMServerConnectionWorker.Port, stoppingToken);
                    _logger.LogInformation("The client connected to the BSM server.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "The client failed to connect to the BSM server.");
                }
            }
            else
            {
                List<PDU> pdus = await _client.ReceiveAndParseAsync(stoppingToken);

                foreach (BSMPDU pdu in pdus.Cast<BSMPDU>())
                {
                    if (pdu.IsValid)
                    {
                        _logger.LogInformation("The client received a valid BSM from the server. {BSM}", pdu.BSM.ToTypeB());
                    }
                    else
                    {
                        _logger.LogWarning("The client received an invalid BSM from the server. {BSM}", pdu.BSM.ToTypeB());
                    }
                }
            }

            await Task.Delay(1000);
        }
    }

    /// <inheritdoc/>
    /// <remarks>
    /// This is overriden so the client can disconnect from the server.
    /// </remarks>
    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        try
        {
            _client.Disconnect();
            _logger.LogInformation("The client disconnect from the server.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "The client failed to disconnect from the server; it may have already been disconnected by the server.");
        }

        await base.StopAsync(cancellationToken);
    }
}

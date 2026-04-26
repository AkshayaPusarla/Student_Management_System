using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using System;
using System.Threading.Tasks;

namespace StudetAzureFunction.ServiceBusTrigger;

public class Function
{
    private readonly ILogger<Function> _logger;

    public Function(ILogger<Function> logger)
    {
        _logger = logger;
    }

    [Function(nameof(Function))]
    public async Task Run(
        [ServiceBusTrigger("queue0", Connection = "ServiceBusConnection")]
        string message)
    {
        _logger.LogInformation($"Received: {message}");
    }
}

using ArchLab1.Controller;
using ArchLab1Lib;
using Azure.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Server;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace ArchLab1.Network
{
    internal class UdpListener : IHostedService
    {
        private readonly IServiceProvider serviceProvider;
        private UdpClient udpClient;
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        private IPEndPoint clientIp;
        private readonly ILogger<UdpListener> logger;
        public UdpListener(IOptions<ServerConfig> serverOps, IOptions<ClientConfig> clientOps, ILogger<UdpListener> logger, IServiceProvider serviceProvider) 
        {
            this.serviceProvider = serviceProvider;
            this.udpClient = new UdpClient(serverOps.Value.Port);
            this.clientIp = new IPEndPoint(IPAddress.Parse(clientOps.Value.Ip), clientOps.Value.Port);
            this.logger = logger;
        }

        public async Task ListenAsync()
        {
            while (true)
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var controller = scope.ServiceProvider.GetService<CsvWebController>();
                    var result = await udpClient.ReceiveAsync();
                    ArchLab1Lib.Request request;
                    using (var stream = new MemoryStream(result.Buffer))
                    {
                        request = (ArchLab1Lib.Request)binaryFormatter.Deserialize(stream);
                    }

                    logger.LogInformation($"Requset from {result.RemoteEndPoint}, command: {request.Command}");
                    byte[] responseBytes;
                    var response = controller.ProcessCommand(request);
                    using (var stream = new MemoryStream())
                    {
                        binaryFormatter.Serialize(stream, response);
                        responseBytes = stream.ToArray();
                    }
                    _ = udpClient.SendAsync(responseBytes, responseBytes.Length, clientIp);
                    logger.LogInformation($"response sent to {clientIp}");
                }
            }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await ListenAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

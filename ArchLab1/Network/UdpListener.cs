
using ArchLab1.Controller;
using ArchLab1Lib;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace ArchLab1.Network
{
    internal class UdpListener : IHostedService
    {
        private UdpClient udpClient;
        private BinaryFormatter binaryFormatter = new BinaryFormatter();
        private CsvWebController controller;
        private IPEndPoint clientIp;
        private readonly ILogger<UdpListener> logger;
        public UdpListener(int port, CsvWebController controller, IPEndPoint clientIp, ILogger<UdpListener> logger) 
        {
            this.udpClient = new UdpClient(port);
            this.controller = controller;
            this.clientIp = clientIp;
            this.logger = logger;
        }

        public async Task ListenAsync()
        {
            while (true)
            {
                var result = await udpClient.ReceiveAsync();
                Request request;
                using (var stream = new MemoryStream(result.Buffer))
                {
                    request = (Request)binaryFormatter.Deserialize(stream);
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

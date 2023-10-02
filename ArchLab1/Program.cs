using System;
using System.Security.Cryptography.X509Certificates;
using ArchLab1.Holders;
using ArchLab1.Model;
using ArchLab1.View;
using ArchLab1.Controller;
using ArchLab1Lib.Model;
using System.Net.Sockets;
using System.Text;
using System.Net;
using Server.View;
using ArchLab1.Network;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Server;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ArchLab1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddOptions<ClientConfig>().BindConfiguration("Client");
            builder.Services.AddOptions<CsvConfig>().BindConfiguration("Csv");
            
            builder.Services.AddOptions<ServerConfig>().BindConfiguration("Server");


            builder.Services.AddSingleton<CsvRepository<TaskEntity, long>>(serviceProvider =>
            {
                var options = serviceProvider.GetService<IOptions<CsvConfig>>();

                return new CsvRepository<TaskEntity, long>(options!.Value.Path);
            });
            builder.Logging.Services.AddLogging();
            builder.Services.AddSingleton<WebView>();
            builder.Services.AddSingleton<CsvWebController>();
            builder.Services.AddHostedService<UdpListener>(serviceProvider =>
            {
                var clientOptions = serviceProvider.GetService<IOptions<ClientConfig>>()!.Value;
                var serverOptions = serviceProvider.GetService<IOptions<ServerConfig>>()!.Value;
                var controller = serviceProvider.GetService<CsvWebController>();
                var logger = serviceProvider.GetService<ILogger<UdpListener>>();
                var ip = new IPEndPoint(IPAddress.Parse(clientOptions.Ip), clientOptions.Port);


                return new UdpListener(serverOptions.Port, controller, ip, serviceProvider.GetRequiredService<ILogger<UdpListener>>());
            });
            var host = builder.Build();
            await host.RunAsync();

        }
    }
}
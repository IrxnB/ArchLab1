using System;
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
using Server.Model;
using Microsoft.EntityFrameworkCore;

namespace ArchLab1 // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static async System.Threading.Tasks.Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddOptions<ClientConfig>().BindConfiguration("Client");
            builder.Services.AddOptions<CsvConfig>().BindConfiguration("Csv");
            
            builder.Services.AddOptions<ServerConfig>().BindConfiguration("Server");
            builder.Services.AddDbContext<ArchDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

            builder.Logging.Services.AddLogging();
            builder.Services.AddSingleton<WebView>();
            builder.Services.AddScoped<CsvWebController>();
            builder.Services.AddHostedService<UdpListener>();
            var host = builder.Build();
            await host.RunAsync();

        }
    }
}
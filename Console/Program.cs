using System.Text;
using OAML.Domain.Protocol;
using OAML.Protocol.Building;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OAML.Infrastructure.Configuration;

namespace Console;

static class Program
{
    /*
     * Console.UI: to send messages to known nodes
       
       Protocol: to get signing/encryption for envelope building
       
       Infrastructure: to store/retrieve node configs from disk/network
     */
    
    private static void LoadPlugins()
    {
        //crypts: exePath/lib/cryptos/
        //signs: exePath/lib/signs/
        
        //ie: exePath/lib/cryptos/aes-256.dll
        
        //idea: automap dll to name using the filename
        //ie: aes-256.dll becomes "aes-256"
        //Then just load all dlls in plugin directory and store them in memory
        // OR if that is a security risk, just call the dll path when you need to run the crypt lib
        
        //example command(s):
        //
        //add crypt "aes" "aes-256.dll"
        //set node john
        //add node crypt "aes" "C:/path/to/public.key" "C:/path/to/private.key"
        //send message john aes "Hey man!"
        
    }
    
    static async Task Main(string[] args) 
    {
        var host = Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration(config =>
            {
                config.AddJsonFile("settings.json", optional: false, reloadOnChange: true);
            })
            .ConfigureServices((context, services) =>
            {
                var config = context.Configuration;
                // Bind configuration
                services.Configure<HostConfig>(context.Configuration.GetSection("host"));
                services.Configure<List<NodeConfig>>(config.GetSection("nodes"));

                // Register application services
                //services.AddSingleton<IEnvelopeBuilder, DefaultEnvelopeBuilder>();
                //services.AddSingleton<INodeRegistry, ConfigNodeRegistry>();

                // Entry point
                services.AddTransient<App>(); // This is your main "runner" class
            })
            .Build();

        // Resolve and run your app
        var app = host.Services.GetRequiredService<App>();
        await app.RunAsync();
        
    }
}
using Console.Commands;
using Microsoft.Extensions.Options;
using OAML.Domain.Cryptography.Keys;
using OAML.Domain.Nodes;
using OAML.Domain.Protocol;
using OAML.Infrastructure.Configuration;
using OAML.Infrastructure.Protocol;
using OAML.Protocol.Building;
using OAML.Protocol.Parsing;
using Spectre.Console.Cli;
using Common.Extensions;
using Console.Cli;

namespace Console;

public class App
{
    private readonly HostConfig _host;
    private readonly ICollection<NodeConfig> _nodes;
    
    private readonly CommandApp _cliApp;

    private Node CurrentNode;

    public App(IOptions<HostConfig> c1, IOptions<List<NodeConfig>> c2, IServiceProvider services)
    {
        _host = c1.Value;
        _nodes = c2.Value;
        
        _cliApp = new CommandApp(new TypeRegistrar(services));
        _cliApp.Configure(config =>
        {
            config.AddCommand<Console.Commands.SendMessage>("send");
        });


    }

    public async Task RunAsync()
    {
        System.Console.WriteLine("App starting...");
        System.Console.WriteLine($"Host IP: {_host.Ip}");
        
        while (true)
        {
            System.Console.Write("> ");
            string input = System.Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) continue;
            
            if (input.Trim().ToLower() is "exit" or "quit")
            {
                System.Console.WriteLine("Exiting...");
                return; // or Environment.Exit(0);
            }
            
            if (input.Trim().ToLower() == "help")
            {
                await _cliApp.RunAsync(new[] { "--help" });
                continue;
            }

            // Tokenize like a shell would
            var args = input.Tokenize();

            try
            {
                await _cliApp.RunAsync(args);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
    
    //Command List
            
    //show: shows commands
            
    //show self: shows configuration for current port
    //set port {x}: sets port for server, restarts server service
    //set ip {x}: sets ip for server, restarts server service
            
    //show node {name}: shows configuration for specified node
    //add node {name}: creates a node (or friend) to send to
    //add node {name} {ip}
    //add node {name} {ip}:{port}
            
    //set node {node}: ?? maybe, before using below.
    //add node crypt {name}: allows one of the loaded crypto managers to be used with this node.
    //add node signature {name}: allows one of loaded signature managers to be used with this node.
    //ping node {name}: pings a node to check if online, must specify valid name
            
    //show crypt: shows supported crypto methods
    //show signature: shows supported signature methods
    //add crypt {name} {path_to_dll}: adds a dll path to load (encryption manager)
    //add signature {name} {path_to_dll}; adds a dll path to load (signature manager)
            
    //maybe two-step this process...
    //send message {node_name} {crypt_name} {sig_name?} {message}
            
    //ie:
    //send message {node_name} {crypt_name} {sig_name}
    //enter message to send: _
            
    //other implications... how would I display chat history? 
    //Do I need to develop a console that uses ascii art to
    //split commands and chat window?
    
    private async Task ReceiveBlob(Node recipient, byte[] blob)
    {
        //parse blob as envelope
        var parser = new BasicEnvelopeParser();

        if (!parser.CanParse(blob))
            throw new NotSupportedException("Invalid blob format");

        var envelope = parser.Parse(blob);
        
        
        //if supported, handle decryption
        var handler = new BasicEnvelopeHandler();

        //TODO: Change recipient here to host since receiving...
        
        if (!handler.CanHandle(envelope, recipient))
            throw new NotSupportedException();
        
        //so instead of recipient here, I would actually want the host.
        //I need to see if the HOST supports the engineId of the envelope
        //in order to decrypt it.
        byte[] decryptedRaw = handler.Decrypt(envelope, recipient);

        string decrypted = System.Text.Encoding.UTF8.GetString(decryptedRaw);

        int bp = 0;
    }
}
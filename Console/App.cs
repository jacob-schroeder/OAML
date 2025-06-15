using Console.Commands;
using Microsoft.Extensions.Options;
using OAML.Domain.Cryptography.Keys;
using OAML.Domain.Nodes;
using OAML.Domain.Protocol;
using OAML.Infrastructure.Configuration;
using OAML.Infrastructure.Protocol;
using OAML.Protocol.Building;
using OAML.Protocol.Parsing;

namespace Console;

public class App
{
    private readonly HostConfig _host;
    private readonly ICollection<NodeConfig> _nodes;

    private Node CurrentNode;

    public App(IOptions<HostConfig> c1, IOptions<List<NodeConfig>> c2)
    {
        _host = c1.Value;
        _nodes = c2.Value;
    }

    public async Task RunAsync()
    {
        System.Console.WriteLine("App starting...");
        
        System.Console.WriteLine($"Host IP: {_host.Ip}");
        System.Console.WriteLine($"First node name: {_nodes.First().Name}");

        CurrentNode = _nodes.First().ToNode();

        await Process();
    }

    private async Task Process()
    {
        //get input from user
        string input = ""; //System.Console.ReadLine();
        var cmd = CommandParser.Parse(input);

        //get the recipient from input
        var recipient = CurrentNode; //GetNode("john")
        
        //send the input to network
        var blob = await SendMessage(recipient, "aes", "Hey claire!");

        
        //get the input from network
        await ReceiveBlob(recipient, blob);
    }

    private async Task<byte[]> SendMessage(Node recipient, string crypt, string message)
    {
        System.Console.WriteLine($"Setting node for {recipient.Name}({recipient.Id})");

        //check if crypt is supported
        var supported = recipient.ConfiguredFor(crypt);
        if(supported == false)
            throw new NotSupportedException("Unsupported protocol");

        //load the crypto engine and start the message
        var engine = recipient.LoadEngine(crypt);
        var builder = new TextBuilder()
            .SetMessage(message); //msg
        
        builder.SetEngine(engine);
        
        //build the encrypted envelope
        var envelope = builder.Build(recipient);
        var bytes = envelope.ToBytes();
        
        //temporary
        return bytes;

        //here's where you would send...
        //var result = client.SendEnvelope(node, envelope); tbh more of a fan of this one...
        // or...
        //var result = client.SendEnvelope(node, bytes);
    }

    private async Task ReceiveBlob(Node recipient, byte[] blob)
    {
        //parse blob as envelope
        var parser = new BasicEnvelopeParser();

        if (!parser.CanParse(blob))
            throw new NotSupportedException("Invalid blob format");

        var envelope = parser.Parse(blob);
        
        
        //if supported, handle decryption
        var handler = new BasicEnvelopeHandler();

        if (!handler.CanHandle(envelope, recipient))
            throw new NotSupportedException();
        
        byte[] decryptedRaw = handler.Decrypt(envelope, recipient);

        string decrypted = System.Text.Encoding.UTF8.GetString(decryptedRaw);

        int bp = 0;
    }
    
    /*
    static void MainNEW(string[] args)
    {
        /* idea
         ========================
         while(_exitNotRequested)
         {
            RenderWindow();
         }
         
        System.Console.WriteLine("Welcome to OAML");
        System.Console.WriteLine("===============");

        while (true)
        {
            var input =  System.Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
                input = "exit";
            
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

            if (input.ToLower().Equals("exit"))
                return;
        }
    }
    */
}
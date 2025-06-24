using Spectre.Console.Cli;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Options;
using OAML.Domain.Nodes;
using OAML.Domain.Protocol;
using OAML.Infrastructure.Configuration;
using OAML.Protocol.Building;

namespace Console.Commands;

public class SendMessageSettings : CommandSettings
{
    [CommandArgument(0, "<node>")]
    [Description("The name of the node to send the message to.")]
    public string Node { get; set; }

    [CommandArgument(1, "<engine>")]
    [Description("The name of the crypto engine to use.")]
    public string Engine { get; set; }

    [CommandArgument(2, "<message>")]
    [Description("The message content to send.")]
    public string Message { get; set; }
}

public class SendMessage : Command<SendMessageSettings>
{
    private readonly ICollection<NodeConfig> _nodes;

    public SendMessage(IOptions<List<NodeConfig>> c2)
    {
        _nodes = c2.Value;
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] SendMessageSettings settings)
    {
        System.Console.WriteLine($"[SendMessage] Sending message to node: {settings.Node}");
        System.Console.WriteLine($"Engine: {settings.Engine}");
        System.Console.WriteLine($"Message: {settings.Message}");

        var recipient = _nodes.Single(n => n.Name == settings.Node).ToNode(); //get from settings.Node
        var crypt = settings.Engine;
        var message = settings.Message;
        
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
        //SendOverTcp(ip, bytes);

        return 0; // success
    }
}
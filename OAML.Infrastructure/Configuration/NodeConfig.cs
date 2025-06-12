using OAML.Domain.Network;
using OAML.Domain.Nodes;

namespace OAML.Infrastructure.Configuration;

public class NodeConfig
{
    public string Name { get; set; }

    public string Ip { get; set; }
    public int Port { get; set; }

    public List<CryptoConfig> Crypts { get; set; } = [];

    public Node ToNode()
    {
        return new Node()
        {
            Name = Name,
            Endpoint = Endpoint.Parse(Ip + ":" + Port),
            
            Crypts = Crypts.Select(c => c.ToCryptProvider()).ToList()
        };
    }
}
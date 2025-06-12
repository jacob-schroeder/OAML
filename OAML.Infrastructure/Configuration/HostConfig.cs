namespace OAML.Infrastructure.Configuration;

public class HostConfig
{
    public string Ip { get; set; }
    public int Port { get; set; }

    public List<CryptoConfig> Crypts { get; set; } = [];
    public List<NodeConfig> Nodes { get; set; } = [];
}
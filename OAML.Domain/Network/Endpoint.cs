using System;
using System.Net;
using System.Text.RegularExpressions;

namespace OAML.Domain.Network;

public class Endpoint
{
    public IPAddress IP { get; }
    public int Port { get; }

    public Endpoint(IPAddress ip, int port)
    {
        if (port is < 1 or > 65535)
            throw new ArgumentOutOfRangeException(nameof(port), "Port must be between 1 and 65535");

        IP = ip;
        Port = port;
    }

    public static Endpoint Parse(string input)
    {
        // Supports formats like "192.168.0.1:8080" or "[::1]:5000"
        var match = Regex.Match(input, @"^\[?(?<ip>.+?)\]?:?(?<port>\d+)$");
        if (!match.Success)
            throw new FormatException("Invalid endpoint format");

        var ipString = match.Groups["ip"].Value;
        var portString = match.Groups["port"].Value;

        if (!IPAddress.TryParse(ipString, out var ip))
            throw new FormatException("Invalid IP address");

        if (!int.TryParse(portString, out var port))
            throw new FormatException("Invalid port number");

        return new Endpoint(ip, port);
    }

    public override string ToString()
    {
        // Add brackets for IPv6
        string ipStr = IP.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6
            ? $"[{IP}]"
            : IP.ToString();

        return $"{ipStr}:{Port}";
    }

    public IPEndPoint ToIpEndPoint() => new IPEndPoint(IP, Port);
}

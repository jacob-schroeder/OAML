using System.Reflection;
using OAML.Domain.Cryptography;

namespace OAML.Domain.Nodes;

public class CryptProvider
{
    private uint? _engineId;

    public uint EngineId => _engineId.Value;
    public string Name { get; set; }
    public string DllPath { get; set; }
    
    public string PublicKeyPath { get; set; }
    public string PrivateKeyPath { get; set; }

    
    public ICryptoEngine LoadDllAsEngine()
    {
        if(DllPath == null)
            throw new NullReferenceException("DllPath");

        if (!File.Exists(DllPath))
            throw new DllNotFoundException(DllPath);
        
        //set id...
        byte[] data = File.ReadAllBytes(DllPath);
        _engineId = Common.Security.Crc32.Compute(data);

        //load plugin...
        var pluginContext = new EngineLoadContext(DllPath);
        Assembly pluginAssembly = pluginContext.LoadFromAssemblyPath(DllPath);

        foreach (var type in pluginAssembly.GetTypes())
        {
            Console.WriteLine($"Type: {type.FullName}");
            Console.WriteLine($"Implements ICryptoEngine? {typeof(ICryptoEngine).IsAssignableFrom(type)}");
            Console.WriteLine($"ICryptoEngine Assembly (expected): {typeof(ICryptoEngine).Assembly.FullName}");
            Console.WriteLine($"ICryptoEngine Assembly (from plugin): {type.GetInterface("ICryptoEngine")?.Assembly.FullName}");
        }


        Type pluginType = pluginAssembly
                            .GetTypes()
                            .FirstOrDefault(t => 
                                typeof(ICryptoEngine).IsAssignableFrom(t) && 
                                !t.IsAbstract
                            );

        if (pluginType != null)
        {
            return (ICryptoEngine)Activator.CreateInstance(pluginType);
        }

        return null;

        // Later, to unload:
        //pluginContext.Unload();
        //GC.Collect();
        //GC.WaitForPendingFinalizers();

        //no longer used
        //return new AesCryptoEngine();
    }
}
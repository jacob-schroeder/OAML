using System.Reflection;
using System.Runtime.Loader;

namespace OAML.Domain.Cryptography;

public class EngineLoadContext : AssemblyLoadContext
{
    private AssemblyDependencyResolver _resolver;

    public EngineLoadContext(string pluginPath) : base(isCollectible: true) // ✅ makes it unloadable
    {
        _resolver = new AssemblyDependencyResolver(pluginPath);
    }

    protected override Assembly Load(AssemblyName assemblyName)
    {
        string assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);
        if (assemblyPath != null)
        {
            return LoadFromAssemblyPath(assemblyPath);
        }

        return null;
    }
}

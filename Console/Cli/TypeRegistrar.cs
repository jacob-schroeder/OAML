using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using System;

namespace Console.Cli;

public sealed class TypeRegistrar : ITypeRegistrar
{
    private readonly IServiceProvider _provider;

    public TypeRegistrar(IServiceProvider provider)
    {
        _provider = provider;
    }

    public ITypeResolver Build()
    {
        return new TypeResolver(_provider);
    }

    public void Register(Type service, Type implementation)
    {
        // no-op: services are already registered in your container
    }

    public void RegisterInstance(Type service, object implementation)
    {
        // no-op: not used
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
        // no-op: not used
    }
}

public sealed class TypeResolver : ITypeResolver
{
    private readonly IServiceProvider _provider;

    public TypeResolver(IServiceProvider provider)
    {
        _provider = provider;
    }
    
    /*
    public object Resolve(Type type)
    {
        
        return _provider.GetRequiredService(type);
    }
    */
    
    public object Resolve(Type type)
    {
        return ActivatorUtilities.GetServiceOrCreateInstance(_provider, type);
    }

}
using Microsoft.Extensions.Configuration;

namespace PandaVaultClient;

public class PandaVaultConfigurationBuilder : IConfigurationBuilder
{
    private readonly PandaVaultConfigurationSource _source;

    public PandaVaultConfigurationBuilder(string url, string secret)
    {
        _source = new PandaVaultConfigurationSource { Url = url, Secret = secret };
    }
    
    public IConfigurationBuilder Add(IConfigurationSource source)
    {
        Sources.Add(source);
        return this;
    }

    public IConfigurationRoot Build()
    {
        return new ConfigurationRoot(new List<IConfigurationProvider> { new PandaVaultConfigurationProvider(_source) });
    }

    public IDictionary<string, object> Properties { get; } = null!;
    public IList<IConfigurationSource> Sources { get; } = null!;
}
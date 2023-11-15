using Microsoft.Extensions.Configuration;

namespace PandaVaultClient;

public class PandaVaultConfigurationSource : IConfigurationSource
{
    public string Url { get; set; } = null!;
    public string Secret { get; set; } = null!;

    public IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        return new PandaVaultConfigurationProvider(this);
    }
}
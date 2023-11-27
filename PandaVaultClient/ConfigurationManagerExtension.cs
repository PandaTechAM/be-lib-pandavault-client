using Microsoft.Extensions.Configuration;

namespace PandaVaultClient;

public static class ConfigurationManagerExtension
{
    public static IConfigurationManager AddPandaVault(this IConfigurationManager configurationManager)
    {
        configurationManager.Add(new PandaVaultConfigurationSource(configurationManager));
        return configurationManager;
    }
}
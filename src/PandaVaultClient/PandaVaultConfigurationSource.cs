using Microsoft.Extensions.Configuration;

namespace PandaVaultClient;

public class PandaVaultConfigurationSource(IConfiguration existingConfiguration) : IConfigurationSource
{
   private IConfiguration ExistingConfiguration { get; } = existingConfiguration;

   public IConfigurationProvider Build(IConfigurationBuilder builder)
   {
      return new PandaVaultConfigurationProvider(ExistingConfiguration);
   }
}
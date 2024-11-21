using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PandaVaultClient;

public static class ConfigurationManagerExtension
{
   public static IConfigurationManager AddPandaVault(this IConfigurationManager configurationManager)
   {
      configurationManager.Add(new PandaVaultConfigurationSource(configurationManager));
      return configurationManager;
   }

   public static WebApplicationBuilder RegisterPandaVaultEndpoint(this WebApplicationBuilder builder)
   {
      builder.Services.AddSingleton<PandaVaultConfigurationProvider>();
      return builder;
   }
}
using System.Collections.Immutable;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace PandaVaultClient;

public static class WebApplicationBuilderExtensions
{
   public static WebApplicationBuilder AddPandaVault(this WebApplicationBuilder builder)
   {
      var configurationManager = builder.Configuration.Add();
      
      configurationManager.Add(new PandaVaultConfigurationSource(configurationManager));
      return configurationManager;
   }
}
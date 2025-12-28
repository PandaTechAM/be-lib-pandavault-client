using Microsoft.AspNetCore.Builder;

namespace PandaVaultClient.Extensions;

public static class WebApplicationBuilderExtensions
{
   public static WebApplicationBuilder AddPandaVault(this WebApplicationBuilder builder)
   {
      var pandaVaultConfigs = PandaVaultHttpClient.FetchConfigurationsAsync()
                                                  .Result;


      builder.Configuration.AddPandaVaultConfigurations(pandaVaultConfigs);
      return builder;
   }
}
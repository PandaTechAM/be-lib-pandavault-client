using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PandaVaultClient.Dtos;

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
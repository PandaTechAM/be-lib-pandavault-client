using Microsoft.Extensions.Configuration;
using PandaVaultClient.Dtos;

namespace PandaVaultClient.Extensions;

internal static class ConfigurationExtensions
{
   internal static void AddPandaVaultConfigurations(this IConfiguration configuration,
      List<PandaVaultConfig> newConfigs)
   {
      foreach (var config in newConfigs)
      {
         configuration[config.Key] = config.Value;
      }

      configuration.EnsureComplete();
   }
   
   private static void EnsureComplete(this IConfiguration configuration)
   {
      foreach (var kvp in configuration.AsEnumerable())
      {
         if (kvp.Value == "**")
         {
            throw new InvalidOperationException($"Configuration key '{kvp.Key}' is not configured in the PandaVault.");
         }
      }
   }
}
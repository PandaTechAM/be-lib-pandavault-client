using Microsoft.Extensions.Configuration;
using PandaVaultClient.Dtos;

namespace PandaVaultClient;

public class PandaVaultConfigurationProvider(IConfiguration existingConfiguration) : ConfigurationProvider
{
   public override void Load()
   {
      var lines = HttpHelper.FetchConfigurationsAsync()
                           .Result;

      var requiredKeys = existingConfiguration.AsEnumerable()
                                              .Where(x => x.Value == "**");

      var missingKeys = requiredKeys.Where(x => lines.TrueForAll(y => y.Key != x.Key))
                                    .Select(x => x.Key)
                                    .ToList();

      if (missingKeys.Count > 0)
      {
         throw new ArgumentException(
            $"The following configurations are empty not set in Vault {string.Join(", ", missingKeys)}");
      }

      Data = lines
             .Where(x => existingConfiguration[x.Key] != null)
             .ToDictionary(x => x.Key, x => x.Value)!;
   }
}
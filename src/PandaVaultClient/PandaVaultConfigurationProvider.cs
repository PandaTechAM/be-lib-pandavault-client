using Microsoft.Extensions.Configuration;
using PandaVaultClient.Dtos;

namespace PandaVaultClient;

public class PandaVaultConfigurationProvider(IConfiguration existingConfiguration) : ConfigurationProvider
{
   public override void Load()
   {
      List<ConfigurationDto> lines;
      try
      {
         lines = HttpHelper.FetchConfigurationsAsync()
                           .Result;
      }
      catch (Exception ex)
      {
         throw new InvalidOperationException("Error on fetching configurations", ex);
      }

      var requiredKeys = existingConfiguration.AsEnumerable()
                                              .Where(x => x.Value == "**");

      var missingKeys = requiredKeys.Where(x => lines.TrueForAll(y => y.key != x.Key))
                                    .Select(x => x.Key)
                                    .ToList();

      if (missingKeys.Count > 0)
      {
         throw new ArgumentException(
            $"The following configurations are empty not set in Vault {string.Join(", ", missingKeys)}");
      }

      Data = lines
             .Where(x => existingConfiguration[x.key] != null)
             .ToDictionary(x => x.key, x => x.value)!;
   }

   public List<AllConfigurationsDto> GetAllConfigurations(string pandaVaultSecret)
   {
      if (pandaVaultSecret != Environment.GetEnvironmentVariable("PANDAVAULT_SECRET"))
      {
         throw new ArgumentException("PandaVault secret is not correct");
      }

      var allConfigurations = existingConfiguration.AsEnumerable()
                                                   .Select(conf => new AllConfigurationsDto
                                                   {
                                                      Key = conf.Key,
                                                      Value = conf.Value
                                                   })
                                                   .ToList();

      return allConfigurations;
   }
}
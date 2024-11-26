using System.Net.Http.Json;
using PandaVaultClient.Dtos;
using RegexBox;

namespace PandaVaultClient;

public static class HttpHelper
{
   private static readonly string Url = Environment.GetEnvironmentVariable("PANDAVAULT_URL")!;
   private static readonly string Secret = Environment.GetEnvironmentVariable("PANDAVAULT_SECRET")!;

   public static async Task<List<ConfigurationDto>> FetchConfigurationsAsync()
   {
      const string endpoint = "/api/v1/vault-configs";
      using var client = new HttpClient();
      var validUrl = PandaValidator.IsUri(Url, false);
      
      if (!validUrl)
      {
         throw new ArgumentNullException($"PANDAVAULT_URL is not valid. Url: {Url}");
      }
      if (string.IsNullOrWhiteSpace(Secret))
      {
         throw new ArgumentNullException("PANDAVAULT_SECRET environment variable is not set");
      }

      client.DefaultRequestHeaders.Add("secret", Secret);
      
      var response = await client.GetAsync($"{Url}{endpoint}");
    
      List<ConfigurationDto> configurations = new();
      
      if (response.IsSuccessStatusCode)
      {
         configurations = (await response.Content.ReadFromJsonAsync<List<ConfigurationDto>>())!;
         
         if (configurations.Count == 0)
         {
            
         }
      }

      return (await response.Content.ReadFromJsonAsync<List<ConfigurationDto>>())!;
   }
}
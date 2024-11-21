using System.Net;
using System.Net.Http.Json;
using PandaVaultClient.Dtos;

namespace PandaVaultClient;

public static class HttpHelper
{
   private static readonly string Url = Environment.GetEnvironmentVariable("PANDAVAULT_URL")!;
   private static readonly string Secret = Environment.GetEnvironmentVariable("PANDAVAULT_SECRET")!;

   public static async Task<List<ConfigurationDto>> FetchConfigurationsAsync()
   {
      const string endpoint = "/api/v1/vault-configs";
      using var client = new HttpClient();

      client.DefaultRequestHeaders.Add("secret", Secret);
      var response = await client.GetAsync($"{Url}{endpoint}");
      if (response.StatusCode == HttpStatusCode.BadRequest)
      {
         throw new HttpRequestException("PANDAVAULT_SECRET environment variable's value is not correct",
            null,
            response.StatusCode);
      }

      if (!response.IsSuccessStatusCode)
      {
         throw new HttpRequestException("Failed to retrieve configuration data from the HTTP endpoint.",
            null,
            response.StatusCode);
      }

      return (await response.Content.ReadFromJsonAsync<List<ConfigurationDto>>())!;
   }
}
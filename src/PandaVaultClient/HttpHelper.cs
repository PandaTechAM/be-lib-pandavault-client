using System.Net;
using System.Net.Http.Json;
using PandaVaultClient.Dtos;
using ResponseCrafter.HttpExceptions;

namespace PandaVaultClient;

public static class HttpHelper
{
   private static readonly string Url = Environment.GetEnvironmentVariable("PANDAVAULT_URL")!;
   private static readonly string Secret = Environment.GetEnvironmentVariable("PANDAVAULT_SECRET")!;

   public static async Task<List<ConfigurationDto>> FetchConfigurationsAsync()
   {
      const string endpoint = "/api/v1/vault-configs";
      using var client = new HttpClient();
      
      if (Url is null)
      {
         throw new ArgumentNullException($"PANDAVAULT_URL environment variable is not set");
      }
      if (Secret is null)
      {
         throw new ArgumentNullException($"PANDAVAULT_SECRET environment variable is not set");
      }

      client.DefaultRequestHeaders.Add("secret", Secret);
      var response = await client.GetAsync($"{Url}{endpoint}");
      if (response.StatusCode == HttpStatusCode.BadRequest)
      {
         throw new BadRequestException("PANDAVAULT_SECRET environment variable's value is not correct");
      }

      if (!response.IsSuccessStatusCode)
      {
         throw new BadRequestException("Failed to fetch configuration from PandaVault. Please check the client settings and network connectivity.");
      }

      return (await response.Content.ReadFromJsonAsync<List<ConfigurationDto>>())!;
   }
}
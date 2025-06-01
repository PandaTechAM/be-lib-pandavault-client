using System.Net.Http.Json;
using PandaVaultClient.Dtos;

namespace PandaVaultClient;

public static class PandaVaultHttpClient
{
   public static async Task<List<PandaVaultConfig>> FetchConfigurationsAsync()
   {
      const string endpoint = "/api/v1/vault-configs";
      var url = Environment.GetEnvironmentVariable("PANDAVAULT_URL") ??
                throw new ArgumentNullException("PANDAVAULT_URL environment variable is not set");

      var secret = Environment.GetEnvironmentVariable("PANDAVAULT_SECRET") ??
                   throw new ArgumentNullException("PANDAVAULT_SECRET environment variable is not set");

      ValidateUrlAndSecret(url, secret);

      try
      {
         using var client = new HttpClient();

         client.DefaultRequestHeaders.Add("secret", secret);

         var response = await client.GetAsync($"{url}{endpoint}");

         if (!response.IsSuccessStatusCode)
         {
            throw new HttpRequestException($"Failed to fetch configurations. Status Code: {response.StatusCode}");
         }

         var configurations = await response.Content.ReadFromJsonAsync<List<PandaVaultConfig>>();

         if (configurations is null || configurations.Count == 0)
         {
            Console.WriteLine("The secret is wrong or there is no configurations set");
         }

         return configurations ?? [];
      }
      catch (Exception)
      {
         Console.WriteLine("Failed to fetch configurations. Below you can read problem details -----");
         throw;
      }
   }

   private static void ValidateUrlAndSecret(string url, string secret)
   {
      var validUrl = IsUri(url, false);

      if (!validUrl)
      {
         throw new ArgumentNullException($"PANDAVAULT_URL is not valid. Url: {url}");
      }

      if (string.IsNullOrWhiteSpace(secret))
      {
         throw new ArgumentNullException("PANDAVAULT_SECRET environment variable is not set");
      }
   }
   
   private static bool IsUri(string uri, bool allowNonSecure = true)

   {
      Uri.TryCreate(uri, UriKind.Absolute, out var parsedUri);

      if (parsedUri is null)
      {
         return false;
      }

      if (!allowNonSecure && parsedUri.Scheme == Uri.UriSchemeHttp)
      {
         return false;
      }

      return true;
   }
}
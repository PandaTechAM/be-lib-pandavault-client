using System.Net;
using System.Net.Http.Json;
using PandaVaultClient.Dtos;

namespace PandaVaultClient;
public static class HttpHelper
{
    public static async Task<List<ConfigurationDto>> FetchConfigurationsAsync()
    {
        const string endpoint = "/api/v1/vault-configs";
        var url = Environment.GetEnvironmentVariable("PANDAVAULT_URL");
        var secret = Environment.GetEnvironmentVariable("PANDAVAULT_SECRET");
        using var client = new HttpClient();

        client.DefaultRequestHeaders.Add("secret", secret);
        var response = await client.GetAsync($"{url}{endpoint}");
        if (response.StatusCode == HttpStatusCode.BadRequest)
        {
            throw new HttpRequestException("PANDAVAULT_SECRET environment variable's value is not correct", null,
                response.StatusCode);
        }

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Failed to retrieve configuration data from the HTTP endpoint.", null,
                response.StatusCode);
        }

        return (await response.Content.ReadFromJsonAsync<List<ConfigurationDto>>())!;
    }
}
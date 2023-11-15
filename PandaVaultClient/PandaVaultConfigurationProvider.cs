using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

namespace PandaVaultClient;

public class PandaVaultConfigurationProvider : ConfigurationProvider
{
    private readonly PandaVaultConfigurationSource _source;

    public PandaVaultConfigurationProvider(PandaVaultConfigurationSource source)
    {
        _source = source;
    }

    public override void Load()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("secret", _source.Secret);
        var response = client.GetAsync($"{_source.Url}/vaults-json").Result;

        if (response.IsSuccessStatusCode)
        {
            var data = response.Content.ReadFromJsonAsync<Dictionary<string, string>>().Result;

            if (data != null)
            {
                Data = data!;
            }
        }
        else
        {
            throw new HttpRequestException("Failed to retrieve configuration data from the HTTP endpoint.", null,
                response.StatusCode);
        }
    }
}
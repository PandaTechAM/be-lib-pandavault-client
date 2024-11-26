using System.Text.Json.Serialization;

namespace PandaVaultClient.Dtos;

public class ConfigurationDto
{
   [JsonPropertyName("key")]
   public string Key { get; set; } = null!;
   
   [JsonPropertyName("value")]
   public string Value { get; set; } = null!;
}
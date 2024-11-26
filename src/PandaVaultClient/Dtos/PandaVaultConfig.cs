using System.Text.Json.Serialization;

namespace PandaVaultClient.Dtos;

public class PandaVaultConfig
{
   [JsonPropertyName("key")]
   public required string Key { get; set; }

   [JsonPropertyName("value")]
   public required string Value { get; set; }
}
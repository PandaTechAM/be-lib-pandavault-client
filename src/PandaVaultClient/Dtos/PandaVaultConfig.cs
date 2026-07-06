using System.Text.Json.Serialization;

namespace PandaVaultClient.Dtos;

/// <summary>Key/value configuration entry returned by the PandaVault service.</summary>
public class PandaVaultConfig
{
    /// <summary>Configuration key to inject into IConfiguration.</summary>
    [JsonPropertyName("key")]
    public required string Key { get; set; }

    /// <summary>Configuration value associated with the key.</summary>
    [JsonPropertyName("value")]
    public required string Value { get; set; }
}

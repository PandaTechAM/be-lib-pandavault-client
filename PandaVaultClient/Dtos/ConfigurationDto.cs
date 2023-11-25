using System.Diagnostics.CodeAnalysis;

namespace PandaVaultClient.Dtos;

[SuppressMessage("ReSharper", "InconsistentNaming")]
public class ConfigurationDto
{
    public string key { get; set; } = null!;
    public string value { get; set; } = null!;
}
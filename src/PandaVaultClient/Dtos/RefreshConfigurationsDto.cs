namespace PandaVaultClient.Dtos;

public class RefreshConfigurationsDto
{
    public string Key { get; set; } = null!;
    public string OldValue { get; set; } = null!;
    public string UpdatedValue { get; set; } = null!;
    public bool Success { get; set; }
    public string Message { get; set; } = null!;
}
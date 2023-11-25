namespace PandaVaultClient.Test;

public class Startup : IHostedService
{
    private readonly PandaVault _vault;

    public Startup(PandaVault vault)
    {
        _vault = vault;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _vault.SetConfigurationsAsync();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
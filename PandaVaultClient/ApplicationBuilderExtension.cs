using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace PandaVaultClient;

public static class ApplicationBuilderExtension
{
    public static void RunPandaVaultClient(this WebApplication app)
    {
        var service = app.Services.GetRequiredService<PandaVault>();
        service.SetConfigurationsAsync().GetAwaiter().GetResult();
    }
}
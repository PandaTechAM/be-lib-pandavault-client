using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PandaVaultClient;

public static class PandaVaultApi
{
    public static void MapPandaVaultApi(this WebApplication app)
    {
        app.MapGet("/configurations",
                (PandaVault vault, [FromHeader] string secret) => vault.GetAllConfigurations(secret))
            .WithTags("Above Board");

        app.MapPatch("/configurations",
                async (PandaVault vault, [FromHeader] string secret) => await vault.RefreshConfigurationsAsync(secret))
            .WithTags("Above Board");
    }
}
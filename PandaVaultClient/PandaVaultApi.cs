using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace PandaVaultClient;

public static class PandaVaultApi
{
    public static void MapPandaVaultApi(this WebApplication app)
    {
        app.MapPatch("/configurations",
                (IConfiguration configuration) => ((IConfigurationRoot)configuration).Reload())
            .WithTags("Above Board");
    }
}
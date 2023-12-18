using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PandaVaultClient;

public static class PandaVaultApi
{
    public static WebApplication MapPandaVaultApi(this WebApplication app)
    {
        app.MapGet("/above-board/configurations", ([FromServices] PandaVaultConfigurationProvider vault, [FromHeader] string secret) =>
        vault.GetAllConfigurations(secret)).WithTags("Above Board");
        return app;
    }
    
    public static WebApplication MapPandaVaultApi(this WebApplication app, string groupName)
    {
        app.MapGet("/configurations", ([FromServices] PandaVaultConfigurationProvider vault, [FromHeader] string secret) =>
        vault.GetAllConfigurations(secret)).WithTags("Above Board").WithGroupName(groupName);
        return app;
    }
}
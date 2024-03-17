using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PandaVaultClient.Dtos;

namespace PandaVaultClient;

public static class PandaVaultApi
{
    public static WebApplication MapPandaVaultApi(this WebApplication app)
    {
        app.MapGet("/above-board/configurations", ([FromServices] PandaVaultConfigurationProvider vault, [FromHeader] string secret) =>
        vault.GetAllConfigurations(secret))
        .Produces<List<AllConfigurationsDto>>();
        return app;
    }
    
    public static WebApplication MapPandaVaultApi(this WebApplication app, string tagName)
    {
        app.MapGet("/configurations",
            ([FromServices] PandaVaultConfigurationProvider vault, [FromHeader] string secret) =>
                vault.GetAllConfigurations(secret))
            .Produces<List<AllConfigurationsDto>>()
            .WithTags(tagName);
        return app;
    }
    
    public static WebApplication MapPandaVaultApi(this WebApplication app, string tagName, string groupName)
    {
        app.MapGet("/configurations",
            ([FromServices] PandaVaultConfigurationProvider vault, [FromHeader] string secret) =>
                vault.GetAllConfigurations(secret))
            .Produces<List<AllConfigurationsDto>>()
            .WithTags(tagName)
            .WithGroupName(groupName);
        return app;
    }
}
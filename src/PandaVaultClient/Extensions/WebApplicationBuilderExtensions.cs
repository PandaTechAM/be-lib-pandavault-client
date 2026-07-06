using Microsoft.AspNetCore.Builder;

namespace PandaVaultClient.Extensions;

/// <summary>Extension methods for wiring PandaVault into a <see cref="WebApplicationBuilder" />.</summary>
public static class WebApplicationBuilderExtensions
{
    /// <summary>
    ///     Fetch PandaVault configurations on startup and merge them into the builder's configuration,
    ///     validating that every required key is present.
    /// </summary>
    public static WebApplicationBuilder AddPandaVault(this WebApplicationBuilder builder)
    {
        var pandaVaultConfigs = PandaVaultHttpClient.FetchConfigurationsAsync()
            .Result;


        builder.Configuration.AddPandaVaultConfigurations(pandaVaultConfigs);
        return builder;
    }
}

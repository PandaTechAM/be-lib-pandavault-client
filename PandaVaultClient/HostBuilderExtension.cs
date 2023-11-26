using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;

namespace PandaVaultClient;

[SuppressMessage("Major Code Smell", "S3928:Parameter names used into ArgumentException constructors should match an existing one ")]
public static class HostBuilderExtension
{
    public static void RegisterPandaVault(this IServiceCollection services)
    {
        var url = Environment.GetEnvironmentVariable("PANDAVAULT_URL");
        if (string.IsNullOrEmpty(url))
        {
            throw new ArgumentNullException(nameof(url),
                "Environment variable PANDAVAULT_URL is not set");
        }
        
        var secret = Environment.GetEnvironmentVariable("PANDAVAULT_SECRET");
        if (string.IsNullOrEmpty(secret))
        {
            throw new ArgumentNullException(nameof(secret),
                "Environment variable PANDAVAULT_SECRET is not set");
        }
        services.AddSingleton<PandaVault>();
    }
}
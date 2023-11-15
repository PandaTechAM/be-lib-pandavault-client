using Microsoft.Extensions.Configuration;

namespace PandaVaultClient
{
    public static class PandaVaultExtensions
    {
        public static IConfigurationBuilder AddPandaVault(this IConfigurationBuilder configurationBuilder)
        {
            var pandaVaultUrl = Environment.GetEnvironmentVariable("PANDA_VAULT_URL");
            var pandaVaultSecret = Environment.GetEnvironmentVariable("PANDA_VAULT_SECRET");

            if (string.IsNullOrEmpty(pandaVaultUrl))
            {
#pragma warning disable S3928
                throw new ArgumentNullException(nameof(pandaVaultUrl),
                    "Environment variable PANDA_VAULT_URL is not set");
#pragma warning restore S3928
            }

            if (string.IsNullOrEmpty(pandaVaultSecret))
            {
#pragma warning disable S3928
                throw new ArgumentNullException(nameof(pandaVaultSecret),
                    "Environment variable PANDA_VAULT_URL is not set");
#pragma warning restore S3928
            }

            configurationBuilder.Add(new PandaVaultConfigurationSource()
            {
                Url = pandaVaultUrl,
                Secret = pandaVaultSecret
            });
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddPandaVault(
            this IConfigurationBuilder configurationBuilder,
            string uri, string secret)
        {
            configurationBuilder.Add(new PandaVaultConfigurationSource { Url = uri, Secret = secret });
            return configurationBuilder;
        }

        public static IConfigurationBuilder AddPandaVault(this IConfigurationBuilder builder,
            Action<PandaVaultConfigurationSource>? configureSource)
            => builder.Add(configureSource);
    }
}
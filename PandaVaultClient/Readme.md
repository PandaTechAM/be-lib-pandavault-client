# PandaVaultClient

PandaVaultClient is an internal NuGet package designed to retrieve and apply configurations from an PandaVault service,
to your .NET projects. This package offers a seamless way to integrate and manage configurations securely within your
applications.

## Features

- **Configuration Retrieval**: Fetches configuration data from an internal service using HTTP requests with secret
  authentication.
- **Configuration Application**: Applies the fetched configuration data to your .NET projects using
  Microsoft's `Microsoft.Extensions.Configuration`.
- **Configuration Refresh**: Refreshes the configuration data at a specified interval, allowing for dynamic
  configuration changes without the need to restart your application.

## Installation

PandaVaultClient can be installed via NuGet Package Manager or by adding the following package reference to your
project:

```bash
dotnet add package Pandatech.PandaVaultClient
```

## Usage

### Basic Configuration Retrieval

To use PandaVaultClient within your project, first, ensure you have the necessary environment variables set for the
configuration service URL and secret:

```csharp
// Set environment variables
Environment.SetEnvironmentVariable("PANDAVAULT_URL", "Your_PandaVault_URL");
Environment.SetEnvironmentVariable("PANDAVAULT_SECRET", "Your_PandaVault_Secret");
```

Then, within your program.cs register the nuget package:
After registering on application startup, you can call `PandaVault.SetConfigurationsAsync()` to fetch and apply the
configurations to your project:
Additionally, you can map endpoints for  `PandaVault.GetAllConfigurations()` or `RefreshConfigurationsAsync` to retrieve
the configurations.

### Program.cs

```csharp
using Microsoft.AspNetCore.Mvc;
using PandaVaultClient;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterPandaVault();
builder.Services.AddHostedService<Startup>();

var app = builder.Build();

app.MapGet("/configurations", (PandaVault vault, [FromHeader] string secret) => vault.GetAllConfigurations(secret));
app.MapPut("/configurations",
    async (PandaVault vault, [FromHeader] string secret) => await vault.RefreshConfigurationsAsync(secret));

app.Run();
```

### Startup.cs

```csharp

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
```

## License

Pandatech.PandaVaultClient is licensed under the MIT License.
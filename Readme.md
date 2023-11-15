# PandaVaultClient

PandaVaultClient is an internal NuGet package designed to retrieve and apply configurations from an PandaVault service,
to your .NET projects. This package offers a seamless way to integrate and manage configurations securely within your
applications.

## Features

- **Configuration Retrieval**: Fetches configuration data from an internal service using HTTP requests with secret
  authentication.
- **Configuration Application**: Applies the fetched configuration data to your .NET projects using
  Microsoft's `Microsoft.Extensions.Configuration`.

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
Environment.SetEnvironmentVariable("PANDA_VAULT_URL", "Your_Panda_Vault_URL");
Environment.SetEnvironmentVariable("PANDA_VAULT_SECRET", "Your_Panda_Vault_Secret");
```

Then, within your application program.cs or where configuration is set up:

```csharp
using PandaVaultConfigurationProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddPandaVault();

var app = builder.Build();

app.Run();
```

## License

Pandatech.PandaVaultClient is licensed under the MIT License.
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
- **Configuration Validation**: Validates that there are no required configurations that have been left blank. If there
  are, the application will not start. To require some configuration to be required, add `"*"` as the value for the key.

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

After setting the environment variables, register the PandaVaultClient service within your project's `Program.cs` file:

### Program.cs

```csharp
using PandaVaultClient;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterPandaVault(); // Registering PandaVaultClient

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.RunPandaVaultClient(); // Running PandaVaultClient on startup
app.MapPandaVaultApi(); // Mapping PandaVaultClient endpoints

app.Run();
```

## License

Pandatech.PandaVaultClient is licensed under the MIT License.
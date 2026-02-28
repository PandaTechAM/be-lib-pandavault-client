# Pandatech.PandaVaultClient

Secure configuration retrieval client for PandaVault service integration in .NET 8+ applications with automatic configuration injection and validation.

## Installation

```bash
dotnet add package Pandatech.PandaVaultClient
```

## Quick Start

### 1. Set Environment Variables

```bash
export PANDAVAULT_URL="https://vault.yourcompany.com"
export PANDAVAULT_SECRET="your-vault-secret"
```

Or in `appsettings.json` (not recommended for secrets):
```json
{
  "PANDAVAULT_URL": "https://vault.yourcompany.com",
  "PANDAVAULT_SECRET": "your-vault-secret"
}
```

### 2. Register in Program.cs

```csharp
using PandaVaultClient.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Fetch and apply PandaVault configurations
builder.AddPandaVault();

var app = builder.Build();
app.Run();
```

### 3. Use Configurations

**Direct access:**
```csharp
var rabbitMqHost = builder.Configuration["RabbitMQSettings:HostName"];
```

**Strongly-typed options:**
```csharp
public class RabbitMQSettings
{
    public required string HostName { get; set; }
    public required string ExchangeName { get; set; }
}

// Register
builder.Services.Configure<RabbitMQSettings>(
    builder.Configuration.GetSection("RabbitMQSettings"));

// Inject
public class MyService
{
    private readonly RabbitMQSettings _settings;
    
    public MyService(IOptions<RabbitMQSettings> options)
    {
        _settings = options.Value;
    }
}
```

## Features

✅ **Automatic configuration retrieval** - Fetches configs from PandaVault on startup  
✅ **Environment variable authentication** - Secure secret management  
✅ **Configuration validation** - Ensures required configs are set  
✅ **IConfiguration integration** - Works with Microsoft.Extensions.Configuration  
✅ **Startup failure on missing configs** - Prevents runtime errors

## Required Configuration Validation

Mark configurations as required by setting their value to `"**"` in your local `appsettings.json`:

```json
{
  "DatabaseConnection": "**",
  "RabbitMQSettings:HostName": "**"
}
```

If PandaVault doesn't provide these configurations, the application **will not start** and throws:

```
InvalidOperationException: Configuration key 'DatabaseConnection' is not configured in the PandaVault.
```

## How It Works

1. **Reads environment variables** (`PANDAVAULT_URL`, `PANDAVAULT_SECRET`)
2. **Validates URL and secret**
3. **Fetches configurations** via HTTP GET to `/api/v1/vault-configs`
4. **Merges into IConfiguration** - PandaVault configs override appsettings.json
5. **Validates required configs** - Ensures no `"**"` placeholders remain

## API Endpoint

PandaVault must expose:

```
GET /api/v1/vault-configs
Headers:
  secret: {PANDAVAULT_SECRET}

Response:
[
  { "key": "DatabaseConnection", "value": "Server=..." },
  { "key": "RabbitMQSettings:HostName", "value": "rabbitmq.local" }
]
```

## Error Handling

**Missing environment variables:**
```
ArgumentNullException: PANDAVAULT_URL environment variable is not set
```

**Invalid URL:**
```
ArgumentNullException: PANDAVAULT_URL is not valid. Url: {url}
```

**HTTP errors:**
```
HttpRequestException: Failed to fetch configurations. Status Code: 401
```

**Wrong secret:**
```
Console: The secret is wrong or there is no configurations set
Returns: Empty list (application continues with local config)
```

## Security Considerations

⚠️ **Do not hardcode secrets** - Always use environment variables  
⚠️ **Use HTTPS** - PandaVault URL should use https:// (http:// is allowed but not recommended)  
⚠️ **Rotate secrets regularly** - Update PANDAVAULT_SECRET periodically  
⚠️ **Validate configurations** - Use the `"**"` pattern for critical configs

## Advanced Usage

### Manual Configuration Fetch

```csharp
var configs = await PandaVaultHttpClient.FetchConfigurationsAsync();

foreach (var config in configs)
{
    Console.WriteLine($"{config.Key} = {config.Value}");
}
```

### Custom Configuration Processing

```csharp
var builder = WebApplication.CreateBuilder(args);

// Fetch configs manually
var vaultConfigs = await PandaVaultHttpClient.FetchConfigurationsAsync();

// Apply with custom logic
foreach (var config in vaultConfigs)
{
    if (config.Key.StartsWith("Secrets:"))
    {
        // Handle secrets differently
        builder.Configuration[config.Key] = DecryptValue(config.Value);
    }
    else
    {
        builder.Configuration[config.Key] = config.Value;
    }
}
```

## Troubleshooting

**Issue:** Application starts but configurations are not applied  
**Solution:** Ensure `AddPandaVault()` is called before accessing configurations

**Issue:** `The secret is wrong or there is no configurations set`  
**Solution:** Verify PANDAVAULT_SECRET matches the vault's expected secret

**Issue:** `Configuration key 'X' is not configured in the PandaVault`  
**Solution:** Either add the config to PandaVault or remove the `"**"` placeholder

## License

MIT
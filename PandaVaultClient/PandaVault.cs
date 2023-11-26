using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using PandaVaultClient.Dtos;

namespace PandaVaultClient;

[SuppressMessage("ReSharper", "ConvertToPrimaryConstructor")]
public class PandaVault
{
    private readonly IConfiguration _configuration;
    private readonly string _environment;
    private readonly string _pandaVaultSecret;

    public PandaVault(IConfiguration configuration)
    {
        _configuration = configuration;
        _environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
        _pandaVaultSecret = Environment.GetEnvironmentVariable("PANDAVAULT_SECRET")!;
    }
    
    public async Task SetConfigurationsAsync()
    {
        var configurations = await HttpHelper.FetchConfigurationsAsync();

        foreach (var config in configurations)
        {
            if (string.IsNullOrEmpty(_configuration[config.key]))
            {
                throw new ArgumentException(
                    $"Key: `{config.key}`'s value is not found in appsettings.{_environment}.json");
            }

            if (string.IsNullOrEmpty(config.value))
            {
                throw new ArgumentException($"Key: {config.key} value is not found in PandaVault");
            }

            _configuration[config.key] = config.value;
        }

        ValidateConfigurations();
    }

    private void ValidateConfigurations()
    {
        var emptyConfigs =
            _configuration.AsEnumerable().Where(kvp => kvp.Value == "-").Select(kvp => kvp.Key).ToList();

        if (emptyConfigs.Count == 0) return;

        var missingConfigs = string.Join(", ", emptyConfigs);
        throw new ArgumentException(
            $"The following configurations are empty in appsettings.{_environment}.json: {missingConfigs}");
    }

    public List<AllConfigurationsDto> GetAllConfigurations(string pandaVaultSecret)
    {
        if (pandaVaultSecret != _pandaVaultSecret)
        {
            throw new ArgumentException("PandaVault secret is not correct");
        }

        var configurations = _configuration.AsEnumerable().ToList();

        return configurations
            .Select(configuration => new AllConfigurationsDto { Key = configuration.Key, Value = configuration.Value })
            .ToList();
    }

    public async Task<List<RefreshConfigurationsDto>> RefreshConfigurationsAsync(string pandaVaultSecret)
    {
        if (pandaVaultSecret != _pandaVaultSecret)
        {
            throw new ArgumentException("PandaVault secret is not correct");
        }

        var configurations = await HttpHelper.FetchConfigurationsAsync();

        var refreshConfigsDto = new List<RefreshConfigurationsDto>();

        foreach (var config in configurations)
        {
            var refreshConfigDto = new RefreshConfigurationsDto
            {
                Key = config.key
            };
            if (string.IsNullOrEmpty(_configuration[config.key]))

            {
                refreshConfigDto.OldValue = "null";
                refreshConfigDto.UpdatedValue = "null";
                refreshConfigDto.Success = false;
                refreshConfigDto.Message =
                    $"Key: `{config.key}`'s value is not found in appsettings.{_environment}.json";
                refreshConfigsDto.Add(refreshConfigDto);
                continue;
            }

            if (string.IsNullOrEmpty(config.value))
            {
                refreshConfigDto.OldValue = _configuration[config.key]!;
                refreshConfigDto.UpdatedValue = _configuration[config.key]!;
                refreshConfigDto.Success = false;
                refreshConfigDto.Message = $"Key: {config.key} value is not found in PandaVault";
                refreshConfigsDto.Add(refreshConfigDto);
                continue;
            }

            if (_configuration[config.key] == config.value)
            {
                continue;
            }

            refreshConfigDto.OldValue = _configuration[config.key]!;
            _configuration[config.key] = config.value;
            refreshConfigDto.UpdatedValue = _configuration[config.key]!;
            refreshConfigDto.Success = true;
            refreshConfigDto.Message = "Updated configuration successfully";
            refreshConfigsDto.Add(refreshConfigDto);
        }

        if (refreshConfigsDto.Count == 0)
        {
            refreshConfigsDto.Add(new RefreshConfigurationsDto
            {
                Key = "null",
                OldValue = "null",
                UpdatedValue = "null",
                Success = true,
                Message = "No configurations has been updated. All configurations are up to date."
            });
        }

        return refreshConfigsDto;
    }
}
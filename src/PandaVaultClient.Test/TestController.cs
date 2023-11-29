using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace PandaVaultClient.Test;

[ApiController]
public class TestController
{
    public TestController(IOptions<RabbitMQSettings> rabbitMqSettings, IConfiguration configuration)
    {
        _rabbitMqSettings = rabbitMqSettings.Value;
        _configuration = configuration;
    }

    private readonly RabbitMQSettings _rabbitMqSettings;
    private readonly IConfiguration _configuration;
    
    [HttpGet("rabbitmqsettings")]
    public RabbitMQSettings GetRabbitMQSettings()
    {
        return _rabbitMqSettings;
    }
}
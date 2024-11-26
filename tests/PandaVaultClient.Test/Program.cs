using PandaVaultClient;
using PandaVaultClient.Extensions;
using PandaVaultClient.Test;

var builder = WebApplication.CreateBuilder(args);


builder.AddPandaVault();

builder.Services.AddEndpointsApiExplorer();

builder.Services.Configure<RabbitMQSettings>(builder.Configuration.GetSection("RabbitMQSettings")); //for test only

builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();
app.Run();
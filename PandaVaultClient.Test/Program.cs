using Microsoft.AspNetCore.Mvc;
using PandaVaultClient;
using PandaVaultClient.Test;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterPandaVault();
builder.Services.AddHostedService<Startup>();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/configurations", (PandaVault vault, [FromHeader] string secret) => vault.GetAllConfigurations(secret));
app.MapPut("/configurations",
    async (PandaVault vault, [FromHeader] string secret) => await vault.RefreshConfigurationsAsync(secret));

app.Run();
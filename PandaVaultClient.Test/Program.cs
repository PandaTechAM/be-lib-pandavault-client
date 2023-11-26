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
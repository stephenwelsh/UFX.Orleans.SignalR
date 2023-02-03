using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Server;
using UFX.Orleans.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder
    .Host
    .UseOrleans(siloBuilder => siloBuilder
        .UseLocalhostClustering()
        .AddMemoryGrainStorage(Constants.StorageName)
        .UseInMemoryReminderService()
        .AddSignalRBackplane(x => x.GrainCleanupPeriod = TimeSpan.FromMinutes(1))
    );

builder.Services.AddSignalR();

var app = builder.Build();

app.MapHub<ChatHub>("/chat");

app.Run();
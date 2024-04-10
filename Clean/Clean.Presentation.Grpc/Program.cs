using Clean.Database;
using Clean.Infrastructure;
using Clean.Presentation.Grpc.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddDatabase();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.MapGrpcService<InvoicesService>();

app.Run();
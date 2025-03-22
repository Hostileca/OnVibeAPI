using Application.DI;
using DataAccess.DI;
using Infrastructure.Hangfire.DI;
using Infrastructure.SignalR.DI;
using OnVibeAPI.DI;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration);
builder.Services.AddInfrastructureHangfire(builder.Configuration);
builder.Services.AddInfrastructureSignalR();
builder.Services.AddApplication();
builder.Services.AddPresentation(builder.Configuration);

var app = builder.Build();

app.StartApplication();
using System;
using System.ServiceProcess;
using System.Diagnostics;
using System.Threading;
using pkb.winutils;
using System.Security.Principal;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.MapGet("/list", () =>
{
    return WinServiceController.GetAllWinService();
}).WithName("GetListOfServices").WithOpenApi();

app.MapGet("/list/{PartialServiceID}", (String PartialServiceID) =>
{
    return WinServiceController.GetMatchingWinService(PartialServiceID);
}).WithName("SearchServices").WithOpenApi();

app.MapGet("/start/{ServiceID}", (String ServiceID) =>
{
    return WinServiceController.StartWinService(ServiceID);
}).WithName("StartService").WithOpenApi();

app.MapGet("/stop/{ServiceID}", (String ServiceID) =>
{
    return WinServiceController.StopWinService(ServiceID);
}).WithName("StopService").WithOpenApi();

app.Run();




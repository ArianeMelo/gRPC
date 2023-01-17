using gRPC.Poc.Repository.IoC;
using gRPC.Poc.Services;
using gRPC.Poc.Services.gRPC;
using System.ComponentModel;

var builder = WebApplication.CreateBuilder(args);
InjecaoDeDependencia.RegistrarInjecaoDependencia(builder.Services);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<UsuarioGrpcService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();

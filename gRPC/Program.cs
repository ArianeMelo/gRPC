using gRPC.API.Services.gRPC;
using gRPC.Poc.Protos;
using static gRPC.API.Services.gRPC.UsuarioService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUsuarioService , UsuarioService>();  
builder.Services.AddGrpcClient<UsuarioProto.UsuarioProtoClient>(options =>
{
    options.Address = new Uri(builder.Configuration["GrpcPoc"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using GRPCForDummies.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddGrpc(); // Add gRPC support

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5001, listenOptions =>
    {
        listenOptions.UseHttps();  // Ensure HTTPS is enabled
        listenOptions.Protocols = HttpProtocols.Http2; // Enforce HTTP/2
    });
});

var app = builder.Build();


app.UseRouting(); 

app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GreetService>(); // Register gRPC service
    endpoints.MapGet("/", () => "Use a gRPC client to communicate.");
});

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

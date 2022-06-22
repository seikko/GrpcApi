using Product.Api.Services;
using ProductAPI.Configuration;
using ProductAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Additional configuration is required to successfully run gRPC on macOS.
// For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

// Add services to the container.
builder.Services.AddGrpc();
var app = builder.Build();
builder.Services.AddOptions(); 
IServiceCollection service = new ServiceCollection();

IConfiguration Configuration = app.Configuration; 
service.Configure<Settings>(Configuration.GetSection("ProductSettings"));


service.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));
service.AddCors(options => {
    options.AddPolicy("cors", policy => {
        policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin().WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
    });
});
app.MapGrpcService<ProductService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
app.Run();

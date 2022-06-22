using Grpc.Net.Client;
using ProductClient;

var channel = GrpcChannel.ForAddress("http://localhost:5248");//grpc service
var client = new Product.ProductClient(channel);
var reply =   client.AddProduct(new AddProductRequest { Id = "12345", Name = "Test Verisi",Description = "Test Verisidir.",Price = 50,AvailableStock = 45 });
Console.WriteLine("Ok");
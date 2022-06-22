using Grpc.Core;
using MongoDB.Bson;
using ProductAPI.Repositories;

namespace Product.Api.Services
{
    public class ProductService : Product.ProductBase
    {
        private readonly IMongoRepository<ProductAPI.Models.Product> _repository;

        public ProductService(IMongoRepository<ProductAPI.Models.Product> repository)
        {
            _repository = repository;
        }

        public async override Task<ProductResponse> AddProduct(AddProductRequest request, ServerCallContext context)
        {
            if (request != null)
            {
                var product = new ProductAPI.Models.Product()
                {
                    AvailableStock = request.AvailableStock,
                    Description = request.Description,
                    Name = request.Name,
                    Price = request.Price
                };
                await _repository.InsertOneAsync(product);
                return new ProductResponse()
                {
                    Price = product.Price,
                    Name = product.Name,
                    Description = product.Description,
                    AvailableStock = product.AvailableStock,
                    Id = request.Id,
                };
            }
            return null;
        }
        public async  override Task<ProductResponse> GetItemById(ProductRequest request, ServerCallContext context)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                context.Status = new Status(StatusCode.FailedPrecondition, $"Id must be not empty (received {request.Id})");
                // _logger.LogInformation("Begin grpc call CatalogService.GetItemById for product code {Id}", request.Id);

                return null;
            }
            var item = await _repository.FindOneAsync(x => x.Id == ObjectId.Parse(request.Id));
            if (item == null)
            {
                context.Status = new Status(StatusCode.FailedPrecondition, $"Product not found {request.Id}");
                return null;
            }
            return new ProductResponse
            {
                Price = item.Price,
                Id = item.Id.ToString(),
                Name = item.Name,
                AvailableStock = item.AvailableStock,
                Description = item.Description
            };
        }
    }
}


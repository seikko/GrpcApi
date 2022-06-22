using MongoDB.Bson;

namespace ProductAPI.Models
{
    public class Product : IDocument
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int AvailableStock { get; set; }
    }
}

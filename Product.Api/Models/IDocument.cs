using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProductAPI.Models
{
    public interface IDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        ObjectId Id { get; set; }
    }
}

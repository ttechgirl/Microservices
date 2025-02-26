using Microsoft.AspNetCore.Routing.Constraints;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

//Entity
namespace CatalogService.Models
{
    public class Items
    {
        [BsonId]
        //[BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}
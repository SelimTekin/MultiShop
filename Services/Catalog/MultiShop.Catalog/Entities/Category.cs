using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MultiShop.Catalog.Entities
{
	public class Category
	{
        // CategoryID'nin id olduğu belirtmek için bu 2 attribute yazmamız lazım
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}

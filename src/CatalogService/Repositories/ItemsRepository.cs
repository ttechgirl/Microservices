using CatalogService.Interfaces;
using CatalogService.Models;
using MongoDB.Driver;

namespace CatalogService.Repositories
{
    public class ItemsRepository : IItemsRepository
    {
        private const string collectionName = "Items"; //set table name
        private readonly IMongoCollection<Items>? dbCollection;
        private readonly FilterDefinitionBuilder<Items> filterBuilder = Builders<Items>.Filter;

        public ItemsRepository()
        {

            var mongoClient = new MongoClient("mongodb://localhost:27017"); //Db connection string
            var database = mongoClient.GetDatabase("Catalog"); //Database name
            dbCollection = database.GetCollection<Items>(collectionName); //get table name is Items
        }

        public async Task<IReadOnlyCollection<Items>> GetAllAsync()
        {
            return await dbCollection.Find(filterBuilder.Empty).ToListAsync() ?? [];
        }

        public async Task<Items> GetAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The Guid cannot be empty.", nameof(id));

            var filter = filterBuilder.Eq(entity => entity.Id, id); // Define the filter
            return await dbCollection.Find(filter).FirstOrDefaultAsync(); //checks the item table and filter the data with id (primary key)
        }

        public async Task<ItemsDto> CreateAsync(CreateItemsRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var item = new Items
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                CreatedDate = DateTimeOffset.UtcNow
            };
            var itemMap = item.DtoMap();
            await dbCollection.InsertOneAsync(item); //insert data into the table 
            return itemMap;
        }

        public async Task<ItemsDto> UpdateAsync(UpdateItemsRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Id == Guid.Empty)
                throw new ArgumentException("The Guid cannot be empty.", nameof(request.Id));

            // Update listed properties
            UpdateDefinition<Items> update = Builders<Items>.Update
            .Set(x => x.Name, request.Name)
            .Set(x => x.Price, request.Price)
            .Set(x => x.Description, request.Description)
            .Set(x => x.ModifiedDate, DateTimeOffset.Now);

            var filter = filterBuilder.Eq(entity => entity.Id, request.Id); // Define the filter
            await dbCollection.UpdateOneAsync(filter, update); //effect update 
            return request.UpdateMap();

        }

        public async Task RemoveAsync(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("The Guid cannot be empty.", nameof(id));

            var filter = filterBuilder.Eq(ex => ex.Id, id); // Define the filter
            await dbCollection.DeleteOneAsync(filter); //effect delete 
        }
    }
}
using Config.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Database.Model.User.CustomIndices;
using System;

// convert the fomr API -> DB models
// This is where I want validation to take place
namespace UserCustomIndices.Services
{
    public class CustomIndexService : ICustomIndexService
    {
        private readonly IMongoCollection<CustomIndex> customIndexCollection;

        public CustomIndexService(IUserInfoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            customIndexCollection = database.GetCollection<CustomIndex>(settings.CustomIndexCollectionName);
        }

        public CustomIndex Get(string id) => customIndexCollection.Find(customIndex => customIndex.Id == id).FirstOrDefault();

        public void Create(CustomIndex customIndex)
        {
            customIndexCollection.InsertOne(customIndex);
        }

        public CustomIndex Update(Guid clientId, CustomIndex customIndexUpdated)
        {
            // verify client owns customIndex

            customIndexCollection.ReplaceOne(customIndex => customIndex.Id == customIndexUpdated.Id, customIndexUpdated);

            return customIndexUpdated;
        }

        public void Remove(CustomIndex bookIn) => customIndexCollection.DeleteOne(book => book.Id == bookIn.Id);

        public void Remove(string id) => customIndexCollection.DeleteOne(book => book.Id == id);

        public List<CustomIndex> Get(Guid userid)
        {
            throw new NotImplementedException();
        }
    }
}
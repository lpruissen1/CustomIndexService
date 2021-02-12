using Config.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Database.Model.User.CustomIndices;
using System;
using Database.Model.User;
using MongoDB.Bson;

// convert the fomr API -> DB models
// This is where I want validation to take place
namespace UserCustomIndices.Services
{
    public class CustomIndexService : ICustomIndexService
    {
        private readonly IMongoCollection<CustomIndex> customIndexCollection;
        private readonly IMongoCollection<UserIndices> userIndexCollection;

        public CustomIndexService(IUserInfoDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            customIndexCollection = database.GetCollection<CustomIndex>(settings.CustomIndexCollectionName);
            userIndexCollection = database.GetCollection<UserIndices>(settings.UserIndicesCollectionName);
        }
        public List<CustomIndex> Get(Guid userId)
        {
            var query = userIndexCollection.Find(entry => entry.userId == userId).FirstOrDefault();

            if (query is null)
                return null;

            var builder = Builders<CustomIndex>.Filter;
            var filter = builder.In(x => x.Id, query.indexId);

            return customIndexCollection.Find(filter).ToList();
        }

        public CustomIndex Get(Guid userId, string indexId)
        {
            var query = userIndexCollection.Find(entry => entry.userId == userId).FirstOrDefault();

            if (query is null)
                return null;

            return customIndexCollection.Find(x => x.Id == indexId).FirstOrDefault();
        }

        public void Create(CustomIndex customIndex, Guid userId)
        {
            var update = Builders<UserIndices>.Update.AddToSet(x => x.indexId, customIndex.Id);

            userIndexCollection.FindOneAndUpdate<UserIndices>(x => x.Id == userId.ToString(), update);
            customIndexCollection.InsertOne(customIndex);
        }

        public CustomIndex Update(Guid clientId, CustomIndex customIndexUpdated)
        {
            
            customIndexCollection.ReplaceOne(customIndex => customIndex.Id == customIndexUpdated.Id, customIndexUpdated);

            return customIndexUpdated;
        }

        public bool Remove(Guid userId, string id)
        {
            var result = customIndexCollection.DeleteOne(book => book.Id == id);

            return result.DeletedCount >= 1;
        }
    }
}
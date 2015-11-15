using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyvia.Data.Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Hyvia.Mongo.DataAccess
{
    public static class AccessDb
    {
         
        static IMongoDatabase _database;

        public static IMongoDatabase Db()
        {
            if (_database != null) return _database;
            IMongoClient client = new MongoClient("mongodb://hyviauser:data123@ds048368.mongolab.com:48368/hyvia-db");
            _database = client.GetDatabase("hyvia-db");
            return _database;
        }

        public static async Task<bool> Insert<T>(T document, string collectionName) 
        {
            var collection = Db().GetCollection<T>(collectionName);
            await collection.InsertOneAsync(document);

            return true;
        }

        public static async Task<IList> GetListOf<TDocument>(SearchData searchData, string fromCollection) where TDocument : BsonDocument
        {
            var collection = Db().GetCollection<BsonDocument>(fromCollection);
            
            FilterDefinition<BsonDocument> filter = new BsonDocument();
            if (searchData != null)
            {
                 filter = Builders<BsonDocument>.Filter.Eq(searchData.SearchField, searchData.SearchValue.First());
                
            }
            
            return await collection.Find(filter).ToListAsync();
            
        }
        public static async Task<IList<T>> GetListOf<T>( string fromCollection)
        {
            var collection = Db().GetCollection<T>(fromCollection);

            return await collection.Find(_ => true).ToListAsync();
        }
        public static async Task<IList<T>> GetListOfWithEntity<T>(IList<SearchData> searchDataList, string fromCollection)
        {
            var collection = Db().GetCollection<T>(fromCollection);
            FilterDefinition<T> filter = new BsonDocument();
            
            if (searchDataList != null && searchDataList.Count > 0)
            {

                foreach (var searchData in searchDataList)
                {
                    string filterQuery = string.Format("/^{0}/i", searchData.SearchValue.First());
                    filter =
                    Builders<T>.Filter.Regex(searchData.SearchField, filterQuery);
                    //  filter = Builders<BsonDocument>.Filter.ElemMatch
                }
            }
            
            var result = await collection.Find(filter).ToListAsync();
            //var result = await collection.Find(_ => true).ToListAsync();
            return result;
        }
    }
}
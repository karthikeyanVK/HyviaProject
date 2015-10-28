using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hyvia.Data.Model;
using Hyvia.Mongo.DataAccess;
using MongoDB.Bson;

namespace Hyvia.API.Command
{
    public class ShopRepository
    {
        public ShopRepository()
        {
            Mapper.CreateMap<ShopCommand, Shop>();
        }
        public async Task<IList> GetShops(string shopName)
        {
            SearchData searchData = null;
            if (!string.IsNullOrEmpty(shopName))
            {
                searchData = new SearchData { SearchField = "ShopName" };
                searchData.SearchValue.Add(shopName);
            }
            return await AccessDb.GetListOf<BsonDocument>(searchData, MongoTables.ShopTableName);
        }
        public async Task<bool> Insertshop(ShopCommand shopCommand)
        {
            var shop =
                Mapper.Map<ShopCommand, Shop>(shopCommand).ToBsonDocument();
            await AccessDb.Insert(shop,MongoTables.ShopTableName);
            return true;
        }
    }
}

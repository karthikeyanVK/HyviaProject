using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Hyvia.Data.Model;
using Hyvia.Mongo.DataAccess;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using System.Collections.Generic;

namespace Hyvia.API.Command
{
    public class ShopRepository
    {
        public ShopRepository()
        {
            Mapper.CreateMap<ShopCommand, Shop>();
            try
            {
                BsonClassMap.RegisterClassMap<Shop>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIdMember(cm.GetMemberMap(x => x.ShopId).SetIdGenerator(StringObjectIdGenerator.Instance));
                });
                 
            }
            catch //Should figureout to check already registered
            { }
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
        public async Task<IList<Shop>> GetShopsByProduct(string productId)
        {
            ProductRepository productRepository = new ProductRepository();
            IList<SearchData> searchDataList = new List<SearchData>();
            SearchData searchData = null;
            if (!string.IsNullOrEmpty(productId))
            {
                searchData = new SearchData
                {
                    SearchField = "ProductId",
                    SearchValue = new List<string> { productId }
                };

                searchDataList.Add(searchData);
            }
            var products = await productRepository.GetProduct(productId);
            var shops = await AccessDb.GetListOf<Shop>(MongoTables.ShopTableName);

            var shopDetails = from shop in shops 
                              join product in products on shop.ShopId equals product.ShopId 
                              select new Shop {
                                  ShopId = shop.ShopId,
                                  Address = shop.Address,
                                  EmailAddress = shop.EmailAddress,
                                  Latitude = shop.Latitude,
                                  Longitude = shop.Longitude,
                                  Pincode = shop.Pincode,
                                  ShopName = shop.ShopName
                              };


            return shopDetails.ToList();

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

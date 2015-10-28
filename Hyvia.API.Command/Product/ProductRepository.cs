using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Hyvia.API.Query;
using Hyvia.Data.Model;
using Hyvia.Mongo.DataAccess;
using MongoDB.Bson;

namespace Hyvia.API.Command
{
    public class ProductRepository
    {
        public ProductRepository()
        {
            Mapper.CreateMap<ProductTypeCommand, ProductType>();
            Mapper.CreateMap<ProductCommand, Product>();
        }

        public async Task<bool> InsertProduct(ProductCommand productCommand)
        {
            var shop =
               Mapper.Map<ProductCommand, Product>(productCommand).ToBsonDocument();
            await AccessDb.Insert(shop, MongoTables.ProductTableName);
            return true;
        }

        public async Task<bool> InsertProductType(ProductTypeCommand productTypeCommand)
        {
            var shop =
               Mapper.Map<ProductTypeCommand, ProductType>(productTypeCommand).ToBsonDocument();
            await AccessDb.Insert(shop, MongoTables.ProductTypeTableName);
            return true;
        }

        public async Task<IList> GetProduct(ProductQuery productQuery)
        {
            IList<SearchData> searchDataList = new List<SearchData>();
            SearchData searchData = null;
           if (!string.IsNullOrEmpty(productQuery.ProductName))
            {
                searchData = new SearchData
                {
                    SearchField = "ProductName",
                    SearchValue = new List<string> {productQuery.ProductName}
                };
                searchDataList.Add(searchData);
            }
           /*if (!string.IsNullOrEmpty(productQuery.ProductTypeId))
          {
              searchData = new SearchData
              {
                  SearchField = "ProductTypeId",
                  SearchValue = new List<string> {productQuery.ProductTypeId}
              };
              searchDataList.Add(searchData);
          }*/
            var result = await AccessDb.GetListOf<BsonDocument>(searchDataList, MongoTables.ProductTableName);
            
            return await AccessDb.GetListOf<BsonDocument>(searchDataList, MongoTables.ProductTableName);
        }


        public async Task<IList> GetProductTypes(string productType)
        {
            SearchData searchData = null;
            if (!string.IsNullOrEmpty(productType))
            {
                searchData = new SearchData { SearchField = "ProductTypeName" };
                searchData.SearchValue.Add(productType);
            }
            return await AccessDb.GetListOf<BsonDocument>(searchData, MongoTables.ProductTypeTableName);
        }
    }

   
}

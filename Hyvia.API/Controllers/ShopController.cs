
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Hyvia.API.Command;
using MongoDB.Bson;
using Cads.VelVenti.Api.Filters;
using Hyvia.Data.Model;
using System.Collections.Generic;

namespace Hyvia.API
{
    [RoutePrefix("api/shops")]
    public class ShopController : ApiController
    {
         private readonly ShopRepository _shopRepository;
         public ShopController()
        {
            _shopRepository = new ShopRepository();
        }
        // GET api/values/5
        [CacheClient]
        public async Task<IList> Get(string shopName)
        {
            var relayResult = await _shopRepository.GetShops(shopName);
            return new[] { relayResult.ToJson() };
        }
        /// <summary>
        /// Return list of shops based on product selected
        /// </summary>
        /// <param name="productId">Sample value ee67c6e1-337d-4e4f-bcb0-70c410141321</param>
        /// <returns></returns>
        [Route("product")]
        [HttpGet]
        [CacheClient]
        public async Task<IList<Shop>> GetShopByProducts(string productId)
        {
            var result = await _shopRepository.GetShopsByProduct(productId);
            return result;
        }

        // POST api/values
        public async Task<bool> Post(ShopCommand shopCommand)
        {

            var relayResult = await _shopRepository.Insertshop(shopCommand);
            return relayResult;
        }
    }
}
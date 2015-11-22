
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Hyvia.API.Command;
using MongoDB.Bson;
using Cads.VelVenti.Api.Filters;

namespace Hyvia.API
{
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
        [Route("product")]
        [HttpGet]
        [CacheClient]
        public async Task<IList> GetShopByProducts(string productId)
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
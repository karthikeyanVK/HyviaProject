
using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Hyvia.API.Command;
using MongoDB.Bson;

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
        public async Task<IList> Get(string shopName)
        {
            var relayResult = await _shopRepository.GetShops(shopName);
            return new[] { relayResult.ToJson() };
        }

        // POST api/values
        public async Task<bool> Post(ShopCommand shopCommand)
        {

            var relayResult = await _shopRepository.Insertshop(shopCommand);
            return relayResult;
        }
    }
}
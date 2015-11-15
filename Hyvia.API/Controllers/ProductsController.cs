
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Hyvia.API.Command;
using Hyvia.API.Query;
using MongoDB.Bson;
using Hyvia.Data.Model;
using Cads.VelVenti.Api.Filters;

namespace Hyvia.API
{
     //[Authorize]
    [RoutePrefix("api/products")]
    public class ProductsController : ApiController
    {
        private readonly ProductRepository _productRepository;
        public ProductsController()
        {
             _productRepository = new ProductRepository();
        }

        // GET api/products/5
        [CacheClient]
        public async Task<IList<Product>> Get([FromUri]ProductQuery productQuery)
        {
            var relayResult = await _productRepository.GetProduct(productQuery);

            return relayResult; 
        }

        // POST api/values
        public async Task<bool> Post(ProductCommand productCommand)
        {

            var relayResult = await _productRepository.InsertProduct(productCommand);
            return relayResult;
        }
        [Route("list")]
        [HttpPost]
        public async Task<bool> Post(IList<ProductCommand> productCommands)
        {
            var relayResult = false;
            foreach (var productCommand in productCommands)
                relayResult = await _productRepository.InsertProduct(productCommand);

            return relayResult;
        }

        [Route("types")]
        [CacheClient]
        public async Task<IList<ProductType>> GetProductTypes(string productName)
        {
            var relayResult = await _productRepository.GetProductTypes(productName);
            return relayResult;
        }

        [Route("types")]
        [HttpPost]
        public async Task<bool> Post(ProductTypeCommand productTypeCommand)
        {
            var relayResult = await _productRepository.InsertProductType(productTypeCommand);
            return relayResult;
        }
        [Route("types/list")]
        [HttpPost]
        public async Task<bool> Post(IList<ProductTypeCommand> productTypeCommands)
        {
            //TODO: to be moved to mongodb list insert
            var relayResult = false;
            foreach (var productTypeCommand in productTypeCommands)
                relayResult = await _productRepository.InsertProductType(productTypeCommand);

            return relayResult;
        }

    }
}
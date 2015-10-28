using System;

namespace Hyvia.API.Command
{
    public class ProductCommand
    {
        public Guid ProductId { get; set; }
        public Guid ProductTypeId { get; set; }
        public Guid ShopId { get; set; }
        public string ProductName { get; set; }
        
    }
}

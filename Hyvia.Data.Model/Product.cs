using System;
 

namespace Hyvia.Data.Model
{
    public class Product
    {
        public Guid ProductTypeId { get; set; }
        public string ProductName { get; set; }
        public Guid ShopId { get; set; }
        public Guid ProductId { get; set; }
    }
}

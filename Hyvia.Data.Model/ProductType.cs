using System;

namespace Hyvia.Data.Model
{
    public class ProductType
    {
        public Guid ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public Guid ParentProductTypeId { get; set; }
    }
}

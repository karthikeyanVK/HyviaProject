using System;

namespace Hyvia.API.Command
{
    public class ProductTypeCommand
    {
        public Guid ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public Guid ParentProductTypeId { get; set; }
    }
}

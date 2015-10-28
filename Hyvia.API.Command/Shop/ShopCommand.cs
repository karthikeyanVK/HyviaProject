using System;

namespace Hyvia.API.Command
{
    public class ShopCommand
    {
        public Guid ShopId { get; set; }
        public string ShopName { get; set; }
        public string EmailAddress { get; set; }
        public string Pincode { get; set; }
        public string Address { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }
}

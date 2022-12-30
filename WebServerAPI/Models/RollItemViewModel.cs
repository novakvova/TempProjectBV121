using System.Collections.Generic;

namespace WebServerAPI.Models
{
    public class RollItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Grams { get; set; }
        public List<string> Description { get; set; }
        public string Img { get; set; }
        public bool Discount { get; set; }
        public int Price { get; set; }
        public int DiscountPrice { get; set; }
    }
}

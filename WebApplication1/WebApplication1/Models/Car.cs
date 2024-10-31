using System.Collections.Generic;

namespace YourNamespace.Models
{
    public class Car
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string MainImageUrl { get; set; }


        public List<string> ImageUrls { get; set; } = new List<string>();
    }
}
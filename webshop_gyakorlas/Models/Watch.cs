using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webshop_gyakorlas.Models
{
    public class Watch
    {
        public int Id { get; set; }

        public Brand Brand { get; set; }
        public int BrandId { get; set; }
        
        public string Model { get; set; }
        public uint Price { get; set; }
        public bool Serviced { get; set; }
        public DateTime? DateOfProduction { get; set; }
        public string Description { get; set; }
    }
}

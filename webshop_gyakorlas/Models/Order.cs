using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webshop_gyakorlas.Helpers;

namespace webshop_gyakorlas.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string WatchesJson { get; set; }
        public string CustomerId { get; set; }

        public DateTime TimeOfOrder { get; set; }
        public bool Packed { get; set; }
        public bool GivenToCourier { get; set; }
        public bool Arrived { get; set; }

        public int Total { get; set; }
    }
}

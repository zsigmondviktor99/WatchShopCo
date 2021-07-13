using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webshop_gyakorlas.Helpers;

namespace webshop_gyakorlas.Models
{
    public class Watch
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter reference number")]
        public string ReferenceNumber { get; set; }

        public Brand Brand { get; set; }

        [Required(ErrorMessage = "Please select brand")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "Please enter model name")]
        public string Model { get; set; }

        [Required(ErrorMessage = "Please enter price")]
        public uint Price { get; set; }

        public bool Serviced { get; set; }

        [Required(ErrorMessage = "Please enter production year")]
        public uint YearOfProduction { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        public string Description { get; set; }

        public string ImagesPath { get; set; }

        public DateTime AddedToShop { get; set; }

        public bool Sold { get; set; }
    }
}

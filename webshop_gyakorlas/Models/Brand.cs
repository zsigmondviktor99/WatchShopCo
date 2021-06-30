using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace webshop_gyakorlas.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter brand name")]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter descreption")]
        public string Descreption { get; set; }

        [Required(ErrorMessage = "Please choose brand logo")]
        public string Logo { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using webshop_gyakorlas.Models;

namespace webshop_gyakorlas.ViewModels
{
    public class BrandViewModel
    {
        [Required(ErrorMessage = "Please enter brand name")]
        [StringLength(maximumLength: 100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter descreption")]
        public string Descreption { get; set; }

        public IFormFile Logo { get; set; }

        public Brand Brand { get; set; }
    }
}

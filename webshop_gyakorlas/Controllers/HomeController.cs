using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using webshop_gyakorlas.Data;
using webshop_gyakorlas.Models;

namespace webshop_gyakorlas.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _context = new ApplicationDbContext();
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Faq()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Brands()
        {
            List<Brand> brands = _context.Brands.ToList().OrderBy(b => b.Name).ToList();
            return View(brands);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

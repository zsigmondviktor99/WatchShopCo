using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using webshop_gyakorlas.Data;
using webshop_gyakorlas.Helpers;
using webshop_gyakorlas.Models;

namespace webshop_gyakorlas.Controllers
{
    public class CheckoutController : Controller
    {
        public ApplicationDbContext _context;

        public CheckoutController()
        {
            _context = new ApplicationDbContext();
        }

        public IActionResult Index()
        {
            //Kifizetes folyamat... elemek torlese ab-bol, e-mail kuldese, stb...
            var cart = SessionHelper.ObjectFromJson<List<Watch>>(HttpContext.Session, "cart");

            Order order = new Order()
            {
                WatchesJson = JsonConvert.SerializeObject(cart),
                CustomerId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value,
                TimeOfOrder = DateTime.Now,
                Packed = false,
                GivenToCourier = false,
                Arrived = false,
                Total = (int)cart.Sum(w => w.Price)
            };

            _context.Orders.Add(order);

            foreach (Watch watch in cart)
            {
                watch.Sold = true;
                _context.Wathces.Update(watch);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }
    }
}

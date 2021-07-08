using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webshop_gyakorlas.Data;
using webshop_gyakorlas.Helpers;
using webshop_gyakorlas.Models;

namespace webshop_gyakorlas.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        public ApplicationDbContext _context;

        public CartController()
        {
            _context = new ApplicationDbContext();
        }

        public IActionResult Index()
        {
            var cart = SessionHelper.ObjectFromJson<List<Watch>>(HttpContext.Session, "cart");
            List<Brand> brandNames = _context.Brands.ToList();

            if (cart == null)
            {
                ViewBag.cart = new List<Watch>();
                ViewBag.total = 0;
            }
            else
            {
                ViewBag.cart = cart;
                ViewBag.total = cart.Sum(c => c.Price);

                //TODO: Kiirja a Brand name-t a tablazatba
                ViewBag.brandNames = brandNames;
            }
            

            return View(ViewBag);
        }

        public int CartContains(int id, List<Watch> cart)
        {
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Id == id)
                {
                    return i;
                }
            }

            return -1;
        }

        public IActionResult AddItemToCart(int id)
        {
            if (SessionHelper.ObjectFromJson<List<Watch>>(HttpContext.Session, "cart") == null)
            {
                //Meg nincs cart
                List<Watch> cart = new List<Watch>();
                cart.Add(_context.Wathces.First(w => w.Id == id));
                SessionHelper.ObjectToJson(HttpContext.Session, "cart", cart);
            }
            else
            {
                //Mar van cart
                List<Watch> cart = SessionHelper.ObjectFromJson<List<Watch>>(HttpContext.Session, "cart");
                if (CartContains(id, cart) != -1)
                {
                    //Cart tartalmazza mar az elemet, ha tobbet is lehetne venni az adott elembol, akkor a db-ot kene novelni, ez most nem jatszik
                }
                else
                {
                    //Cart meg nem tartalmazza az elemet, ezert felvesszul
                    cart.Add(_context.Wathces.First(w => w.Id == id));
                }
                SessionHelper.ObjectToJson(HttpContext.Session, "cart", cart);
            }

            return RedirectToAction("Index", "Cart");
        }

        public IActionResult RemoveItemFromCart(int id)
        {
            List<Watch> cart = SessionHelper.ObjectFromJson<List<Watch>>(HttpContext.Session, "cart");
            int index = CartContains(id, cart);
            
            if (index != -1)
            {
                cart.RemoveAt(index);
            }

            SessionHelper.ObjectToJson(HttpContext.Session, "cart", cart);
            return RedirectToAction("Index", "Cart");
        }
    }
}

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
    public class OrderController : Controller
    {
        public ApplicationDbContext _context;

        public OrderController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize(Roles = RoleName.Admin)]
        public IActionResult Index()
        {
            ViewBag.orders = _context.Orders.ToList();
            return View(ViewBag);
        }

        public IActionResult CustomerOrder(string id)
        {
            //TODO: igy kiderul az ugyfel id-ja az ab-bol (idegen kulcs User objektumra??)
            ViewBag.order = _context.Orders.ToList().Find(o => o.CustomerId == id);
            return View(ViewBag);
        }

        [Authorize(Roles = RoleName.Admin)]
        public IActionResult Details(int id)
        {
            //TODO: Kiirja a Brand name-t a tablazatba
            Order order = _context.Orders.ToList().Find(o => o.Id == id);
            ViewBag.order = order;
            ViewBag.orderedWatches = SessionHelper.WatchesFromOrder<List<Watch>>(order);
            return View(ViewBag);
        }

        public IActionResult Delete(int id)
        {
            Order order = _context.Orders.ToList().Find(o => o.Id == id);

            foreach (Watch watch in SessionHelper.WatchesFromOrder<List<Watch>>(order))
            {
                watch.Sold = false;
                _context.Wathces.Update(watch);
            }

            _context.Orders.Remove(order);
            _context.SaveChanges();

            return RedirectToAction("Index", "Order");
        }
    }
}

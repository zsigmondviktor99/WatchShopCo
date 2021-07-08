using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webshop_gyakorlas.Helpers;

namespace webshop_gyakorlas.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Index()
        {
            //Kifizetes folyamat... elemek torlese ab-bol, e-mail kuldese, stb...
            SessionHelper.TruncateSession(HttpContext.Session);
            return RedirectToAction("Index", "Home");
        }
    }
}

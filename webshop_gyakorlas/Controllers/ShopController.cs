using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using webshop_gyakorlas.Data;
using webshop_gyakorlas.Models;
using webshop_gyakorlas.ViewModels;

namespace webshop_gyakorlas.Controllers
{
    public class ShopController : Controller
    {
        #region Adattagok
        public ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        #endregion

        #region Konstruktor
        public ShopController(IWebHostEnvironment environment)
        {
            _context = new ApplicationDbContext();
            _environment = environment;
        }
        #endregion

        #region Alprogramok
        // GET: ShopController
        public ActionResult Index()
        {
            List<Watch> watches = _context.Wathces.Include(w => w.Brand).ToList();
            List<WatchViewModelForShopIndex> watchViewModelForShopIndices = new List<WatchViewModelForShopIndex>();
            WatchViewModelForShopIndex.WwwrootPath = _environment.WebRootPath;

            foreach (Watch watch in watches)
            {
                watchViewModelForShopIndices.Add(new WatchViewModelForShopIndex() { Watch = watch});
            }
            return View(watchViewModelForShopIndices);
        }

        // GET: ShopController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ShopController/Create
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create()
        {
            WatchViewModel watchViewModel = new WatchViewModel()
            {
                Brands = _context.Brands.ToList().OrderBy(b => b.Name).ToList()
            };

            return View(watchViewModel);
        }

        // POST: ShopController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create(WatchViewModel watchViewModel)
        {
            /*if (ModelState.IsValid)
            {
                string filesPath;

                if (watchViewModel.Images.Count == 0)
                {
                    filesPath = Path.Combine(Path.DirectorySeparatorChar.ToString(), "images", "noimage.png");
                }
                else
                {
                    filesPath = UploadFiles(watchViewModel);
                }

                Watch watch = new Watch()
                {
                    ReferenceNumber = watchViewModel.ReferenceNumber,
                    BrandId = watchViewModel.BrandId,
                    Model = watchViewModel.Model,
                    Price = watchViewModel.Price,
                    Serviced = watchViewModel.Serviced,
                    YearOfProduction = watchViewModel.YearOfProduction,
                    Description = watchViewModel.Description,
                    ImagesPath = filesPath
                };

                _context.Wathces.Add(watch);
                _context.SaveChanges();
            }*/

            string filesPath;

            if (watchViewModel.Images.Count == 0)
            {
                filesPath = Path.Combine(Path.DirectorySeparatorChar.ToString(), "images", "noimage.png");
            }
            else
            {
                filesPath = UploadFiles(watchViewModel);
            }

            Watch watch = new Watch()
            {
                ReferenceNumber = watchViewModel.ReferenceNumber,
                BrandId = watchViewModel.BrandId,
                Model = watchViewModel.Model,
                Price = watchViewModel.Price,
                Serviced = watchViewModel.Serviced,
                YearOfProduction = watchViewModel.YearOfProduction,
                Description = watchViewModel.Description,
                ImagesPath = filesPath
            };

            _context.Wathces.Add(watch);
            _context.SaveChanges();

            return RedirectToAction("Index", "Shop");
        }

        // GET: ShopController/Edit/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ShopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShopController/Delete/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ShopController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private string UploadFiles(WatchViewModel watchViewModel)
        {
            string imagesPath = null;

            if (watchViewModel.Images.Count > 0)
            {
                //TODO: nem jo a szervizeltseg allapot + mappa helyett filet hoz letre
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images", "products");

                imagesPath = /*watchViewModel.Brand.Name + "_" + */watchViewModel.Model + "_" + watchViewModel.ReferenceNumber + "_" + Guid.NewGuid();
                string filesPath = Path.Combine(uploadFolder, imagesPath);

                Directory.CreateDirectory(filesPath);
                //var fileStream = new FileStream(Path.Combine(Path.DirectorySeparatorChar.ToString(), imagesPath), FileMode.Create);
                //var fileStream = new FileStream(filesPath, FileMode.Create);
                FileStream fileStream = null;

                foreach (IFormFile image in watchViewModel.Images)
                {
                    /*using (var fileStream = new FileStream(filesPath, FileMode.Create))
                    {
                        image.CopyTo(fileStream);
                    }*/

                    //fileStream = new FileStream(Path.Combine(filesPath, image.FileName), FileMode.Create);
                    //fileStream = new FileStream(filesPath, FileMode.Create);

                    string filePath = Path.Combine(filesPath, image.FileName);

                    fileStream = System.IO.File.Create(filePath);
                    image.CopyTo(fileStream);

                    /*using (var fileStream = System.IO.File.Create(filesPath))
                    {
                        image.CopyTo(fileStream);
                        fileStream.Flush();
                    }*/

                    fileStream.Close();
                }

                //fileStream.Close();
            }

            return Path.Combine(Path.DirectorySeparatorChar.ToString(), "images", "products", imagesPath);
        }

        private void DeleteFiles(string imagesPath)
        {
            //TODO: Implementalni
            System.IO.File.Delete(_environment.WebRootPath + imagesPath);
        }
        #endregion
    }
}

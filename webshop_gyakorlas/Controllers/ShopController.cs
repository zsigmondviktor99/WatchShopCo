using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            WatchViewModelForShopIndex.WwwrootPath = _environment.WebRootPath;
        }
        #endregion

        #region Alprogramok
        // GET: ShopController
        public ActionResult Index()
        {
            List<Watch> watches = _context.Wathces.Include(w => w.Brand).ToList().OrderByDescending(w => w.AddedToShop).ToList();
            List<WatchViewModelForShopIndex> watchViewModelForShopIndices = new List<WatchViewModelForShopIndex>();

            foreach (Watch watch in watches)
            {
                watchViewModelForShopIndices.Add(new WatchViewModelForShopIndex() { Watch = watch});
            }

            return View(watchViewModelForShopIndices);
        }

        // GET: ShopController/Details/5
        public ActionResult Details(int id)
        {
            Watch watch = _context.Wathces.Include(w => w.Brand).ToList().Find(w => w.Id == id);

            if (watch != null)
            {
                WatchViewModelForShopIndex watchViewModelForShopIndex = new WatchViewModelForShopIndex()
                {
                    Watch = watch
                };

                return View(watchViewModelForShopIndex);
            }
            else
            {
                return View("Error");
            }
        }

        // GET: ShopController/Create
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create()
        {
            WatchViewModel watchViewModel = new WatchViewModel()
            {
                Brands = _context.Brands.Select(b => new SelectListItem {
                    Value = b.Id.ToString(),
                    Text = b.Name
                }).ToList().OrderBy(b => b.Text).ToList()
            };

            return View(watchViewModel);
        }

        // POST: ShopController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create(WatchViewModel watchViewModel)
        {
            string filesPath;

            if (watchViewModel.Images == null)
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
                ImagesPath = filesPath,
                AddedToShop = DateTime.Now,
                Sold = false
            };

            _context.Wathces.Add(watch);
            _context.SaveChanges();

            return RedirectToAction("Index", "Shop");
        }

        // GET: ShopController/Edit/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id)
        {
            Watch watch = _context.Wathces.Include(w => w.Brand).ToList().Find(w => w.Id == id);

            if (watch != null)
            {
                WatchViewModel watchViewModel = new WatchViewModel()
                {
                    Watch = watch,
                    ReferenceNumber = watch.ReferenceNumber,
                    Model = watch.Model,
                    Price = watch.Price,
                    YearOfProduction = watch.YearOfProduction,
                    Description = watch.Description,
                    Serviced = watch.Serviced,
                    Brands = _context.Brands.Select(b => new SelectListItem
                    {
                        Value = b.Id.ToString(),
                        Text = b.Name,
                    }).ToList().OrderBy(b => b.Text).ToList(),
                    SelectedTag = watch.BrandId
                };

                return View(watchViewModel);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: ShopController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(WatchViewModel watchViewModel, int id)
        {
            Watch watch = _context.Wathces.Include(w => w.Brand).ToList().Find(w => w.Id == id);

            if (watchViewModel.Images != null)
            {
                //TODO: kepek update kitalalni
            }

            watch.ReferenceNumber = watchViewModel.ReferenceNumber;
            watch.Model = watchViewModel.Model;
            watch.Price = watchViewModel.Price;
            watch.YearOfProduction = watchViewModel.YearOfProduction;
            watch.Description = watchViewModel.Description;
            watch.Serviced = watchViewModel.Serviced;
            watch.BrandId = watchViewModel.SelectedTag;

            _context.Wathces.Update(watch);
            _context.SaveChanges();

            return RedirectToAction("Index", "Shop");
        }

        // POST: ShopController/Delete/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int id)
        {
            Watch watch = _context.Wathces.Include(w => w.Brand).ToList().Find(w => w.Id == id);

            if (watch != null)
            {
                if (!watch.ImagesPath.Contains("noimage"))
                {
                    //WatchViewModelForShopIndex tipus tulajdonsagaival konnyebb lekerdezni a kepek utjait
                    WatchViewModelForShopIndex watchViewModelForShopIndex = new WatchViewModelForShopIndex()
                    {
                        Watch = watch
                    };
                    DeleteFiles(watchViewModelForShopIndex.ImagesPaths, watch.ImagesPath);
                }
                _context.Wathces.Remove(watch);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Shop");
        }

        private string UploadFiles(WatchViewModel watchViewModel)
        {
            string imagesPath = null;

            if (watchViewModel.Images.Count > 0)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images", "products");

                imagesPath = watchViewModel.Model + "_" + watchViewModel.ReferenceNumber + "_" + Guid.NewGuid();
                string filesPath = Path.Combine(uploadFolder, imagesPath);

                Directory.CreateDirectory(filesPath);
                FileStream fileStream = null;

                foreach (IFormFile image in watchViewModel.Images)
                {
                    string filePath = Path.Combine(filesPath, image.FileName);

                    fileStream = System.IO.File.Create(filePath);
                    image.CopyTo(fileStream);

                    fileStream.Close();
                }

                fileStream.Dispose();
            }

            return Path.Combine(Path.DirectorySeparatorChar.ToString(), "images", "products", imagesPath);
        }

        private void DeleteFiles(string[] imagesPaths, string imagesPath)
        {
            foreach (string path in imagesPaths)
            {
                System.IO.File.Delete(_environment.WebRootPath + path);
            }
            Directory.Delete(_environment.WebRootPath + imagesPath);
        }
        #endregion
    }
}

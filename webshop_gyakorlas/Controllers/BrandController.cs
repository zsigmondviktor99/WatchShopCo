using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class BrandController : Controller
    {
        public ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public BrandController(IHostingEnvironment environment)
        {
            _context = new ApplicationDbContext();
            _environment = environment;
        }

        // GET: BrandController
        public ActionResult Index()
        {
            List<Brand> brands = _context.Brands.ToList().OrderBy(b => b.Name).ToList();
            return View(brands);
        }

        // GET: BrandController/Details/5
        public ActionResult Details(int id)
        {
            Brand brand = _context.Brands.ToList().Find(b => b.Id == id);

            if (brand != null)
            {
                //TODO: Brandet atrakni BrandViewModelbe
                return View(brand);
            }
            else
            {
                return RedirectToAction("Index", "Brand");
            }
        }

        // GET: BrandController/Create
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrandController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Create(BrandViewModel brandViewModel)
        {
            if (ModelState.IsValid)
            {
                string fileName = UploadFile(brandViewModel);

                Brand brand = new Brand()
                {
                    Name = brandViewModel.Name,
                    Descreption = brandViewModel.Descreption,
                    Logo = fileName
                };

                _context.Brands.Add(brand);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Brand");

            /*try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }*/
        }

        private string UploadFile(BrandViewModel brandViewModel)
        {
            string fileName = null;

            if (brandViewModel.Logo != null)
            {
                string uploadFolder = Path.Combine(_environment.WebRootPath, "images", "brands");

                fileName = brandViewModel.Logo.FileName;
                string filePath = Path.Combine(uploadFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    brandViewModel.Logo.CopyTo(fileStream);
                }
            }

            return "/images/brands/" + fileName;
        }

        // GET: BrandController/Edit/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BrandController/Edit/5
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

        // GET: BrandController/Delete/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BrandController/Delete/5
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
    }
}

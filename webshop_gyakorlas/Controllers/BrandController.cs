﻿using Microsoft.AspNetCore.Authorization;
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
        #region Adattagok
        public ApplicationDbContext _context;
        private IWebHostEnvironment _environment;
        #endregion

        #region Konstruktor
        public BrandController(IWebHostEnvironment environment)
        {
            _context = new ApplicationDbContext();
            _environment = environment;
        }
        #endregion

        #region Alprogramok
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
                BrandViewModel brandViewModel = new BrandViewModel()
                {
                    Name = brand.Name,
                    Descreption = brand.Descreption,
                    Brand = brand
                };

                return View(brandViewModel);
            }
            else
            {
                return View("Error");
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
                string fileName;

                if (brandViewModel.Logo == null)
                {
                    fileName = Path.Combine(Path.DirectorySeparatorChar.ToString(), "images", "noimage.png");
                }
                else
                {
                    fileName = UploadFile(brandViewModel);
                }

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
        }

        // GET: BrandController/Edit/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(int id)
        {
            Brand brand = _context.Brands.ToList().Find(b => b.Id == id);

            if (brand != null)
            {
                BrandViewModel brandViewModel = new BrandViewModel()
                {
                    Name = brand.Name,
                    Descreption = brand.Descreption,
                    Brand = brand
                };

                return View(brandViewModel);
            }
            else
            {
                return View("Error");
            }
        }

        // POST: BrandController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Edit(BrandViewModel brandViewModel, int id)
        {
            Brand brand = _context.Brands.ToList().Find(b => b.Id == id);

            if (brandViewModel.Logo != null)
            {
                DeleteFile(brand.Logo);
                brand.Logo = UploadFile(brandViewModel);
            }

            brand.Name = brandViewModel.Name;
            brand.Descreption = brandViewModel.Descreption;

            _context.Brands.Update(brand);
            _context.SaveChanges();

            return RedirectToAction("Index", "Brand");
        }

        // GET: BrandController/Delete/5
        [Authorize(Roles = RoleName.Admin)]
        public ActionResult Delete(int id)
        {
            Brand brand = _context.Brands.ToList().Find(b => b.Id == id);

            if (brand != null)
            {
                if (!brand.Logo.Contains("noimage.png"))
                {
                    DeleteFile(brand.Logo);
                }

                _context.Brands.Remove(brand);
                _context.SaveChanges();
            }

            return RedirectToAction("Index", "Brand");
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

            return Path.Combine(Path.DirectorySeparatorChar.ToString(), "images", "brands", fileName);
        }

        private void DeleteFile(string logoPath)
        {
            System.IO.File.Delete(_environment.WebRootPath + logoPath);
        }
        #endregion
    }
}

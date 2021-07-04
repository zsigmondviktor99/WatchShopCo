using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using webshop_gyakorlas.Models;

namespace webshop_gyakorlas.ViewModels
{
    public class WatchViewModelForShopIndex
    {
        public Watch Watch { get; set; }
        public static string WwwrootPath { get; set; }

        public string Title
        {
            get
            {
                return $"{Watch.Brand.Name} {Watch.Model} ({Watch.ReferenceNumber})";
            }
        }

        public string FirstImagePath
        {
            get
            {
                string[] imagesPath = Directory.GetFiles(WwwrootPath + Watch.ImagesPath);
                string[] firstImagePathParts;

                try
                {
                    firstImagePathParts = imagesPath.First(p => p.Contains("main")).Split(Path.DirectorySeparatorChar);
                }
                catch
                {
                    firstImagePathParts = imagesPath[0].Split(Path.DirectorySeparatorChar);
                }

                int wwwrootindex = 0;

                while (wwwrootindex < firstImagePathParts.Length && firstImagePathParts[wwwrootindex] != "wwwroot")
                {
                    wwwrootindex++;
                }

                string firstImagePath = "";

                for (int i = wwwrootindex + 1; i < firstImagePathParts.Length; i++)
                {
                    firstImagePath += Path.DirectorySeparatorChar.ToString() + firstImagePathParts[i];
                }

                return firstImagePath;
            }
        }
    }
}

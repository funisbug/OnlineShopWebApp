using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;

namespace OnlineShopWebApp.Helpers
{
    public class ImageProvider
    {
        private readonly IWebHostEnvironment appEnvironment;

        public ImageProvider(IWebHostEnvironment appEnvironment)
        {
            this.appEnvironment = appEnvironment;
        }

        public string SafeFile(IFormFile file, ImageFolders folder)
        {
            if (file != null)
            {
                var folderPath = Path.Combine(appEnvironment.WebRootPath + "/images/" + folder);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                var fileName = Guid.NewGuid() + "." + file.FileName.Split('.').Last();
                string path = Path.Combine(folderPath, fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                return "/images/" + folder + "/" + fileName;
            }
            return "/images/products/a790b802-446c-4c18-8787-dfd9392fc78b.jpg";
        }
    }
}

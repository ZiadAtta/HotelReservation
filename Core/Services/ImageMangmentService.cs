using Ecom.core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.FileProviders;

namespace Ecom.Infrastructture.Repositories.Service
{
    public class ImageMangmentService : IImageMandmentService
    {
        private readonly IFileProvider _fileProvider;
        public ImageMangmentService(IFileProvider fileProvider)
        {
            _fileProvider = fileProvider;
        }
        public async Task<List<string>> AddImageAsync(IFormFileCollection files, string src)
        {
            var saveImagesSrc = new List<string>();
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var ImageDirctory = Path.Combine("wwwroot", "Images", src);//"wwwroot\\Images\\Iphon 16"
            if (Directory.Exists(ImageDirctory) is not true)
            {
                Directory.CreateDirectory(ImageDirctory);
            }

            foreach (var file in files)
            {
                if (file.Length > 0)
                {
                    //check image extentions
                    var extension = Path.GetExtension(file.FileName).ToLower();
                    if (!allowedExtensions.Contains(extension))
                    {
                        // skip file or throw an exception
                        throw new ArgumentException($"Only {string.Join(",", allowedExtensions)} are allowed.");
                        // continue;
                    }
                    //get image name
                    var imageName = file.FileName;// Lab-4.jpeg"
                    var imageSrc = $"/Images/{src}/{imageName}";// "/Images/Iphon 16/Lab-4.jpeg"
                    var root = Path.Combine(ImageDirctory, imageName);// "wwwroot\\Images\\Iphon 16\\Lab-4.jpeg"
                    using (FileStream stream = new FileStream(root, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    saveImagesSrc.Add(imageSrc);
                }
            }
            return saveImagesSrc;
        }

        public void DeleteImageAsync(string src)
        {
            var info = _fileProvider.GetFileInfo(src);
            var root = info.PhysicalPath;
            File.Delete(root);
        }
    }
}

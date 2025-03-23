using BLL.Contract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FileServices : IFileService
    {
        public void DeleteFile(string oldImage)
        {
            if (string.IsNullOrEmpty(oldImage))
            {
                throw new ArgumentException("File name cannot be null or empty", nameof(oldImage));
            }

            var filePath = Path.Combine(@"./wwwroot/Images", oldImage);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
        //--------------------------------------------------------------------------------

        public async Task<string> SaveFile(object imageFile, string[] allowedExtensions)
        {
            if (imageFile is not IFormFile file)
            {
                throw new ArgumentException("Invalid file type", nameof(imageFile));
            }

            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException("File type is not allowed");
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var uploadsFolder = Path.Combine(@"./wwwroot/Images");
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            Directory.CreateDirectory(uploadsFolder);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return uniqueFileName;
        }
        //--------------------------------------------------------------------------------
        public string UploadFile(IFormFile file, string destinationFolder)
        {
            var uniqueFileName = string.Empty;

            if (file != null && file.Length > 0)
            {
                //    "./wwwroot/Images"

                // Escape Characters 

                //"I said \'hello\' "
                //@"I said 'hello' "

                var uploadsFolder = Path.Combine(@"./wwwroot/", destinationFolder);
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;  // ==> map to  uniqueidentifier  in sql server
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                //   "./wwwroot/Images/6394736969856_Mohamed.jpg"
                using (var fileStream = new FileStream(filePath, FileMode.Create)) // Resources written inside using()  must implement IDisposable interface
                {
                    file.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}

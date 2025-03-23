using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Contract
{
    public interface IFileService
    {
        void DeleteFile(string oldImage);
        Task<string> SaveFile(object imageFile, string[] allowedExtensions);
        string UploadFile(IFormFile file, string destinationFolder);
    }
}

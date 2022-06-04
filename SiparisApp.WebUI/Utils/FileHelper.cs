using System.IO; // dosya yükleme işlemleri için
using Microsoft.AspNetCore.Http;

namespace SiparisApp.WebUI.Utils
{
    public class FileHelper
    {
        public static string FileLoader(IFormFile formFile, string filePath = "/Img/")
        {
            string fileName = "";

            if (formFile != null)
            {
                fileName = formFile.FileName;
                string directory = Directory.GetCurrentDirectory() + "/wwwroot" + filePath + fileName;
                using var stream = new FileStream(directory, FileMode.Create);
                formFile.CopyTo(stream);
            }

            return fileName;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Utilities
{
    public static class FileHelper
    {
        public static string CreateFile(Microsoft.AspNetCore.Http.IFormFile file, string relativePath, string fileName)
        {
            var fileType = file.FileName.Substring(file.FileName.LastIndexOf('.'));
            var path = Path.GetTempFileName();
            path = path.Replace(".tmp", fileType);
            if (file.Length > 0)
                using (var stream = new FileStream(path, FileMode.Create))
                    file.CopyTo(stream);
            File.Move(path, Directory.GetCurrentDirectory() + $"/wwwroot/{relativePath}/{fileName}{fileType}", true);
            return $"{relativePath}/{fileName}{fileType}";
        }
    }
}

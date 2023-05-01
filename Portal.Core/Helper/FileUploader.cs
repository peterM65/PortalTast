using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Portal.Core.Helper
{
    public static class FileUploader
    {
        public static string UploadFile(string folderName, IFormFile file)
        {

            try
            {
                // 1 ) Get Directory
                string FolderPath = Directory.GetCurrentDirectory() + Path.Combine("/wwwroot/Docs/", folderName);


                //2) Get File Name
                string FileName = Guid.NewGuid() + Path.GetFileName(file.FileName);


                // 3) Merge Path with File Name
                string FinalPath = Path.Combine(FolderPath, FileName);


                //4) Save File As Streams "Data Overtime"
                using (var Stream = new FileStream(FinalPath, FileMode.Create))
                {
                    file.CopyTo(Stream);
                }

                return FileName;

            }
            catch (Exception ex)
            {
                return ex.Message;

            }
        }
        public static void RemoveFile(string folderName, string fileName)

        {

            var path = Directory.GetCurrentDirectory() + Path.Combine("/wwwroot/Docs", folderName, fileName);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

    }
}


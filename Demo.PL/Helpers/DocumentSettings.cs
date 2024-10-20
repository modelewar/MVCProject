using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace Demo.PL.Helpers
{
    public class DocumentSettings
    {
        //1:Upload
        public static string UploadFile(IFormFile file , string FolderName )
        {
            //1:Get Located FolderPath
            string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Fiels",FolderName);

            //2.Get FileName And Make It Unique
            string FileName = $"{Guid.NewGuid()}{file.FileName}";
            
            //3.Get FilePath[ FolderPath , FileName]
            string FilePath = Path.Combine(FolderPath, FileName);

            //4.Save File As Stream
            var FS = new FileStream(FilePath, FileMode.Create);
            file.CopyTo(FS);

            //5.Return FileName
            return FileName;
        }
    }
}

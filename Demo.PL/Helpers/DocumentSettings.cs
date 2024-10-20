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

        //2:Delete 

        public static void DeleteFile(string FolderName , string FileName)
        {
            //1:Get Located FolderPath
            string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwroot\\Files",FolderName,FileName);

            //2: Delete File If Exists
            if (File.Exists(FilePath))
            {
                File.Delete(FilePath);
            }
            

        }
    }
}

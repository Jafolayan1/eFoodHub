using eFoodHub.UI.Interfaces;

namespace eFoodHub.UI.Helpers
{
    public class FileHelper : IFileHelper
    {
        private readonly IWebHostEnvironment _env; //To get the information specific to the enviroment.

        public FileHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        /// <summary>
        /// Chages the fileName to current daytimeyear
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static string GenerateFileName(string fileName)
        {
            string[] strName = fileName.Split('.');
            string strFileName = $"{DateTime.Now.ToUniversalTime():yyyyMMdd\\THHmmssfff}.{strName[^1]}";
            return strFileName;
        }

        public void DeleteFile(string imageUrl)
        {
            if (File.Exists($"{_env.WebRootPath}{imageUrl}"))
            {
                File.Delete($"{_env.WebRootPath}{imageUrl}");
            }
        }

        public string UploadFile(IFormFile file)
        {
            var uploads = Path.Combine(_env.WebRootPath, "images");
            bool exist = Directory.Exists(uploads);
            if (!exist)
                Directory.CreateDirectory(uploads);

            //Saving File
            var fileName = GenerateFileName(file.FileName);
            var fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create);
            file.CopyToAsync(fileStream);

            return "/images/" + fileName;
        }
    }
}
namespace eFoodHub.UI.Interfaces
{
    public interface IFileHelper
    {
        void DeleteFile(string imageUrl);

        string UploadFile(IFormFile file);
    }
}
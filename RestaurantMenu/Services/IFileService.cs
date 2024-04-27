namespace RestaurantMenu.Services
{
    public interface IFileService
    {
        Tuple<int, string> SaveImage(IFormFile imageFile, string folderPath);
        public bool DeleteImage(string imageFileName, string folderPath);
    }
}

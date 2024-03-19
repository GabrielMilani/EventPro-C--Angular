namespace EventPro.Api.Helpers;

public interface IUtils
{
    Task<string> SaveImage(IFormFile imageFile, string destination);
    void DeleteImage(string imageName, string destination);
}
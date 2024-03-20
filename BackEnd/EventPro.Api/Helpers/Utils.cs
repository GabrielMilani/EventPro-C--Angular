
using Microsoft.AspNetCore.Hosting;

namespace EventPro.Api.Helpers;

public class Utils : IUtils
{

    private readonly IWebHostEnvironment _webHostEnvironment;

    public Utils(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public void DeleteImage(string imageName, string destination)
    {
        if (!string.IsNullOrEmpty(imageName))
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, @$"Resources/{destination}", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }

    public async Task<string> SaveImage(IFormFile imageFile, string destination)
    {
        string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
            .Take(10).ToArray()).Replace(' ','-');
        imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";
        var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, @$"Resources/{destination}", imageName);
        using (var fileStream = new FileStream(imagePath, FileMode.Create))
        {
            await imageFile.CopyToAsync(fileStream);
        };
        return imageName;
    }
}
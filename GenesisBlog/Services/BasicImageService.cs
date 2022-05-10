using GenesisBlog.Services.Interfaces;

namespace GenesisBlog.Services
{
    public class BasicImageService : IImageService
    {
      
    public string ConvertByteArrayToFile(byte[] imageData, string ext)
    {
        var fileData = Convert.ToBase64String(imageData);
        var srcString = $"data:{ext};base64,{fileData}";
        return srcString;
    }

    public async Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
    {
        //Introduce an error handling mechanism known as a Try/Catch block
        try
        {
            using MemoryStream memoryStream = new();
            await file.CopyToAsync(memoryStream);
            var byteFile = memoryStream.ToArray();
            return byteFile;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }

    }

}

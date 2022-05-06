using GenesisBlog.Services.Interfaces;

namespace GenesisBlog.Services
{
    public class BasicImageService : IImageService
    {
        public string ConvertByteArrayToFile(byte[] imageData, string ext)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}

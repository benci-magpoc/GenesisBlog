namespace GenesisBlog.Services.Interfaces
{
    //I know what I want to do I just dont know how to do it....
    public interface IImageService
    {
        Task<byte[]> ConvertFileToByteArrayAsync(IFormFile file);
        string ConvertByteArrayToFile(byte[] imageData, string ext);
    }
}

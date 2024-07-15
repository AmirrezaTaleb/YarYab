using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public static class FileHelper
{
    public static async Task<byte[]> ConvertToByteArrayAsync(this IFormFile file)
    {
        if (file == null || file.Length == 0)
            return null;

        using (var memoryStream = new MemoryStream())
        {
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}

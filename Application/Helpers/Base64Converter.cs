using Microsoft.AspNetCore.Http;

namespace Application.Helpers;

public static class Base64Converter
{
    public static string ConvertToBase64(IFormFile formFile)
    {
        if (formFile == null || formFile.Length == 0)
        {
            return string.Empty;
        }

        using var memoryStream = new MemoryStream();
        formFile.CopyTo(memoryStream);
        var fileBytes = memoryStream.ToArray();

        return Convert.ToBase64String(fileBytes);
    }
    
    public static byte[] ConvertToByteArray(string base64String)
    {
        return Convert.FromBase64String(base64String);
    }
}
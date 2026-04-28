using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PastryFlow.Application.Common;

public static class FileUploadHelper
{
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".heic" };
    private const long MaxFileSize = 10 * 1024 * 1024; // 10 MB

    /// <summary>
    /// Dosyayı belirtilen kategori altına kaydeder.
    /// Örnek: category="deliveries" → uploads/deliveries/2024-04-27/guid.jpg
    /// </summary>
    public static async Task<string> SaveFileAsync(
        IFormFile file,
        string category,
        string contentRootPath,
        CancellationToken ct = default)
    {
        // Validasyon
        if (file == null || file.Length == 0)
            throw new ArgumentException("Dosya boş olamaz.");
            
        if (file.Length > MaxFileSize)
            throw new ArgumentException($"Dosya boyutu {MaxFileSize / (1024 * 1024)} MB'dan büyük olamaz.");
            
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!AllowedExtensions.Contains(extension))
            throw new ArgumentException($"Geçersiz dosya formatı. İzin verilen formatlar: {string.Join(", ", AllowedExtensions)}");

        // Dizin oluştur
        var dateFolder = DateTime.UtcNow.ToString("yyyy-MM-dd");
        var relativePath = Path.Combine("uploads", category, dateFolder);
        var absolutePath = Path.Combine(contentRootPath, relativePath);
        
        if (!Directory.Exists(absolutePath))
            Directory.CreateDirectory(absolutePath);

        // Benzersiz dosya adı
        var fileName = $"{Guid.NewGuid()}{extension}";
        var filePath = Path.Combine(absolutePath, fileName);

        // Kaydet
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream, ct);
        }

        // Relative URL döndür (veritabanına kaydedilecek)
        return $"/{relativePath.Replace("\\", "/")}/{fileName}";
    }

    /// <summary>
    /// Dosyayı siler
    /// </summary>
    public static void DeleteFile(string relativeUrl, string contentRootPath)
    {
        if (string.IsNullOrEmpty(relativeUrl)) return;
        
        var absolutePath = Path.Combine(contentRootPath, relativeUrl.TrimStart('/'));
        if (File.Exists(absolutePath))
        {
            File.Delete(absolutePath);
        }
    }
}

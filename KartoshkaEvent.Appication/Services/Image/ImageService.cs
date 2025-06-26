using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace KartoshkaEvent.Application.Services.Image
{
    public class ImageService : IImageService
    {

        public async Task<List<byte[]>> GetBytesAsync(List<IFormFile> images, CancellationToken ct)
        {
            var bytes = new List<byte[]>();
            foreach (var image in images)
            {
                using var memory = new MemoryStream();
                await image.CopyToAsync(memory, ct);
                bytes.Add(memory.ToArray());
            }
            return bytes; 
        }

        public List<EventImage> WriteImagesAndReturn(List<byte[]> imagesBytes, Guid eventId)
        {
            if (!Directory.Exists("wwwroot/"))
                Directory.CreateDirectory("wwwroot/");

            Directory.SetCurrentDirectory("wwwroot/");

            if (!Directory.Exists("images/"))
                Directory.CreateDirectory("images/");

            var images = new List<EventImage>();

            foreach (var bytes in imagesBytes)
            {
                var imageId = Guid.NewGuid();

                if (!Directory.Exists($"images/{eventId}))"))
                    Directory.CreateDirectory($"images/{eventId}");

                var pathToWrite = $"images/{eventId}/{imageId}.webp";
                images.Add(new() { Id = imageId, EventId = eventId, ImagePath = pathToWrite });

                File.WriteAllBytes(pathToWrite, bytes);
            }

            Directory.SetCurrentDirectory("/app");

            return images;
        }
    }
}

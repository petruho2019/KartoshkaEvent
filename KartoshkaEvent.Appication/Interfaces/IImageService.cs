using KartoshkaEvent.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface IImageService
    {
        Task<List<byte[]>> GetBytesAsync(List<IFormFile> images, CancellationToken ct);

        List<EventImage> WriteImagesAndReturn(List<byte[]> imagesBytes, Guid eventId);
    }
}

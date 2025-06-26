using KartoshkaEvent.Application.Interfaces;
using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Services.Tags
{
    public class TagService : ITagService
    {
        public Result<List<Tag>> GetValidTags(Dictionary<string, Tag> tagsFromDb, List<string> namesTagsFromRequest)
        {
            var validTags = new List<Tag>();
            foreach (var nameOfTag in namesTagsFromRequest)
            {
                if (!tagsFromDb.TryGetValue(nameOfTag, out var tag))
                    return Result<List<Tag>>.BadRequest($"Тег '{nameOfTag}' не найден");
                validTags.Add(tag);
            }

            return Result<List<Tag>>.Ok(validTags);
        }
    }
}

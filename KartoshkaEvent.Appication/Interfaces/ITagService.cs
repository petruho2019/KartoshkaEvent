using KartoshkaEvent.Domain.Common.Utils;
using KartoshkaEvent.Domain.Models;

namespace KartoshkaEvent.Application.Interfaces
{
    public interface ITagService
    {
        Result<List<Tag>> GetValidTags(Dictionary<string, Tag> tagsFromDb, List<string> namesTagsFromRequest);
    }
}

using GameReviewSite.Infrastructure.Data;

namespace GameReviewSite.Core.Contracts
{
    public interface ITagService
    {
        Task<bool> CreateTag(string name);

        Task<IEnumerable<Tag>> GetTags();
    }
}

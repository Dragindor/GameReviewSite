using GameReviewSite.Infrastructure.Data;

namespace GameReviewSite.Core.Contracts
{
    public interface ITagService
    {
        Task<bool> CreateTag(string name);

        Task<Tag> GetTagForEdit(string id);

        Task<IEnumerable<Tag>> GetTags();

        Task<bool> UpdateTag(Tag tag);
    }
}

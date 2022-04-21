using GameReviewSite.Core.Contracts;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Repositories;

namespace GameReviewSite.Core.Services
{
    public class TagService :ITagService
    {
        public readonly ApplicationDbContext data;
        public readonly IApplicationDbRepository repo;
        public TagService(ApplicationDbContext _data, IApplicationDbRepository _repo)
        {
            data = _data;
            repo = _repo;
        }

        public async Task<bool> CreateTag(string name)
        {
            var tag = new Tag()
            { Name = name };
            await data.Tags.AddAsync(tag);
            await data.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            var tags = this.data.Tags.ToList();

            return tags;
        }
    }
}

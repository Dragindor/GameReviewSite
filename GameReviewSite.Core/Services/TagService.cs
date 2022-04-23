using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
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

        public async Task<bool> RemoveFromTagsAsync(AddGameViewModel game, List<Tag> tags)
        {
            game.Tags = null;
            if (game.Tags == null)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> AddToTagsAsync(AddGameViewModel game, List<AddTagToGame> model)
        {
            game.Tags = new List<Tag>();
            try
            {
                foreach (var tag in model)
                {
                    if (tag.Selected)
                    {
                        game.Tags.Add(new Tag { Id = tag.Id, Name = tag.Name });
                    }
                    
                }
                var gameTag = await repo.GetByIdAsync<Game>(game.Id);

                if (gameTag != null)
                {
                    gameTag.Tags = game.Tags;                  

                    await repo.SaveChangesAsync();
                }
                await data.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }           
        }

        public async Task<bool> CreateTag(string name)
        {
            var tag = new Tag()
            { Name = name };
            await data.Tags.AddAsync(tag);
            await data.SaveChangesAsync();
            return true;
        }

        public async Task<Tag> GetTagForEdit(string id)
        {
            var tag = await repo.GetByIdAsync<Tag>(id);

            if (tag==null)
            {
                throw new ArgumentNullException(nameof(tag));
            }

            var foundTag = new Tag()
            {
                Id = id,
                Name = tag.Name
            };

            return foundTag;
        }

        public async Task<IEnumerable<Tag>> GetTags()
        {
            var tags = this.data.Tags.ToList();

            return tags;
        }


        public async Task<bool> UpdateTag(Tag tag)
        {
            bool result = false;

            var Tag = await repo.GetByIdAsync<Tag>(tag.Id);

            if (Tag != null)
            {
                Tag.Name = tag.Name;
                await repo.SaveChangesAsync();
                result = true;
            }

            return result;
        }
    }
}

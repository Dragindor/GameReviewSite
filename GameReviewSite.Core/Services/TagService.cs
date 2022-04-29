using GameReviewSite.Core.Contracts;
using GameReviewSite.Core.Models;
using GameReviewSite.Infrastructure.Data;
using GameReviewSite.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> RemoveFromTagsAsync(Game game, List<Tag> tags)
        {
             var gamechanged = data.Games.Where(x => x.Id == game.Id)
                .Include(x => x.Tags)
                .FirstOrDefault();
            gamechanged.Tags = new List<Tag>();
            await repo.SaveChangesAsync();
            if (gamechanged.Tags.Count==0)
            {
                return true;
            }
            else return false;
        }

        public async Task<bool> AddToTagsAsync(Game game, List<AddTagToGame> model)
        {              
            try
            {
                var gameTags = data.Games.Where(x => x.Id == game.Id)
                .Include(x => x.Tags)
                .FirstOrDefault();
                foreach (var tag in model)
                {
                    if (tag.Selected)
                    {
                        Tag tagFound = data.Tags.FirstOrDefault(t => t.Id == tag.Id);
                        gameTags.Tags.Add(tagFound);
                        await repo.SaveChangesAsync();
                    }
                    
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }           
        }

        public async Task<bool> CreateTag(string name)
        {
            if (name==null)
            {
                return false;
            }
            else if (await TagAlreadyExist(name))
            {
                return false;
            }

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
                throw new ArgumentNullException($"The tag with id:{id} was not found.");
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
        public async Task<bool> TagAlreadyExist(string tagName)
        {
            var tag = await data.Tags.FirstOrDefaultAsync(x => x.Name == tagName);

            if (tag == null)
            {
                return false;
            }

            return true;
        }
    }
}

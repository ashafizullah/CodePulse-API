using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation;

public class BlogPostRepository(ApplicationDbContext dbContext) : IBlogPostRepository
{
    public async Task<BlogPost> CreateAsync(BlogPost blogPost)
    {
        await dbContext.BlogPosts.AddAsync(blogPost);
        await dbContext.SaveChangesAsync();

        return blogPost;
    }

    public async Task<IEnumerable<BlogPost>> GetAllAsync()
    {
        return await dbContext.BlogPosts.ToListAsync();
    }
}
using CodePulse.API.Data;
using CodePulse.API.Models.Domain;
using CodePulse.API.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodePulse.API.Repositories.Implementation;

public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    public async Task<Category> CreateAsync(Category category)
    {
        await dbContext.Categories.AddAsync(category);
        await dbContext.SaveChangesAsync();

        return category;
    }

    public async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await dbContext.Categories.ToListAsync();
    }

    public async Task<Category?> GetById(Guid id)
    {
        return await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Category?> UpdateAsync(Category category)
    {
        var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == category.Id);

        if (existingCategory is not null)
        {
            dbContext.Entry(existingCategory).CurrentValues.SetValues(category);
            await dbContext.SaveChangesAsync();

            return category;
        }

        return null;
    }

    public async Task<Category?> DeleteAsync(Guid id)
    {
        var existingCategory = await dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

        if (existingCategory is null)
        {
            return null;
        }

        dbContext.Categories.Remove(existingCategory);
        await dbContext.SaveChangesAsync();

        return existingCategory;
    }
}
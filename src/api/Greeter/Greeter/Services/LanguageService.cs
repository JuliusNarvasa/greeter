using Greeter.Models;
using Microsoft.EntityFrameworkCore;

namespace Greeter.Services;

public class LanguageService(GreeterDbContext dbContext)
{
    public async Task<Language[]> GetLanguages()
    {
        var languages = await dbContext.Languages.ToArrayAsync();
        
        return languages.ToArray();
    }
}
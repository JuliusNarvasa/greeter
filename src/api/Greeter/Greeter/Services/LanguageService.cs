using Greeter.Models;
using Microsoft.EntityFrameworkCore;

namespace Greeter.Services;

public class LanguageService(GreeterDbContext dbContext, OpenApiChatService openApiChatService)
{
    public async Task<Language[]> GetLanguages(string request)
    {
        var apiKey = openApiChatService.GetOpenApiKey(request);
        var languages = await dbContext.Languages.ToArrayAsync();
        
        return languages.ToArray();
    }
}
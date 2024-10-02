using Greeter.DTO;
using Greeter.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Exception = System.Exception;

namespace Greeter.Services;

public class LanguageService(GreeterDbContext dbContext, OpenApiChatService openApiChatService)
{
    public async Task<Language[]> GetLanguages()
    {
        var languages = await dbContext.Languages.ToArrayAsync();
        
        return languages.ToArray();
    }

    public async Task<Language[]> AddLanguages(LanguageRequestDto request)
    {
        var newLanguages = request.languages.Select(language =>
            new Language()
            {
                LanguageId = Guid.NewGuid(),
                LanguageName = language.Language,
                CountryOfOrigin = language.CountryOfOrigin
            });
        
        dbContext.Languages.AddRange(newLanguages);
        await dbContext.SaveChangesAsync();
        
        var languages = await dbContext.Languages.ToArrayAsync();
        
        return languages.ToArray();
    }

    public async Task<LanguageGreetingResponse> GetRandomLanguageGreeting(string name)
    {
        var languages = await dbContext.Languages
            .Include(l => l.Translations)
            .ToArrayAsync();
        
        if (languages.Length == 0)
        {
            throw new Exception("No languages available to translate");
        }
        
        var random = new Random();
        var randomIndex = random.Next(0, languages.Length);
        var randomLanguage = languages[randomIndex];

        var response = new LanguageGreetingResponse()
        {
            LanguageName = randomLanguage.LanguageName,
            CountryOfOrigin = randomLanguage.CountryOfOrigin,
        };
        
        Translation translation;

        if (randomLanguage.Translations.Count == 0)
        {
            var responseStringObject = await openApiChatService.RequestGreetingTranslation(randomLanguage.LanguageName);
        
            var translationString = JsonConvert.DeserializeObject<TranslationResponse>(responseStringObject)!;

            translation = new Translation()
            {
                TranslationId = Guid.NewGuid(),
                LanguageId = randomLanguage.LanguageId,
                Morning = translationString.morning,
                Afternoon = translationString.afternoon,
                Evening = translationString.evening,
            };
            
            dbContext.Translations.Add(translation);
            await dbContext.SaveChangesAsync();
        }
        else
        {
            translation = randomLanguage.Translations.First();
        }
        
        response.Morning = translation.Morning.Replace("[name]", name);
        response.Afternoon = translation.Afternoon.Replace("[name]", name);
        response.Evening = translation.Evening.Replace("[name]", name);

        return response;
    }
}
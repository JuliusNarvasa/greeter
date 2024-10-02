using ChatGPT.Net;

namespace Greeter.Services;

public class OpenApiChatService(IConfiguration configuration)
{
    public async Task<string> RequestGreetingTranslation(string requestedLanguage)
    {
        var apiKey = configuration["OPENAPI_API_KEY"];

        if (apiKey == null)
        {
            throw new ApplicationException("OPENAPI_API_KEY is missing");
        }
        
        var openai = new ChatGpt(apiKey);

        try
        {
            var response = await openai.Ask($"You are a program that just translates \"Good Morning [name]!\", \"Good Evening [name]!\", \"Good Afternoon [name]!\" to different languages that will be specified.\n\nYou will accept in this format:\n[language]\n\nFor example:\nlatin\n\nYou will output translations for \"Good Morning [name]\",\"Good Afternoon [name]\", \"Good Evening [name]\" in Latin, but Romanized, in JSON format like this:\n\n{{\n    \"morning\": [MORNING TRANSLATION HERE],\n    \"afternoon\": [AFTERNOON TRANSLATION HERE],\n    \"evening\": [EVENING TRANSLATION HERE]\n}}\n\nNow do this: {requestedLanguage}");
            return response;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}

public class TranslationResponse
{
    public string morning { get; set; }
    public string afternoon { get; set; }
    public string evening { get; set; }
}
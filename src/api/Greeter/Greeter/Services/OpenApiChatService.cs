using ChatGPT.Net;

namespace Greeter.Services;

public class OpenApiChatService(IConfiguration configuration)
{
    public async Task<string> GetOpenApiKey(string request)
    {
        var apiKey = configuration["OPENAPI_API_KEY"];

        if (apiKey == null)
        {
            throw new ApplicationException("OPENAPI_API_KEY is missing");
        }
        
        var openai = new ChatGpt(apiKey);

        try
        {
            var fixedSentence = await openai.Ask($"Fix the following sentence for spelling and grammar: {request}");
            return fixedSentence;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
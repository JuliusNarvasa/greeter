using Greeter.Services;
using Microsoft.AspNetCore.Mvc;

namespace Greeter.Controllers;

[ApiController]
[Route("[controller]")]
public class OpenApiController(OpenApiChatService openApiChatService) : ControllerBase
{
    [HttpGet("test-api")]
    public async Task<IActionResult> GetLanguages()
    {
        try
        {
            var languages = await openApiChatService.GetOpenApiKey("Test");
            return Ok(languages);
        }
        catch (ApplicationException e)
        {
            Console.WriteLine(e);
            return StatusCode(500);
        }
    }
}